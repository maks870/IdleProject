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
        public bool promptDone = true;
        public bool sound = true;

        public DataObject areaWeapon = new DataObject(new List<DataValue>() { new DataValue("areaDamage", 0), new DataValue("areaSlow", 0) });
        public DataObject shootWeapon = new DataObject(new List<DataValue>() { new DataValue("shootDamage", 0), new DataValue("shootRange", 0) });
        public DataObject spinWeapon = new DataObject(new List<DataValue>() { new DataValue("spinDamage", 0), new DataValue("spinRadius", 0) });
        public DataObject explosiveWeapon = new DataObject(new List<DataValue>() { new DataValue("explodeDamage", 0), new DataValue("explodeRadius", 0) });
        public DataObject slayWeapon = new DataObject(new List<DataValue>() { new DataValue("slayDamage", 0), new DataValue("slaySpeed", 0) });
        public DataObject playerSkill = new DataObject(new List<DataValue>() { new DataValue("health", 0), new DataValue("speed", 0) });
        public DataObject inventory = new DataObject(new List<DataValue>() { new DataValue("slots", 0), new DataValue("awards", 0) });
        public DataObject collector = new DataObject(new List<DataValue>() { new DataValue("collectorRadius", 0), new DataValue("experienceMultiplier", 0) });
        public int gold = 0;
    }
}
