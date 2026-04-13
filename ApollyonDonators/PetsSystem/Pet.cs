using PlayerRoles;
using ProjectMER.Commands.Modifying.Scale;
using UnityEngine;

namespace ApollyonDonators.PetsSystem
{
    public class Pet
    {
        public string PetName { get; set; }
        public RoleTypeId PetRole { get; set; }
        public string SchematicName { get; set; }
        public bool IsPetVisibleForOwner { get; set; } = true;
        public int MaxDistance { get; set; } = 20;
        public int Speed { get; set; } = 30;
        public Vector3 Scale { get; set; } = new Vector3(.5f, .5f, .5f);
        public Vector3 SchematicScale { get; set; } = Vector3.one;
    }
}
