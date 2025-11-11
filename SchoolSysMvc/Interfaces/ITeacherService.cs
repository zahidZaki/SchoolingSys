using SchoolSysMvc.Dto;

namespace SchoolSysMvc.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto?> GetTeacherByIdAsync(int id);
    }
}
