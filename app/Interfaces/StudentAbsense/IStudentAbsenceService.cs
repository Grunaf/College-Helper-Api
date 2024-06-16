﻿using app.Dtos.StudentAbsence;
using app.Models;
using app.Services;

namespace app.Interfaces.StudentAbsense
{
    public interface IStudentAbsenceService
    {
        public Task<ResultCreateOrDelete> CreateOrDeleteAsync(CreateOrDeleteStudentAbsenceRequestDto absenceDto);
        public Task<List<StatStudentAbsenceRequestDto>> GetStatStudentAbsensesByHeadBoyChatIdAsync(long headBoyChatId);
    }
}