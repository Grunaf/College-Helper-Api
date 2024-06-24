using app.Dtos.StudentGroup;
using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupRepository
    {
        Task<List<StudentGroup>> GetAllAsync();
        Task<StudentGroup?> GetByIdAsync(int id);
        Task<StudentGroup?> DeleteAsync(int id);
        Task<StudentGroup> UpdateAsync(int id, UpdateStudentGroupRequestDto studentGroupDto);
        Task<bool> CheckIfStudentsExceptHeadBoyExistInGroupAsync(int studentGroupId);
        Task<StudentGroup> CreateAsync(StudentGroup studenModel);
    }
}
