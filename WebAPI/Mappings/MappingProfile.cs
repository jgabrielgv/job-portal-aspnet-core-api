using System;
using AutoMapper;
using WebAPI.Core.Domain;
using WebAPI.DTOs;

namespace WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();

            CreateMap<Application, ApplicationDTO>();
            CreateMap<ApplicationDTO, Application>();

            CreateMap<ApplicationDTO, Candidate>();
        }
    }
}