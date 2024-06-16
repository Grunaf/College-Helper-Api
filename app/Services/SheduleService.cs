using app.Dtos.SheduleDay;
using app.Dtos.Subject;
using app.Interfaces;
using app.Interfaces.Shedule;
using app.Models;

namespace app.Services
{
    public class SheduleService : ISheduleService
    {
        private readonly ISheduleRepository _sheduleRepo;
        private readonly ISubjectRepository _subjectRepo;
        private readonly IStudentGroupSubjectRepository _groupSubjectRepo;
        private readonly IStudentGroupSubjectService _groupSubjectService;
        private readonly IStudentGroupService _groupService;
        public SheduleService(ISheduleRepository sheduleRepository, ISubjectRepository subjectRepository,
                                IStudentGroupSubjectRepository groupSubjectRepository, IStudentGroupSubjectService groupSubjectService, IStudentGroupService studentGroupService)
        {
            _sheduleRepo = sheduleRepository;
            _subjectRepo = subjectRepository;
            _groupSubjectRepo = groupSubjectRepository;
            _groupSubjectService = groupSubjectService;
            _groupService = studentGroupService;
        }

        public async Task<CreateSheduleRequestDto> CreateSheduleAsync(long headBoyChatId, CreateSheduleRequestDto sheduleRequestDto)
        {
            var studentGroupModel = await _groupService.GetGroupByHeadBoyChatIdAsync(headBoyChatId);

            await _sheduleRepo.DeleteSheduleIfExistsByStudentGroupIdAsync(studentGroupModel.Id);

            Dictionary<string, Subject> subjectCache = [];

            for (byte w = 0; w < sheduleRequestDto.Weeks.Length; w++) //Неделя
            {
                var week = sheduleRequestDto.Weeks[w];

                for (byte d = 0; d < week.Days.Count; d++) //Дни
                {
                    var day = week.Days[d];
                    List<SheduleDaySubject> subjects = await ProcessPairsInDayAsync(day.PairsInDay, studentGroupModel.Id, subjectCache);
                    var sheduleDayModel = CreateSheduleDayModel(studentGroupModel, w, d, subjects);
                    await _sheduleRepo.CreateSheduleForDayAsync(sheduleDayModel);
                }
            }
            await _groupSubjectService.SyncGroupSubjectsFromScheduleAsync(studentGroupModel.Id, subjectCache);

            return sheduleRequestDto;
        }

        private async Task<List<SheduleDaySubject>> ProcessPairsInDayAsync(string pairsInDay, int studentGroupId, Dictionary<string, Subject> subjectCache)
        {
            const string dividerPair = ", ";
            byte spot = 1;
            string subgroup = null;

            var pairs = pairsInDay.Split(dividerPair);
            List<SheduleDaySubject> sheduleDaySubjects = [];

            foreach (var pair in pairs) //Пары
            {
                string subject = RemoveSubgroupInfo(pair);

                int index = pair.IndexOf('#');

                if (index != -1)
                {
                    subgroup = pair.Substring(index + 1).Trim();
                }

                if (subject != "-") // Если не окно
                {
                    Subject subjectModel = await EnsureSubjectExistsAndAssignToGroupAsync(subject, subjectCache);

                    sheduleDaySubjects.Add(new SheduleDaySubject
                    {
                        SubjectId = subjectModel.Id,
                        SubgroupSequence = subgroup,
                        Spot = spot
                    });
                }
                spot++;
            }
            return sheduleDaySubjects;
        }
        static string RemoveSubgroupInfo(string input)
        {
            string[] subgroups = { " #1", " #2", " #*" };

            foreach (var subgroup in subgroups)
            {
                input = input.Replace(subgroup, "");
            }

            return input.Trim();
        }


        private async Task<Subject> EnsureSubjectExistsAndAssignToGroupAsync(string subjectTitle, Dictionary<string, Subject> subjectCache)
        {
            if (!subjectCache.ContainsKey(subjectTitle))
            {
                Subject? subjectModel = await _subjectRepo.GetByNameAsync(subjectTitle);
                subjectModel ??= await _subjectRepo.CreateAsync(new Subject { Title = subjectTitle });
                subjectCache.Add(subjectTitle, subjectModel);
            }

            return subjectCache[subjectTitle];
        }

        private SheduleDay CreateSheduleDayModel(StudentGroup groupModel, byte week, byte day, List<SheduleDaySubject> subjects)
        {
            return new SheduleDay
            {
                StudentGroup = groupModel,
                CountWeek = ++week,
                CountDay = day,
                SheduleDaySubjects = subjects,
            };
        }

        public async Task<GetSheduleDayRequestDto> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId)
        {
            string[] daysOfWeeks = ["Понедельник", "Вторник", "Cреда", "Четверг", "Пятница", "Суббота", "Воскресенье"];

            var studentGroupModel = await _groupService.GetGroupByHeadBoyChatIdAsync(studentChatId);

            int totalWeeks = (DateTime.Now.DayOfYear - 1) / 7 + 1;
            byte numberOfWeeks = (totalWeeks % 2) == 0 ? (byte)2 : (byte)1;

            byte dayOfWeek = (byte)DateTime.Now.DayOfWeek;
            dayOfWeek = (dayOfWeek == 0) ? (byte)6 : dayOfWeek--;

            var sheduleDayModel = await _sheduleRepo.GetNextSheduleDayByStudentChatIdAsync(studentGroupModel.Id, numberOfWeeks, dayOfWeek);
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
                    SubgroupSequence = subject.SubgroupSequence,
                    Spot = subject.Spot
                });
                spot++;
            }
            return new GetSheduleDayRequestDto {
                    SubjectDtos = sheduleDaySubjectsDto,
                    dayOfWeek = daysOfWeeks[sheduleDayModel.CountDay],
                    numOfWeek = sheduleDayModel.CountWeek,
                    };
        }
    }
}
