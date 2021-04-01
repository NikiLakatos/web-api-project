using System.Threading.Tasks;
using Repos.Dtos.Character;
using Repos.Modals;
using web_api_project.Dtos.CharacterSkill;

namespace web_api_project.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
         Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}