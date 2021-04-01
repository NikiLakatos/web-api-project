using System.Collections.Generic;

namespace web_api_project.Modals
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<CharacterSkill> CharacterSkills {get; set;}
    }
}