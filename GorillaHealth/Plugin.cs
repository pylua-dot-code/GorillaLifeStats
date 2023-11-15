using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace GorillaLifeStats
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.10")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public int Health;
        public int Stamina;
        bool inRoom;

        public void DamageManager(int AddRemoveSet,  int Value, bool GetHealth)
        {
            if (AddRemoveSet == 0)
            {
                Health += Value;
            }
            if (AddRemoveSet == 1)
            {
                Health -= Value;
            }
            if (AddRemoveSet == 2)
            {
                Health = Value;
            }
            if (GetHealth)
            {
                Logger.LogInfo("GorillaLifeStats Health is " + Health);
            }
        }
        public void StaminaManager(int AddRemoveSet, int Value, bool GetStamina)
        {
            if (AddRemoveSet == 0)
            {
                Stamina += Value;
            }
            if (AddRemoveSet == 1)
            {
                Stamina -= Value;
            }
            if (AddRemoveSet == 2)
            {
                Stamina = Value;
            }
            if (GetStamina)
            {
                Logger.LogInfo("GorrilaLifeStats Stamina is " + Stamina);
            }
        }
    }
}
