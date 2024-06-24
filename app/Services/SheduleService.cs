using app.Dtos.SheduleDay;
using app.Dtos.Subject;
using app.Exceptions;
using app.Interfaces;
using app.Interfaces.Shedule;
using app.Mappers;
using app.Models;

namespace app.Services
{
    public class SheduleService(ISheduleRepository sheduleRepository, ISubjectRepository subjectRepository,
                            IHomeworkService homeworkService, IStudentGroupSubjectService groupSubjectService, IStudentService studentService) : ISheduleService
    {
        private readonly ISheduleRepository _sheduleRepo = sheduleRepository;
        private readonly ISubjectRepository _subjectRepo = subjectRepository;
        private readonly IHomeworkService _homeworkService = homeworkService;
        private readonly IStudentGroupSubjectService _groupSubjectService = groupSubjectService;
        private readonly IStudentService _studentService = studentService;

        public async Task<SheduleRequestDto> CreateSheduleAsync(long headBoyChatId, SheduleRequestDto sheduleRequestDto)
        {
            var headBoyModel = await _studentService.GetHeadBoyOrThrowExceptionAsync(headBoyChatId);

            await _sheduleRepo.DeleteSheduleIfExistsByStudentGroupIdAsync(headBoyModel.StudentGroupId);

            Dictionary<string, Subject> subjectCache = [];

            for (byte w = 0; w < sheduleRequestDto.Weeks.Length; w++) //Неделя
            {
                var week = sheduleRequestDto.Weeks[w];

                for (byte d = 0; d < week.Days.Count; d++) //Дни
                {
                    var day = week.Days[d];
                    List<SheduleDaySubject> subjects = await ProcessPairsInDayAsync(day.PairsInDay, subjectCache);
                    var sheduleDayModel = CreateSheduleDayModel(headBoyModel.StudentGroupId, w, d, subjects);
                    await _sheduleRepo.CreateSheduleForDayAsync(sheduleDayModel);
                }
            }
            await _groupSubjectService.SyncGroupSubjectsFromScheduleAsync(headBoyModel.StudentGroupId, subjectCache);

            return sheduleRequestDto;
        }

        private async Task<List<SheduleDaySubject>> ProcessPairsInDayAsync(List<PairDto> pairsInDay, Dictionary<string, Subject> subjectCache)
        {
            byte spot = 1;
            List<SheduleDaySubject> sheduleDaySubjects = [];

            foreach (var pair in pairsInDay) //Пары
            {
                if (pair != null) // Если не окно
                {
                    string subject = pair.Subject;
                    char? subgroup = pair.Subgroup;

                    Subject subjectModel = await EnsureSubjectExistsAsync(subject, subjectCache);

                    sheduleDaySubjects.Add(new SheduleDaySubject
                    {
                        SubjectId = subjectModel.Id,
                        Subgroup = subgroup,
                        Spot = spot
                    });
                }
                spot++;
            }
            return sheduleDaySubjects;
        }

        private async Task<Subject> EnsureSubjectExistsAsync(string subjectTitle, Dictionary<string, Subject> subjectCache)
        {
            if (!subjectCache.ContainsKey(subjectTitle))
            {
                Subject? subjectModel = await _subjectRepo.GetByNameAsync(subjectTitle);
                subjectModel ??= await _subjectRepo.CreateAsync(new Subject { Title = subjectTitle });
                subjectCache.Add(subjectTitle, subjectModel);
            }

            return subjectCache[subjectTitle];
        }

        private SheduleDay CreateSheduleDayModel(int studentGroupId, byte week, byte day, List<SheduleDaySubject> subjects)
        {
            return new SheduleDay
            {
                StudentGroupId = studentGroupId,
                CountWeek = ++week,
                CountDay = day,
                SheduleDaySubjects = subjects,
            };
        }

        public async Task<SheduleRequestDto> GetSheduleByChatIdAsync(long studentChatId)
        {
            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            if (!await _sheduleRepo.CheckIfExistsSheduleDaysByStudentGroupIdAsync(studentModel.StudentGroupId))
            {
                throw new DataNotFoundException("Расписание не добавлено");
            }

            var sheduleDays = await _sheduleRepo.GetAllByStudentGroupId(studentModel.StudentGroupId);
            sheduleDays = sheduleDays.OrderBy(sd => sd.CountWeek).ThenBy(sd => sd.CountDay).ToList();

            var sheduleDto = new SheduleRequestDto();
            for (int w=0; w<2; w++)
            {
                sheduleDto.Weeks[w] = new WeekDto();
                foreach (var day in sheduleDays.Where(sd => sd.CountWeek == w+1).ToList())
                {
                    var sheduleDaySubjects = day.SheduleDaySubjects.OrderBy(sds => sds.Spot).ToList();
                    var pairsInDayDto = new List<PairDto?>();

                    int index = 1;
                    foreach (var subject in sheduleDaySubjects)
                    {
                        while (index != subject.Spot)
                        {
                            pairsInDayDto.Add(null);
                            index++;
                        }
                        pairsInDayDto.Add(subject.ToPairDtoFromSheduleDaySubject());
                        index++;
                    }
                    sheduleDto.Weeks[w].Days.Add(new DayDto { PairsInDay = pairsInDayDto });
                }
            }
            return sheduleDto;
        }

        public async Task<GetSheduleDayRequestDto> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId)
        {
            string[] daysOfWeeks = ["Понедельник", "Вторник", "Cреда", "Четверг", "Пятница", "Суббота", "Воскресенье"];

            var studentModel = await _studentService.GetStudentOrThrowExceptionAsync(studentChatId);
            if (!await _sheduleRepo.CheckIfExistsSheduleDaysByStudentGroupIdAsync(studentModel.StudentGroupId))
            {
                throw new DataNotFoundException("Расписание не добавлено");
            }

            var (numberOfWeeks, dayOfWeek) = GetNextWeekAndDay();

            var sheduleDayModel = await _sheduleRepo.GetNextSheduleDayByStudentGroupIdAsync(studentModel.StudentGroupId, numberOfWeeks, dayOfWeek);
            var sheduleDaySubjectsModels = sheduleDayModel.SheduleDaySubjects.OrderBy(s => s.Spot).ToList();

            List<GetSheduleDaySubjectRequestDto?> sheduleDaySubjectsDto = [];
            byte spot = 1;
            foreach (var subject in sheduleDaySubjectsModels)
            {
                if (subject.Spot != spot)
                {
                    sheduleDaySubjectsDto.Add(null);
                }
                sheduleDaySubjectsDto.Add(new GetSheduleDaySubjectRequestDto
                {
                    SubjectId = subject.SubjectId,
                    Title = subject.Subject.Title,
                    Subgroup = subject.Subgroup,
                    Spot = subject.Spot
                });
                spot++;
            }

            var homeworks = await _homeworkService.GetHomeworksForSubjects(sheduleDaySubjectsModels.Select(sds => sds.SubjectId).ToList(), studentModel.StudentGroupId);
            var homeworksDtos = homeworks.Select(h => h.ToGetHomeworkWithSubjectTitleRequestDtoFromHomeworkModel()).ToList();

            return new GetSheduleDayRequestDto {
                    SubjectDtos = sheduleDaySubjectsDto,
                    HomeworksDto = homeworksDtos,
                    dayOfWeek = daysOfWeeks[sheduleDayModel.CountDay],
                    numOfWeek = sheduleDayModel.CountWeek,
                    };
        }
        private (byte, byte) GetNextWeekAndDay()
        {
            int totalWeeks = (DateTime.Now.DayOfYear - 1) / 7 + 1;
            byte numberOfWeeks = (totalWeeks % 2) == 0 ? (byte)2 : (byte)1;
            byte dayOfWeek = (byte)DateTime.Now.DayOfWeek;
            dayOfWeek = (dayOfWeek == 0) ? (byte)6 : dayOfWeek--;
            return (numberOfWeeks, dayOfWeek);
        }
    }
}
