using System;
using System.Collections.Generic;
using web_api_project.Modals;

namespace Repos.Modals
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Alex";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User User { get; set; }
        public Weapon Weapon { get; set; }
        public List<CharacterSkill> CharacterSkills {get; set;}

    }
}