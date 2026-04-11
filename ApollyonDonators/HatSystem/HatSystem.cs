using JetBrains.Annotations;
using LabApi.Features.Wrappers;
using Mirror;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ApollyonDonators.HatSystem
{
    public class HatSystem
    {
        public static readonly Dictionary<string, SchematicObject> _hats = new Dictionary<string, SchematicObject>();

        public static List<Hat> hats = new List<Hat>() 
        {
            new Hat
            {
                Hatname = "frog",
                SchematicName = "Frog",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.25f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "horns",
                SchematicName = "Horns",
                IsSchematicVisibleForOwner = false,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 90, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "popnpills",
                SchematicName = "PopnPills",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "halo",
                SchematicName = "Halo",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "policehat",
                SchematicName = "PoliceHat",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "estrellas",
                SchematicName = "SeeingStars",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "chefhat2",
                SchematicName = "ChefHat2",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            },
            new Hat
            {
                Hatname = "chefhat",
                SchematicName = "ChefHat",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Vector3(0, 0, 0),
                Scale = new UnityEngine.Vector3(1, 1, 1)
            }
        };

        public static void AddHatToPlayer(Player player, Hat hat)
        {
            var hatOb = SpawnSchematic(hat);
            if (hatOb is null)
            {
                LabApi.Features.Console.Logger.Info("Failed to spawn hat schematic.");
                return;
            }

            
            hatOb.transform.position = player.Position + hat.Offset;
            hatOb.transform.rotation = player.Rotation;
            hatOb.transform.parent = player.GameObject.transform;

            _hats[player.UserId] = hatOb;

            var netIdentity = hatOb.GetComponent<NetworkIdentity>();
            var visibility = hatOb.GetComponent<HatVisibility>();

            if (visibility == null)
                visibility = hatOb.gameObject.AddComponent<HatVisibility>();

            if (netIdentity != null && player.Connection != null)
            {
                if (!hat.IsSchematicVisibleForOwner)
                {
                    visibility.TargetSetVisible(player.Connection, false);
                }
            }
        }

        public static void RemoveHatFromPlayer(Player player)
        {
            if (_hats.TryGetValue(player.UserId, out var hatOb))
            {
                hatOb.Destroy();
                _hats.Remove(player.UserId);
            }
        }

        public static SchematicObject SpawnSchematic(Hat config)
        {
            return ObjectSpawner.SpawnSchematic(
                config.SchematicName,
                Vector3.zero,
                config.Rotation,
                config.Scale
            );
        }
    }
}
