using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ApollyonDonators.HatSystem
{
    public class Hat
    {
        public string Hatname { get; set; }
        public string SchematicName { get; set; }
        public bool IsSchematicVisibleForOwner { get; set; } = true;
        public Vector3 Offset { get; set; } = Vector3.zero;
        public Vector3 Rotation { get; set; } = Vector3.zero;
        public Vector3 Scale { get; set; } = Vector3.one;
    }
}
