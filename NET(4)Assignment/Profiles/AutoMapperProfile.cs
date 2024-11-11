using AutoMapper;
using NET3Assignment.DTOs;
using NET3Assignment.Models;
using System;

namespace NET3Assignment.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.HasValue ? src.Gender.ToString().ToLowerInvariant() : null))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString().ToLowerInvariant()));

            CreateMap<UserRequestDTO, User>()
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));

            CreateMap<Teacher, TeacherResponseDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender.HasValue ? src.User.Gender.ToString().ToLowerInvariant() : null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.ToString().ToLowerInvariant()))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

            CreateMap<TeacherRequestDTO, Teacher>();
        }
    }
}
