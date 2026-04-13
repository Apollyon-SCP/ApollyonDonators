using ApollyonDonators.HatSystem;
using ApollyonDonators.PetsSystem;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace ApollyonDonators
{
    public class config
    {
        public List<Hat> hats { get; set;  } = new List<Hat>()
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

        public List<Pet> Pets { get; set; } = new List<Pet>()
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

    }
}
