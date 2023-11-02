using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.IO;
using UnityEngine;
using Utilla;
using HoneyLib;
using HoneyLib.Events;

namespace healthlib
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public int Health = 100;

        bool inRoom;

        void Start()
        {
            HoneyLib.Events.Events.InfectionTagEvent += OnTag;
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        
        int getHealth() => return Health;

        void OnEnable() => HarmonyPatches.ApplyHarmonyPatches();
        void OnDisable() => HarmonyPatches.RemoveHarmonyPatches();

        // BepInEx provides a logger property in the BaseUnityPlugin
        // doing this automatically adds the plugin name and also appropriately represents severity
        // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/3_logging.html
        void OnGameInitialized(object sender, EventArgs e)
            Logger.LogInfo("Thanks for using HealthLib - pylua.code");

        void OnTag(object sender, InfectionTagEventArgs e)
        {
            if (e.taggedPlayer.IsLocal && e.taggingPlayer != null)
                removeHealth(10);
        }

        void Update()
        {
            if (Health <= 0)
            {
                Photon.Pun.PhotonNetwork.Disconnect();
                Logger.LogInfo("Kicked from room");
            }
        }

        void addHealth(int healthadded)
        {
            if (Health < 100)
            {
                Logger.LogInfo($"Health added: {healthadded}");
                Health = Health + healthadded;
            }
            else
                Logger.LogWarning("Already at max health");
        }

        void removeHealth(int healthremoved)
        {
            if (Health > 0)
            {
                Logger.LogInfo($"Health removed: {healthremoved}");
                Health = Health - healthremoved;
            }
            else
                Logger.LogWarning("Already at min health, player should be dead");
        }

        void setHealth(int healthSet)
        {
            Debug.LogInfo($"Health set: {healthSet}");
            Health = healthSet;
        }

        void OnJoin() => setHealth(100);
        void OnLeave() => setHealth(100);
    }
}
