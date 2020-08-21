using AutoMapper;
using IL.DTO.Core.CommonDTO;
using IL.DTO.Core.PODTO;
using IL.DTO.Core.UserDTO;
using IM.Data.Core;

namespace Inventory.App_Code
{
    public class AutoMapperBootstrap : Profile
    {
        public AutoMapperBootstrap()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<outlet, CommonOutletDTO>();
            CreateMap<UserRole, CommonDTO>().ForMember(src => src.NormalizeName, a => a.MapFrom(des => des.name));
            CreateMap<movementType, CommonDTO>();
            CreateMap<unit, CommonDTO>();
            CreateMap<Status, CommonDTO>().ForMember(src => src.NormalizeName, a => a.MapFrom(des => des.name));
            CreateMap<PORequestDTO, prRequest>();
        }
    }
}