using System.Threading.Tasks;
using Repos.Dtos.Character;
using Repos.Modals;
using web_api_project.Dtos.Weapon;

namespace web_api_project.Services.WeaponService
{
    public interface IWeaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}