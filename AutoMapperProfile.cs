using System.Linq;
using AutoMapper;
using Repos.Dtos.Character;
using Repos.Modals;
using web_api_project.Dtos.Skill;
using web_api_project.Dtos.Weapon;
using web_api_project.Modals;

namespace Repos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>()
                .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill,GetSkillDto>();
        }
    }
}