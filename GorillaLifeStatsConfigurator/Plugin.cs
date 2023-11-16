using BepInEx;
using System;
using UnityEngine;
using Utilla;
using GorillaLifeStats;
using BepInEx.Configuration;
namespace GorillaLifeStatsConfigurator
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInDependency("com.pyluadotcode.gorillatag.gorillahealth", "1.0.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {

        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        private ConfigEntry<int> configHealth;
        private ConfigEntry<bool> configStamina;

        void OnGameInitialized(object sender, EventArgs e)
        {
            configHealth = Config.Bind("Health", "BaseHealthSet", 100, "The base health");
            configHealth = Config.Bind("Stamina", "BaseStaminaSet", 1000, "The base stamina");

        }

        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
