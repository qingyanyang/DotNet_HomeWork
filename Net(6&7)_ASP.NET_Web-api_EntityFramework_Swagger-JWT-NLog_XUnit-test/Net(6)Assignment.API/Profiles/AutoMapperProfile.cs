using AutoMapper;
using Net_6_Assignment.DTOs;
using Net_6_Assignment.Models;
namespace Net_6_Assignment.Profiles
{
    public class AutoMapperProfile:Profile
    {
        // from * to *
        public AutoMapperProfile() {
            // User
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender !=null? src.Gender.ToString().ToLowerInvariant() : null))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses != null? src.Courses.Select(c=>c.CourseName) : null));

            CreateMap<UserLoginRequestDTO, User>()
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));


            CreateMap<UserUpdateRequestDTO, User>();

            // Category
            CreateMap<CategoryRequestDTO, Category>();

            CreateMap<CategoryUpdateRequestDTO, Category>();

            CreateMap<Category, CategoryResponseDTO>()
                .ForMember(dest => dest.CategoryLevel, opt => opt.MapFrom(src => src.CategoryLevel.ToString().ToLowerInvariant()))
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.Parent != null? src.Parent.CategoryName:null))
                .ForMember(dest => dest.CourseNames, opt => opt.MapFrom(src => src.Courses != null ? src.Courses.Select(course=>course.CourseName).ToList():null)); 

            // Course
            CreateMap<CourseRequestDTO, Course>();

            CreateMap<Course, CourseResponseDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}
