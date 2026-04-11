using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;

namespace ApollyonDonators.Commandsss.HatCommands.ChildCommand
{
    [CommandHandler(typeof(ParentHatCommand))]
    public class quitarHatCommand : ICommand
    {
        public string Command => "quitar";

        public string[] Aliases => null;

        public string Description => "comando para ponerte un gorro";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasAnyPermission("donatortier2.hat.quitar"))
            {
                response = "No tienes permitido poner quitarte un gorro, no eres donador";
                return false;
            }

            HatSystem.HatSystem.RemoveHatFromPlayer(player);

            response = "gorro quitado";
            return true;
        }
    }
}
