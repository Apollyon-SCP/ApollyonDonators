using LabApi.Features.Wrappers;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace ApollyonDonators.HatSystem
{
    public class HatSystem
    {
        public static readonly Dictionary<string, SchematicObject> _hats = new Dictionary<string, SchematicObject>();
        public static readonly Dictionary<string, SchematicObject> _DummysHat = new Dictionary<string, SchematicObject>();

        public static List<Hat> hats = new List<Hat>() 
        {
            new Hat
            {
                Hatname = "frog",
                SchematicName = "Frog",
                IsSchematicVisibleForOwner = true,
                Offset = new Vector3(0, 0.55f, 0),
                Rotation = new Quaternion(0, 180, 0, 0),
                Scale = new Vector3(0.5f, 0.5f, 0.5f)
            },
            new Hat
            {
                Hatname = "horns",
                SchematicName = "Horns",
                IsSchematicVisibleForOwner = false,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Quaternion(0, 90, 0, 0),
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Hat
            {
                Hatname = "policehat",
                SchematicName = "PoliceHat",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0, 0.55f, 0),
                Rotation = new UnityEngine.Quaternion(0, 0, 0, 0),
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Hat
            {
                Hatname = "chefhat2",
                SchematicName = "ChefHat2",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0f, 1f, -0.05f),
                Rotation = new UnityEngine.Quaternion(0, 0, 90, 0),
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
            },
            new Hat
            {
                Hatname = "chefhat",
                SchematicName = "ChefHat",
                IsSchematicVisibleForOwner = true,
                Offset = new UnityEngine.Vector3(0f, 0.55f, -0.1f),
                Rotation = new UnityEngine.Quaternion(0, 0, 0, 0),
                Scale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f)
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

            hatOb.transform.localScale = hat.Scale;
            hatOb.transform.lossyScale.Set(hat.Scale.x, hat.Scale.y, hat.Scale.z);

            hatOb.transform.position = player.Position + hat.Offset;
            hatOb.transform.rotation = player.Rotation * hat.Rotation;
            hatOb.transform.parent = player.GameObject.transform;

            _hats[player.UserId] = hatOb;
        }

        public static void AddHatToPet(Player player, Hat hat)
        {
            var hatOb = SpawnSchematic(hat);
            if (hatOb is null)
            {
                LabApi.Features.Console.Logger.Info("Failed to spawn hat schematic.");
                return;
            }

            hatOb.transform.localScale = hat.Scale;
            hatOb.transform.lossyScale.Set(hat.Scale.x, hat.Scale.y, hat.Scale.z);

            hatOb.transform.position = player.Position - hat.Offset;
            hatOb.transform.rotation = player.Rotation * hat.Rotation;
            hatOb.transform.parent = player.GameObject.transform;

            _hats[player.NetworkId.ToString()] = hatOb;
        }

        public static void RemoveHatFromPlayer(Player player)
        {
            if (_hats.TryGetValue(player.UserId, out var hatOb))
            {
                hatOb.Destroy();
                _hats.Remove(player.UserId);
            }
        }
        public static void RemoveHatFromPet(Player player)
        {
            if (_DummysHat.TryGetValue(player.NetworkId.ToString(), out var hatOb))
            {
                hatOb.Destroy();
                _hats.Remove(player.NetworkId.ToString());
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
