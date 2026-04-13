using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ApollyonDonators.Commandsss
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class scaleCommand : ICommand
    {
        public string Command => "escala";

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

            if (!player.HasAnyPermission("donatortier1.scale"))
            {
                response = "No tienes permiso para usar este comando, Dona al server!";
                return false;
            }

            if (arguments.Count < 3)
            {
                response = "Debes proporcionar 3 valores (x, y, z)";
                return false;
            }

            if (!float.TryParse(arguments.At(0), NumberStyles.Float, CultureInfo.InvariantCulture, out float x) ||
                !float.TryParse(arguments.At(1), NumberStyles.Float, CultureInfo.InvariantCulture, out float y) ||
                !float.TryParse(arguments.At(2), NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
            {
                response = "No pongas letras, solo funcionan números";
                return false;
            }

            if (x < 0.8 || y < 0.8 || z < 0.8 || x > 1.8 || y > 1.8 || z > 1.8)
            {
                response = "Los valores deben estar entre 0.8 y 1.8";
                return false;
            }


            var newScale = new Vector3(x, y, z);
            player.Scale = newScale;

            response = "Escala cambiada";
            return true;
        }
    }
}
