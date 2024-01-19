using AutoMapper;
using Model;
using Modell.Model;

namespace DTO;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
        CreateMap<UserModelDTO,UserModel>();
        CreateMap<ContactModelDTO, ContactModel>();
        CreateMap<GroupModelDTO, GroupModel>();
        CreateMap<PasswordModelDTO, PasswordModel>();
        CreateMap<RankModelDTO, RankModel>();
        CreateMap<RoleModelDTO, RoleModel>();
        CreateMap<StatusModelDTO, StatusModel>();
    }

}

