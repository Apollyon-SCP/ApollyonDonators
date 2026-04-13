using ApollyonDonators.PetsSystem;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Linq;

namespace ApollyonDonators.Commandsss.PetCommands.ChildCommand
{
    [CommandHandler(typeof(ParentPetCommand))]
    public class ponerPetCommand : ICommand
    {
        public string Command => "poner";

        public string[] Aliases => null;

        public string Description => "comando para ponerte una mascota";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donatortier2.petponer"))
            {
                response = "No tienes permitido poner mascotas, no eres donador";
                return false;
            }

            if (arguments.At(0) == null)
            {
                response = "Tienes que especificar una mascota";
                return false;
            }
                
            var playerPet = PetSystem._pets.Any(p => p.Key == player.UserId);
            if (playerPet)
            {
                response = "Tienes que quitarte la mascota para ponerte otra";
                return false;
            }

            Pet pet = PetSystem.Pets.Where(p => p.PetName == arguments.At(0)).FirstOrDefault();
            if (pet == null)
            {
                response = "Esa mascota no existe";
                return false;
            }

            PetSystem.SpawnPetForPlayer(player, pet);

            response = "Mascota puesta";
            return true;
        }
    }
}
