
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = null;
       // public bool feedbackDone;
        public bool promptDone;
        public bool sound;

        // Ваши сохранения
      //  public string newPlayerName = "Hello!";
      //  public bool[] openLevels = new bool[3];
        //
        public Dictionary<string, int> shootWeapon = new Dictionary<string, int>(2) { ["shootDamage"] = 0, ["shootRange"] = 0 };
        public int gold = 100;
    }
}
