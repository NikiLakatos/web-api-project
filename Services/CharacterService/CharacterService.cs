using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repos.Context;
using Repos.Dtos.Character;
using Repos.Modals;

namespace Repos.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId () => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _dataContext.AddAsync(character);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = (_dataContext.Characters.Where(u => u.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbChararacter = await _dataContext.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbChararacter.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(); ;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(s => s.Id == id && s.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _dataContext.Characters.FirstOrDefaultAsync(i => i.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.Defense = updateCharacter.Defense;
                character.HitPoints = updateCharacter.HitPoints;
                character.Intelligence = updateCharacter.Intelligence;
                character.Strength = updateCharacter.Strength;

                _dataContext.Update(character);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _dataContext.Characters.FirstOrDefaultAsync(i => i.Id == id && i.User.Id == GetUserId());
                if(character != null){
                    _dataContext.Remove(character);
                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = (_dataContext.Characters.Where(c => c.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                }
                else 
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
                
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }
    }
}