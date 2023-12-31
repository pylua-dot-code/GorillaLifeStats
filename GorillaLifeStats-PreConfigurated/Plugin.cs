﻿using BepInEx;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilla;

namespace GLSP
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.10")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public int BaseHealth = 100;
        public int Health = 100;
        public int BaseStamina = 1000;
        public int Stamina = 1000;
        bool inRoom;

        public void DamageManager(int Function,  int Value, bool GetHealth)
        {
            if (Function == 0)
            {
                Health += Value;
            }
            if (Function == 1)
            {
                Health -= Value;
            }
            if (Function == 2)
            {
                Health = Value;
            }
            if (GetHealth)
            {
                Logger.LogInfo("GorillaLifeStats Health is " + Health);
            }
        }
        public void StaminaManager(int Function, int Value, bool GetStamina)
        {
            if (Function == 0)
            {
                Stamina += Value;
            }
            if (Function == 1)
            {
                Stamina -= Value;
            }
            if (Function == 2)
            {
                Stamina = Value;
            }
            if (GetStamina)
            {
                Logger.LogInfo("GorrilaLifeStats Stamina is " + Stamina);
            }
        }

        void Update()
        {
            if (Health > BaseHealth)
            {
                Health = BaseHealth;
                Logger.LogWarning("Health was larger than BaseHealth. Setting to BaseHealth");
            }
            if (Health < 0)
            {
                Health = 0;
                Logger.LogWarning("Health was smaller than 0, Setting Health to 0");
            }
            if (Stamina > BaseStamina)
            {
                Stamina = BaseStamina;
                Logger.LogWarning("Stamina was larger than BaseStamina. Setting to BaseStamina");
            }
            if (Health < 0)
            {
                Health = 0;
                Logger.LogWarning("Stamina was smaller than 0, Setting Stamina to 0");
            }
        }
    }
}
