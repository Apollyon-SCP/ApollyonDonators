using ApollyonDonators.PetsSystem;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApollyonDonators.Commandsss.PetCommands.ChildCommand
{
    [CommandHandler(typeof(ParentPetCommand))]
    public class ListPetsCommand : ICommand
    {
        public string Command => "lista";

        public string[] Aliases => null;

        public string Description => "comando para ver la lista de mascotas";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.HasPermissions("donator.petlist"))
            {
                response = "No tienes permitido ver la lista de mascotas, no eres donador";
                return false;
            }

            List<string> pets = Main.Instance.Config.Pets.Select(p => p.PetName).ToList();

            response = string.Join("\n ", pets);
            return true;
        }
    }
}
