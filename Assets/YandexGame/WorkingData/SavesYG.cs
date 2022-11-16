
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;
        public bool sound;

        // Ваши сохранения
        public int money = 1;
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        //
        public int shootWeaponLvl = 0; 
        public int spinWeaponLvl = 0; 
        public int areaWeaponLvl = 0; 
    }
}
