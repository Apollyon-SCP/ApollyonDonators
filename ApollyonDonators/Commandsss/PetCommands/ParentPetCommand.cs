using ApollyonDonators.Commandsss.PetCommands.ChildCommand;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;

namespace ApollyonDonators.Commandsss.PetCommands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ParentPetCommand : ParentCommand
    {
        public override string Command => "mascota";

        public override string[] Aliases => null;

        public override string Description => "Comando para tener mascotas, Donadores only";
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new ListPetsCommand());
            RegisterCommand(new ponerPetCommand());
            RegisterCommand(new quitarPetCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donator.pet"))
            {
                response = "No tienes permiso para ejecutar este comando, dona al server";
                return false;
            }

            response = ".mascota <lista/poner <Nombre de la mascota>/quitar>";
            return true;
        }
    }
}
