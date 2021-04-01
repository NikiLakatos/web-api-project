using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repos.Context;
using Repos.Dtos.Character;
using Repos.Modals;
using web_api_project.Dtos.CharacterSkill;
using web_api_project.Modals;

namespace web_api_project.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterSkillService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _dataContext.Characters
                        .Include(c => c.Weapon)
                        .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                        .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId 
                        && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character == null) {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response; 
                }
                Skill skill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if(skill == null) {
                     response.Success = false;
                    response.Message = "Skiil not found.";
                    return response; 
                }

                CharacterSkill characterSkill = new CharacterSkill {
                    Character = character,
                    Skill = skill,
                };

                await _dataContext.CharacterSkills.AddAsync(characterSkill);
                await _dataContext.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception e) 
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}