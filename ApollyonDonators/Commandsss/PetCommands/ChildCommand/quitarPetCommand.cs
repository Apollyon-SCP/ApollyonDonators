using ApollyonDonators.PetsSystem;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollyonDonators.Commandsss.PetCommands.ChildCommand
{
    [CommandHandler(typeof(ParentPetCommand))]
    public class quitarPetCommand : ICommand
    {
        public string Command => "quitar";

        public string[] Aliases => null;

        public string Description => "comando para ponerte un gorro";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donator.petquitar"))
            {
                response = "No tienes permitido poner quitarte un gorro, no eres donador";
                return false;
            }

            PetSystem.RemovePetFromPlayer(player);

            response = "mascota quitada";
            return true;
        }
    }
}
