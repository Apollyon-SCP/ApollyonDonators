using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollyonDonators.Commandsss
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class setNickNameCommand : ICommand
    {
        public string Command => "nick";

        public string[] Aliases => null;

        public string Description => "Comando de donador, comando para cambiar tu nick";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "No se ha encontrado usuario";
                return false;
            }

            if (!player.HasPermissions("donatortier2.nickname"))
            {
                response = "No tienes permiso para usar este comando, Dona al server!";
                return false;
            }

            string[] args = arguments.ToArray();
            string newNickname = string.Join(" ", args);

            player.DisplayName = newNickname;

            response = "Tu nuevo nick es: " + newNickname;
            return true;
        }
    }
}
