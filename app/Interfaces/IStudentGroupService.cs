using app.Models;

namespace app.Interfaces
{
    public interface IStudentGroupService
    {
        public Task<StudentGroup> GetGroupByHeadBoyChatIdAsync(long headBoyChatId);
    }
}
