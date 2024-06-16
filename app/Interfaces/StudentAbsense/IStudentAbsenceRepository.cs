﻿using app.Dtos.StudentAbsence;
using app.Models;

namespace app.Interfaces.StudentAbsense
{
    public interface IStudentAbsenceRepository
    {
        public Task<List<StudentAbsence>> GetAllByStudentIdAsync(long studentId);
        public Task<List<StudentAbsence>> GetStatStudentAbsencesByStudentGroupIdAsync(int studentGroupId);
        public Task<List<StudentAbsence>> GetAllByStudentGroupIdAsync(int studentGroupId, DateTime date, byte lessonNumber);
        public Task<StudentAbsence> DeleteByIdAsync(int id);
        public Task<StudentAbsence> CreateAsync(StudentAbsence studentAttendanceModel);
        //public Task<StudentAttendance?> UpdateAsync(int id, StudentAttendanceUpdateRequestDto studentAttendanceDto);
    }
}
