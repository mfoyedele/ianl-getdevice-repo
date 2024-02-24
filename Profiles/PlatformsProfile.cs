using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
     
            CreateMap<PlatformCreateDto, Platform>();
                    }
    }
}