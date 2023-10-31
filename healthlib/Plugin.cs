using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace healthlib
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public int Health;
        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            Debug.Log("HealthLib: Thanks for using HealthLib - pylua.code");
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {

        }

        void Update()
        {

        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

       
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
