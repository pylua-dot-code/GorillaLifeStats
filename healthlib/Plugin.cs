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

        bool verboseMode;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void setVerbose(bool verbose)
        {
            if (verbose)
            {
                verboseMode = true;
            }
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            Debug.Log("HealthLib: Credits | Thanks for using HealthLib - pylua.code");
        }
        void OnTag(object sender, InfectionTagEventArgs e)
        {
            if (e.taggedPlayer.IsLocal && e.taggingPlayer != null)
            {
                removeHealth(5);
            }
        }
        void Update()
        {
            if (Health <= 0)
            {
                PhotonNetwork.Disconnect();
                Debug.Log("HealthLib: Action | Kicked from room");
            }
            if (verboseMode)
            {
                Debug.Log("HealthLib: Verbose | Checked Health");
            }

        }

        void addHealth(int healthadded)
        {
            if (Health < 100)
            {
                Debug.Log("HealthLib: Action | Health Added: " + healthadded);
                Health = Health + healthadded;
            }
            else
            {
                Debug.Log("HealthLib: Error | Already at Max Health!");
            }
        }

        void removeHealth(int healthremoved)
        {
            if (Health > 0)
            {
                Debug.Log("HealthLib: Action | Health Removed: " + healthremoved);
                Health = Health - healthremoved;
            }
            else
            {
                Debug.Log("HealthLib: Error | Already at Min Health. Player should be dead");
            }
        }

        void setHealth(int healthSet)
        {
            Debug.Log("HealthLib: Action | Health Set:" + healthSet);
            Health = healthSet;
        }

    }
}
