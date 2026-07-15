using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApollyonDonators.Commandsss.HatCommands.ChildCommand
{
    [CommandHandler(typeof(ParentHatCommand))]
    public class listhatCommands : ICommand
    {
        public string Command => "lista";

        public string[] Aliases => null;

        public string Description => "comando para ver la lista de gorros";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donator.hatlist"))
            {
                response = "No tienes permitido ver la lista de gorros, no eres donador";
                return false;
            }

            List<string> hats = Main.Instance.Config.hats.Select(h => h.Hatname).ToList();

            response = string.Join("\n ", hats);
            return true;
        }
    }
}
