using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api_project.Dtos.CharacterSkill;
using web_api_project.Services.CharacterSkillService;

namespace web_api_project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterSkillController : ControllerBase
    {
      private readonly ICharacterSkillService _characterSkillService;
      public CharacterSkillController(ICharacterSkillService characterSkillService)
      {
          _characterSkillService = characterSkillService;
      }
        
        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill) 
        {
            return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
        }
    }
}