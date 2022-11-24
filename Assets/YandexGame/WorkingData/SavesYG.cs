using System;
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public int recordScore = 0;
       // public bool feedbackDone;
        public bool promptDone;
        public bool sound = true;

        public Dictionary<string, int> areaWeapon = new Dictionary<string, int>(2) { ["areaDamage"] = 0, ["areaSlow"] = 0 };
        public Dictionary<string, int> shootWeapon = new Dictionary<string, int>(2) { ["shootDamage"] = 0, ["shootRange"] = 0 };
        public Dictionary<string, int> spinWeapon = new Dictionary<string, int>(2) { ["spinDamage"] = 0, ["spinRadius"] = 0 };
        public Dictionary<string, int> explosiveWeapon = new Dictionary<string, int>(2) { ["explodeDamage"] = 0, ["explodeRadius"] = 0 };
        public Dictionary<string, int> slayWeapon = new Dictionary<string, int>(2) { ["slayDamage"] = 0, ["slaySpeed"] = 0 };
        public Dictionary<string, int> playerSkill = new Dictionary<string, int>(2) { ["health"] = 0, ["speed"] = 0 };
        public Dictionary<string, int> inventory = new Dictionary<string, int>(2) { ["slots"] = 0, ["awards"] = 0 };
        public int gold = 0;
    }
}
