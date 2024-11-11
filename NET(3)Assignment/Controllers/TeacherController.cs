using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.DTOs;
using NET3Assignment.Models;

namespace NET3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController: ControllerBase
    {
        private readonly IMapper _mapper;
        private static List<Teacher> _teacherList = new List<Teacher>();
        private static List<User> _userList = new List<User>();
        private static List<Department> _departmentList = new List<Department>();
        public TeacherController(IMapper mapper) {
            _mapper = mapper;
            // create user list sample, one-to-one with teacher
            _userList.Add(new User
            {
                UserId = Guid.NewGuid(),
                UserName = "John Doe",
                Email = "john.doe@example.com",
                HashedPassword = "hashed password example01",
                Role = 0,
            });

            _userList.Add(new User
            {
                UserId = Guid.NewGuid(),
                UserName = "Jane Smith",
                Email = "jane.smith@example.com",
                HashedPassword = "hashed password example02",
                Role = 0,
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

        [HttpGet]
        public JsonResult GetTeacherInfo()
        {
            var teacherList = _mapper.Map<List<TeacherResponseDTO>>(_teacherList);
            return new JsonResult(teacherList);
        }

        [HttpGet("{id:guid}")]
        public JsonResult GetTeacherInfoById(Guid id)
        {
            var teacherFound = _teacherList.FirstOrDefault(teacher=>teacher.UserId==id);
            if(teacherFound == null) return new JsonResult("Teacher not found!") { StatusCode = 404 };
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return new JsonResult(teacherResponse);
        }

        [HttpGet]
        public JsonResult GetTeacherInfoByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new JsonResult("Email parameter is required") { StatusCode = 400 };
            }
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.User.Email == email);
            if (teacherFound == null) return new JsonResult("Teacher not found!") { StatusCode = 404 };
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return new JsonResult(teacherResponse);
        }

        [HttpPost]
        public JsonResult CreateTeacher([FromBody] TeacherRequestDTO teacherInfo)
        {
            Guid userId = teacherInfo.UserId;
            Guid departmentId = teacherInfo.DepartmentId;
            // search
            var userFound = _userList.FirstOrDefault(user => user.UserId == userId);
            if(userFound == null) return new JsonResult("User not found!") { StatusCode = 404 };
            var departmentFound = _departmentList.FirstOrDefault(department => department.DepartmentId == departmentId);
            if (departmentFound == null) return new JsonResult("Department not found!") { StatusCode = 404 };
            // create
            Teacher newTeacher = _mapper.Map<Teacher>(teacherInfo);
            // some navigation properties
            newTeacher.TeacherId = Guid.NewGuid();
            newTeacher.User = userFound;
            newTeacher.Department = departmentFound;
            // update db
            _teacherList.Add(newTeacher);
            // convert to repsonse
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(newTeacher);
            return new JsonResult(teacherResponse) { StatusCode = 201 };
        }

        [HttpPut("{id:guid}")]
        public JsonResult UpdateTeacherInfoById([FromBody] TeacherRequestDTO teacherInfo)
        {
            //search
            var userFound = _userList.FirstOrDefault(user=>user.UserId == teacherInfo.UserId);
            if (userFound == null) return new JsonResult("User not found") { StatusCode = 404 };
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.UserId == teacherInfo.UserId);
            if (teacherFound == null) return new JsonResult("Teacher not found") { StatusCode = 404 };
            // update
            _mapper.Map(teacherInfo, teacherFound);
            TeacherResponseDTO teacherResponse = _mapper.Map<TeacherResponseDTO>(teacherFound);
            return new JsonResult(teacherResponse);
        }

        [HttpDelete("{id:guid}")]
        public JsonResult DeleteTeacherById(Guid id)
        {
            //search
            var userFound = _userList.FirstOrDefault(user => user.UserId == id);
            if (userFound == null) return new JsonResult("User not found") { StatusCode = 404 };
            var teacherFound = _teacherList.FirstOrDefault(teacher => teacher.UserId == id);
            if (teacherFound == null) return new JsonResult("Teacher not found") { StatusCode = 404 };
            //delete
            _userList.Remove(userFound);
            _teacherList.Remove(teacherFound);

            return new JsonResult(null) { StatusCode = 204 };
        }
    }
}
