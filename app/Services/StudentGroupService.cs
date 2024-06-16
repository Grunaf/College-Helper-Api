using app.Interfaces;
using app.Models;
using System.Text.RegularExpressions;

namespace app.Services
{
    public class StudentGroupService(IStudentGroupRepository studentGroupRepository, IStudentRepository studentRepository) : IStudentGroupService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        private readonly IStudentGroupRepository _groupRepository = studentGroupRepository;

        public async Task<StudentGroup> GetGroupByHeadBoyChatIdAsync(long headBoyChatId)
        {
            var headBoy = await _studentRepository.GetByChatIdAsync(headBoyChatId);

            if (headBoy != null && headBoy.IsHeadBoy)
            {
                var groupModel = await _groupRepository.GetByIdAsync(headBoy.StudentGroupId);
                return groupModel;
            }

            throw new InvalidOperationException("Пользователь не является старостой");
        }
    }
}
