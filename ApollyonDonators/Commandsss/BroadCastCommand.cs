using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Linq;

namespace ApollyonDonators.Commandsss
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BroadCastCommand : ICommand
    {
        public string Command => "bc";

        public string[] Aliases => null;

        public string Description => "Comando de donador, comando para enviar un broadcast";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "No se ha encontrado usuario";
                return false;
            }

            if (!player.HasPermissions("donator.bc"))
            {
                response = "No tienes permiso para usar este comando, Dona al server!";
                return false;
            }

            string[] args = arguments.ToArray();
            string broadcast = string.Join(" ", args);

            Server.SendBroadcast($"<b><color=yellow>[{player.DisplayName}]</color></b>\n" + broadcast, 7);

            response = "Broadcast enviado";
            return true;
        }
    }
}
