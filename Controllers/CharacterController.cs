using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Dtos.Character;
using Repos.Modals;
using Repos.Services.CharacterService;

namespace Repos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _repoCharacter;
        public CharacterController(ICharacterService repoCharacter)
        {
            _repoCharacter = repoCharacter;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repoCharacter.GetAllCharacter());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _repoCharacter.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _repoCharacter.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> response = await _repoCharacter.UpdateCharacter(updateCharacter);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _repoCharacter.DeleteCharacter(id);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}