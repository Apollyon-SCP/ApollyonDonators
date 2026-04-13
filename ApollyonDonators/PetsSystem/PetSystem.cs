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

                pett.SetRole(RoleTypeId.Tutorial);
                pett.Position = player.Position;
                pett.Scale = pet.Scale;
                pett.GroupName = "Mascota";
                pett.IsGodModeEnabled = true;
                pett.EnableEffect<SilentWalk>(255);

                if (!npc.gameObject.TryGetComponent<PlayerFollower>(out var component))
                {
                    npc.gameObject.AddComponent<PlayerFollower>().Init(player.ReferenceHub, pet.MaxDistance, speed: pet.Speed);
                }
                else
                {
                    component.Init(player.ReferenceHub);
                }
            });

            if (string.IsNullOrEmpty(pet.SchematicName))
            {
                ChangeAppearance(pett, pet.PetRole);
                Timing.RunCoroutine(DisguiseRefreshCoroutine(pett, pet.PetRole));
                return;
            }

            HatSystem.HatSystem.AddHatToPet(pett, new Hat
            {
                SchematicName = pet.SchematicName,
                Scale = pet.SchematicScale,
                Offset = new Vector3(0, 0.85f, 0),
            });

            Timing.CallDelayed(0.5f, () =>
            {
                pett.EnableEffect<Fade>(255);
            });

        }


        public static void ChangeAppearance(this Player player, RoleTypeId type, bool skipJump = false, byte unitId = 0) => ChangeAppearance(player, type, Player.List.Where(x => x != player), skipJump, unitId);

        public static void ChangeAppearance(Player player, RoleTypeId type, IEnumerable<Player> playersToAffect, bool skipJump = false, byte unitId = 0)
        {
            PlayerRoleBase roleBase = type.GetRoleBase();
            if (roleBase == null)
            {
                Logger.Error($"No se pudo obtener RoleBase para {type}");
                return;
            }


            bool isRisky = roleBase.Team is Team.Dead || !player.IsAlive;
            NetworkWriterPooled writer = NetworkWriterPool.Get();
            writer.WriteUShort(38952);
            writer.WriteUInt(player.NetworkId);
            writer.WriteRoleType(type);

            if (roleBase is HumanRole humanRole)
            {
                if (player.Role == null || player.Role.GetRoleBase() is not HumanRole)
                    isRisky = true;

                writer.WriteByte(unitId);
            }


            if (roleBase is ZombieRole)
            {

                if (player.Role == null || player.Role.GetRoleBase() is not ZombieRole)
                    isRisky = true;

                writer.WriteUShort((ushort)Mathf.Clamp(Mathf.CeilToInt(player.MaxHealth), ushort.MinValue, ushort.MaxValue));
            }


            if (roleBase is FpcStandardRoleBase fpc)
            {
                FpcStandardRoleBase playerFpc = null;
                if (player.Role != null)
                    playerFpc = player.Role.GetRoleBase() as FpcStandardRoleBase;

                if (playerFpc == null)
                    isRisky = true;
                else
                    fpc = playerFpc;


                if (fpc.FpcModule == null || fpc.FpcModule.MouseLook == null)
                {
                    //Logger.Warn($"MouseLook es null para {player.Nickname}, se omite sync de FPC");
                    writer.WriteRelativePosition(new RelativePosition(Vector3.one));
                    writer.WriteUShort(0);
                }
                else
                {
                    fpc.FpcModule.MouseLook.GetSyncValues(0, out ushort value, out ushort _);
                    writer.WriteRelativePosition(fpc.FpcModule.RelativePosition);
                    writer.WriteUShort(value);
                }
            }

            if (playersToAffect != null)
            {
                foreach (Player target in playersToAffect)
                {
                    if (target == null || target.ReferenceHub == null)
                    {
                        Logger.Error("target o target.ReferenceHub es null en playersToAffect");
                        continue;
                    }

                    if (target.ReferenceHub != player.ReferenceHub || !isRisky)
                        target.Connection.Send(writer.ToArraySegment());
                    else
                        Logger.Error($"Prevent Self-Desync of {player.Nickname} with {type}");
                }
            }

            if (!skipJump)
                player.Position += Vector3.up * 0.25f;
        }

        public static IEnumerator<float> DisguiseRefreshCoroutine(Player npcPlayer, RoleTypeId disguise)
        {
            uint npcNetId = npcPlayer.NetworkId;
            if (!KnownAppearances.ContainsKey(npcNetId))
                KnownAppearances[npcNetId] = new HashSet<int>();

            while (npcPlayer != null && npcPlayer.ReferenceHub != null)
            {
                yield return Timing.WaitForSeconds(1.0f);

                foreach (Player target in Player.List)
                {
                    if (target == npcPlayer || target.IsHost) continue;

                    float distance = Vector3.Distance(target.Position, npcPlayer.Position);
                    bool alreadyKnows = KnownAppearances[npcNetId].Contains(target.PlayerId);

                    // Si el jugador se acerca y NO conoce la apariencia, se la enviamos
                    if (distance < 40f && !alreadyKnows)
                    {
                        ChangeAppearance(npcPlayer, disguise, new[] { target });
                        KnownAppearances[npcNetId].Add(target.PlayerId);
                    }
                    // Si el jugador se aleja mucho, "olvida" la apariencia 
                    // para que se la volvamos a enviar cuando se acerque de nuevo
                    else if (distance > 50f && alreadyKnows)
                    {
                        KnownAppearances[npcNetId].Remove(target.PlayerId);
                    }
                }
            }
            KnownAppearances.Remove(npcNetId);
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
