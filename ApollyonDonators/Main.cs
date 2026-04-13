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

        public eventHandler eventHandler { get; set; }

        public override void Enable()
        {
            eventHandler = new eventHandler();
            LabApi.Events.Handlers.PlayerEvents.Left += eventHandler.OnPlayerLeft;
            LabApi.Events.Handlers.PlayerEvents.Death += eventHandler.OnPlayerDied;
            LabApi.Events.Handlers.ServerEvents.RoundEndingConditionsCheck += eventHandler.OnRoundEndingCheck;
        }

        public override void Disable()
        {
            LabApi.Events.Handlers.ServerEvents.RoundEndingConditionsCheck -= eventHandler.OnRoundEndingCheck;
            LabApi.Events.Handlers.PlayerEvents.Death -= eventHandler.OnPlayerDied;
            LabApi.Events.Handlers.PlayerEvents.Left -= eventHandler.OnPlayerLeft;
            eventHandler = null;
        }
    }
}
