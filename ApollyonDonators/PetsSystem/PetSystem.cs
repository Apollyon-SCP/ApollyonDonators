using ApollyonDonators.HatSystem;
using CentralAuth;
using CommandSystem.Commands.RemoteAdmin.Dummies;
using CustomPlayerEffects;
using LabApi.Features.Extensions;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using MEC;
using Mirror;
using NetworkManagerUtils.Dummies;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Scp1507;
using ProjectMER.Commands.Modifying.Scale;
using RelativePositioning;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Logger = LabApi.Features.Console.Logger;

namespace ApollyonDonators.PetsSystem
{
    public static class PetSystem
    {
        // Diccionario: ID del NPC -> Lista de IDs de jugadores que ya "conocen" su disfraz
        public static Dictionary<uint, HashSet<int>> KnownAppearances = new Dictionary<uint, HashSet<int>>();
        public static Dictionary<string, Pet> _pets = new Dictionary<string, Pet>();
        public static Dictionary<string, ReferenceHub> PlayePets = new Dictionary<string, ReferenceHub>();
        public static List<Pet> Pets { get; set; } = new List<Pet>() 
        { 
            new Pet
            {
                PetName = "dog",
                PetRole = RoleTypeId.Scp939,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "peste",
                PetRole = RoleTypeId.Scp049,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "negrito",
                PetRole = RoleTypeId.Scp106,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "zombidito",
                PetRole = RoleTypeId.Scp0492,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "peanut",
                PetRole = RoleTypeId.Scp173,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "chungus",
                PetRole = RoleTypeId.Scp096,
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Pet
            {
                PetName = "chispitas",
                PetRole = RoleTypeId.Tutorial,
                SchematicName = "Sparky",
                SchematicScale = new UnityEngine.Vector3(1f, 1f, 1f),
                IsPetVisibleForOwner = true,
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            }
        };

        public static void SpawnPetForPlayer(Player player, Pet pet)
        {
            var npc = DummyUtils.SpawnDummy("Mascota de " + player.Nickname);
            npc.authManager.syncMode = (SyncMode)ClientInstanceMode.Host;

            Player pett = Player.Get(npc);
            PlayePets[player.UserId] = npc;
            _pets[player.UserId] = pet;

            Timing.CallDelayed(0.1f, () =>
            {
                if (pett == null || player == null) return;

                pett.SetRole(pet.PetRole);
                pett.Position = player.Position;
                pett.Scale = pet.Scale;
                pett.IsGodModeEnabled = true;

                if (!npc.gameObject.TryGetComponent<PlayerFollower>(out var component))
                {
                    npc.gameObject.AddComponent<PlayerFollower>().Init(player.ReferenceHub, pet.MaxDistance, speed: pet.Speed);
                }

                if (string.IsNullOrEmpty(pet.SchematicName))
                {
                    
                }
                else
                {
                    HatSystem.HatSystem.AddHatToPet(pett, new Hat
                    {
                        SchematicName = pet.SchematicName,
                        Scale = pet.SchematicScale,
                        Offset = new Vector3(0, 0.85f, 0),
                    });
                    pett.EnableEffect<Fade>(255);
                }
            });
        }

        public static void RemovePetFromPlayer(Player player)
        {
            if (PlayePets.TryGetValue(player.UserId, out ReferenceHub mascota))
            {
                if (mascota != null && mascota.gameObject != null)
                {
                    NetworkServer.Destroy(mascota.gameObject);
                }
                PlayePets.Remove(player.UserId);
            }

            if (_pets.ContainsKey(player.UserId))
            {
                _pets.Remove(player.UserId);
            }

            HatSystem.HatSystem.RemoveHatFromPet(player);
        }

        public static bool PlayerHavePet(Player player)
        {
            return PlayePets.ContainsKey(player.UserId);
        }
    }
}
