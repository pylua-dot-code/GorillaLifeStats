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
        public int Damage = 10;

        bool inRoom;

        void Start()
        {
            HoneyLib.Events.Events.InfectionTagEvent += OnTag;
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        
        public int getHealth()
        {
            return Health;
        }

        public int getRemainingHealth()
        {
            return 100 - Health;
        }

        public void setDamage(int damage)
        {
            Damage = damage;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
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
                removeHealth(Damage);
            }
        }

        void Update()
        {
            if (Health <= 0)
            {
                Photon.Pun.PhotonNetwork.Disconnect();
                Debug.Log("HealthLib: Action | Kicked from room");
            }
        }

        public void addHealth(int healthadded)
        {
            // we dont want it to make it go more than 100, right?
            if (Health < 100 && Health + healthadded < 100)
            {
                Debug.Log("HealthLib: Action | Health Added: " + healthadded);
                Health += healthadded;
            }
            else
            {
                if (Health < 100)
                {
                    addHealth(100 - (Health - healthadded));
                }
                Debug.Log("HealthLib: Error | Already at Max Health or something!");
            }
        }

        public void removeHealth(int healthremoved)
        {
            if (Health > 0 && Health - healthremoved > 0)
            {
                Debug.Log("HealthLib: Action | Health Removed: " + healthremoved);
                Health -= healthremoved;
            }
            else
            {
                if (Health > 0)
                {
                    removeHealth(100 - (Health - healthremoved));
                }
                Debug.Log("HealthLib: Error | Already at Min Health or something. Player should be dead if the players health is 0");
            }
        }

        public void setHealth(int healthSet)
        {
            Debug.Log("HealthLib: Action | Health Set:" + healthSet);
            Health = healthSet;
        }

        // should be called by the mods when joined and left a room.
        public void OnJoin()
        {
            setHealth(100);
        }

        public void OnLeave()
        {
            setHealth(100);
        }
    }
}
