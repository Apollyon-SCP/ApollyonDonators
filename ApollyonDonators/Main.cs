using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollyonDonators
{
    public class Main : Plugin
    {
        public override string Name => "Donator_Plugin";

        public override string Description => "Plugin de donadores";

        public override string Author => "davilone32";

        public override Version Version => new Version(1, 0, 0);

        public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

        public override void Enable()
        {
            
        }

        public override void Disable()
        {
            
        }
    }
}
