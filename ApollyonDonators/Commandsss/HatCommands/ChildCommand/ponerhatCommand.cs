using ApollyonDonators.HatSystem;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Linq;

namespace ApollyonDonators.Commandsss.HatCommands.ChildCommand
{
    [CommandHandler(typeof(ParentHatCommand))]
    public class ponerhatCommand : ICommand
    {
        public string Command => "poner";

        public string[] Aliases => null;

        public string Description => "comando para ponerte un gorro";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donatortier2.hatponer"))
            {
                response = "No tienes permitido poner gorros, no eres donador";
                return false;
            }

            if (arguments.At(0) == null)
            {
                response = "Tienes que especificar el gorro";
                return false;
            }

            var playerhat = HatSystem.HatSystem._hats.Any(p => p.Key == player.UserId);
            if (playerhat)
            {
                response = "Tienes que quitarte el gorro para ponerte otro";
                return false;
            }

            Hat hat = Main.Instance.Config.hats.Where(p => p.Hatname == arguments.At(0)).FirstOrDefault();
            if (hat == null)
            {
                response = "Ese gorro no existe";
                return false;
            }

            HatSystem.HatSystem.AddHatToPlayer(player, hat);

            response = "gorro puesto";
            return true;
        }
    }
}
