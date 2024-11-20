using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.Common.Exceptions;
using NET3Assignment.DTOs;
using NET3Assignment.Models;

namespace NET3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private static List<Teacher> _teacherList = new List<Teacher>();
        private static List<User> _userList = new List<User>();
        private static List<Department> _departmentList = new List<Department>();
        public TeacherController(IMapper mapper)
        {
            _mapper = mapper;
            // create user list sample, one-to-one with teacher
            _userList.Add(new User
            {
                UserId = Guid.NewGuid(),
                UserName = "John Doe",
                Email = "john.doe@example.com",
                HashedPassword = "hashed password example01",
                Role = Common.Enums.RoleEnum.Teacher,
                Gender = Common.Enums.GenderEnum.Female,
            });

            _userList.Add(new User
            {
                UserId = Guid.NewGuid(),
                UserName = "Jane Smith",
                Email = "jane.smith@example.com",
                HashedPassword = "hashed password example02",
                Role = Common.Enums.RoleEnum.Teacher,
                Gender = Common.Enums.GenderEnum.Male,
            });

            // create department list sample
            _departmentList.Add(new Department
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Mathematics",
                DepartmentDescription = "Department of Mathematics"
            });

            _departmentList.Add(new Department
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Physics",
                DepartmentDescription = "Department of Physics"
            });

            // create teacher list sample
            _teacherList.Add(new Teacher
            {
                TeacherId = Guid.NewGuid(),
                UserId = _userList[0].UserId,//foreign key
                DepartmentId = _departmentList[0].DepartmentId,//foreign key
                User = _userList[0],//navigation property
                Department = _departmentList[0],//navigation property
                Description = "Experienced mathematics teacher.",
                Specialty = "Algebra and Calculus"
            });

            _teacherList.Add(new Teacher
            {
                TeacherId = Guid.NewGuid(),
                UserId = _userList[1].UserId,
                DepartmentId = _departmentList[1].DepartmentId,
                User = _userList[1],
                Department = _departmentList[1],
                Description = "Expert in Physics and Quantum Mechanics.",
                Specialty = "Quantum Physics"
            });
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetTeacherById(Guid id)
        {
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.UserId == id);
            if (teacherFound == null)
            {
                throw new A4NotFoundException("teacher not found.");
            }   
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return Ok(teacherResponse);
        }

        [HttpGet]
        public IActionResult GetTeacher([FromQuery] string? email)
        {
            // get all teachers
            if (string.IsNullOrEmpty(email))
            {
                var teacherList = _mapper.Map<List<TeacherResponseDTO>>(_teacherList);
                return Ok(teacherList);
            }
            
            // get teacher by email
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.User.Email == email);
            if (teacherFound == null)
            {
                throw new A4NotFoundException("teacher not found.");
            }
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return Ok(teacherResponse);
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] TeacherRequestDTO teacherInfo)
        {
            Guid userId = teacherInfo.UserId;
            Guid departmentId = teacherInfo.DepartmentId;
            // search
            var userFound = _userList.FirstOrDefault(user => user.UserId == userId);
            if (userFound == null)
            {
                throw new A4NotFoundException("user not found.");
            }
            var departmentFound = _departmentList.FirstOrDefault(department => department.DepartmentId == departmentId);
            if (departmentFound == null)
            {
                throw new A4NotFoundException("department not found.");
            }
            // create
            Teacher newTeacher = _mapper.Map<Teacher>(teacherInfo);
            // some navigation properties
            newTeacher.TeacherId = Guid.NewGuid();
            newTeacher.User = userFound;
            newTeacher.Department = departmentFound;
            // update db
            _teacherList.Add(newTeacher);
            // convert to repsonse
            TeacherResponseDTO teacherResponseDTO = _mapper.Map<TeacherResponseDTO>(newTeacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = newTeacher.TeacherId }, teacherResponseDTO);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateTeacherInfoById([FromBody] TeacherRequestDTO teacherInfo)
        {
            //search
            var userFound = _userList.FirstOrDefault(user => user.UserId == teacherInfo.UserId);
            if (userFound == null)
            {
                throw new A4NotFoundException("user not found.");
            }
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.UserId == teacherInfo.UserId);
            if (teacherFound == null)
            {
                throw new A4NotFoundException("teacher not found.");
            }
            // update
            _mapper.Map(teacherInfo, teacherFound);
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return Ok(teacherResponse);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteTeacherById(Guid id)
        {
            //search
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null)
            {
                throw new A4NotFoundException("user not found.");
            }
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.UserId == id);
            if (teacherFound == null)
            {
                throw new A4NotFoundException("teacher not found.");
            }
            //delete
            _userList.Remove(userFound);
            _teacherList.Remove(teacherFound);

            return NoContent();
        }
    }
}
