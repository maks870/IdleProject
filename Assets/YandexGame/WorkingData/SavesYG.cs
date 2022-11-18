
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
       // public bool feedbackDone;
        public bool promptDone;
        public bool sound;

        // Ваши сохранения
      //  public string newPlayerName = "Hello!";
      //  public bool[] openLevels = new bool[3];
        //
        public Dictionary<string, int> areaWeapon = new Dictionary<string, int>(2) { ["areaDamage"] = 0, ["areaSlow"] = 0 };
        public Dictionary<string, int> shootWeapon = new Dictionary<string, int>(2) { ["shootDamage"] = 0, ["shootRange"] = 0 };
        public Dictionary<string, int> spinWeapon = new Dictionary<string, int>(2) { ["spinDamage"] = 0, ["spinRadius"] = 0 };
        public int gold = 100;
    }
}
