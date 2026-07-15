using ApollyonDonators.Commandsss.HatCommands.ChildCommand;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollyonDonators.Commandsss.HatCommands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ParentHatCommand : ParentCommand
    {
        public override string Command => "gorro";

        public override string[] Aliases => null;

        public override string Description => "Comando para tener gorros, Donadores only";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new listhatCommands());
            RegisterCommand(new ponerhatCommand());
            RegisterCommand(new quitarHatCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donator.hat"))
            {
                response = "No tienes permiso para ejecutar este comando, dona al server";
                return false;
            }

            response = ".gorro <lista/poner <Nombre del gorro>/quitar>";
            return true;
        }
    }
}
