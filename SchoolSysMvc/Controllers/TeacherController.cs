using Microsoft.AspNetCore.Mvc;
using SchoolSysMvc.Interfaces;
using SchoolSysMvc.Services;

namespace SchoolSysMvc.Controllers
{
    public class TeacherController : Controller
    {
        
            private readonly ITeacherService _teacherService;

            public TeacherController(ITeacherService teacherService)
            {
                _teacherService = teacherService;
            }

            public async Task<IActionResult> Index()
            {
                var teachers = await _teacherService.GetAllTeachersAsync();
                return View(teachers);
            }

            public async Task<IActionResult> Details(int id)
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                if (teacher == null)
                    return NotFound();

                return View(teacher);
            }
        
    }
}
