using app.Dtos.SheduleDay;
using app.Dtos.Subject;
using app.Interfaces;
using app.Migrations;
using app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace app.Services
{
    public class SheduleService : ISheduleService
    {
        private readonly IStudentGroupRepository _groupRepo;
        private readonly ISheduleRepository _sheduleRepo;
        private readonly ISubjectRepository _subjectRepo;
        private readonly IStudentRepository _studentRepo;
        public SheduleService(IStudentGroupRepository groupRepository, ISheduleRepository sheduleRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository)
        {
            _groupRepo = groupRepository;
            _sheduleRepo = sheduleRepository;
            _subjectRepo = subjectRepository;
            _studentRepo = studentRepository;
        }

        
        public async Task<CreateSheduleRequestDto> CreateSheduleAsync(long headBoyChatId, CreateSheduleRequestDto sheduleRequestDto)
        {
            CreateSheduleRequestDto schedule = new CreateSheduleRequestDto
            {
                Weeks = new WeekDto[2]
            };
            try
            {
                var groupModel = await _groupRepo.GetByHeadBoyChatIdAsync(headBoyChatId);

                for (byte w = 0; w < sheduleRequestDto.Weeks.Length; w++) //Неделя
                {
                    var week = sheduleRequestDto.Weeks[w];

                    WeekDto weekDto = new()
                    {
                        Days = []
                    };
                    for (byte d = 0; d < week.Days.Count; d++) //Дни
                    {
                        var day = week.Days[d];
                        List<SheduleDaySubject> subjects = await ProcessPairsInDay(day.PairsInDay);
                        var sheduleDayModel = CreateSheduleDayModel(groupModel, w, d, subjects);
                        await _sheduleRepo.CreateSheduleForDayAsync(sheduleDayModel);
                        DayDto dayDto = new()
                        {
                            PairsInDay = day.PairsInDay,
                        };

                        weekDto.Days.Add(dayDto);
                    }

                    schedule.Weeks[w] = weekDto;
                }

                return schedule;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private async Task<List<SheduleDaySubject>> ProcessPairsInDay(string pairsInDay)
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
                    Subject subjectModel = await GetOrCreateSubject(subject);
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
        private async Task<Subject> GetOrCreateSubject(string pair)
        {
            Subject? subjectModel = await _subjectRepo.GetByNameAsync(pair);
            subjectModel ??= await _subjectRepo.CreateAsync(new Subject { Title = pair });
            return subjectModel;
        }

        private SheduleDay CreateSheduleDayModel(StudentGroup groupModel, byte week, byte day, List<SheduleDaySubject> subjects)
        {
            return new SheduleDay
            {
                StudentGroup = groupModel,
                CountWeek = ++week,
                CountDay = ++day,
                SheduleDaySubjects = subjects,
            };
        }

        public async Task<List<SheduleDaySubjectDto>> GetTommorowSheduleDayByStudentChatIdAsync(long studentChatId)
        {
            try
            {
                var studentModel = await _studentRepo.GetByChatIdAsync(studentChatId);
                var studentGroup = await _groupRepo.GetByIdAsync(studentModel.StudentGroupId);

                int totalWeeks = (DateTime.Now.DayOfYear - 1) / 7 + 1;
                byte numberOfWeeks = (totalWeeks % 2) == 0 ? (byte)2 : (byte)1;

                byte dayOfWeek = (byte)DateTime.Now.DayOfWeek;
                dayOfWeek = (dayOfWeek == 0) ? (byte)7 : dayOfWeek;

                var sheduleDaySubjects = await _sheduleRepo.GetNextSheduleDayByStudentChatIdAsync(studentGroup.Id, numberOfWeeks, dayOfWeek);

                List<SheduleDaySubjectDto?> sheduleDaySubjectsDto = [];
                byte spot = 1;
                foreach (var subject in sheduleDaySubjects)
                {
                    if (subject.Spot != spot)
                    {
                        sheduleDaySubjectsDto.Add(null);
                    }
                    sheduleDaySubjectsDto.Add(new SheduleDaySubjectDto
                    {
                        SubjectId = subject.SubjectId,
                        Title = subject.Subject.Title,
                        SubgroupSequence = subject.SubgroupSequence,
                        Spot = subject.Spot
                    });
                    spot++;
                }
                return sheduleDaySubjectsDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
