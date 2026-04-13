using ApollyonDonators.PetsSystem;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApollyonDonators
{
    public class eventHandler
    {
        public void OnPlayerLeft(PlayerLeftEventArgs ev)
        {
            if (ev.Player == null) return;

            if (PetsSystem.PetSystem.PlayerHavePet(ev.Player))
            {
                PetsSystem.PetSystem.RemovePetFromPlayer(ev.Player);
            }
        }

        public void OnPlayerDied(PlayerDeathEventArgs ev) 
        {
            if (ev.Player == null) return;

            if (PetsSystem.PetSystem.PlayerHavePet(ev.Player))
            {
                PetsSystem.PetSystem.RemovePetFromPlayer(ev.Player);
            }
        }

        public void OnRoundEndingCheck(LabApi.Events.Arguments.ServerEvents.RoundEndingConditionsCheckEventArgs ev)
        {
            // Calculamos manualmente si la ronda debería acabar IGNORANDO mascotas
            var hubs = ReferenceHub.AllHubs.Where(h => !PetSystem.PlayePets.ContainsValue(h)).ToList();

            int mtfCientificos = hubs.Count(h => h.GetTeam() == Team.FoundationForces || h.GetTeam() == Team.Scientists);
            int chaosClaseD = hubs.Count(h => h.GetTeam() == Team.ChaosInsurgency || h.GetTeam() == Team.ClassD);
            int scps = hubs.Count(h => h.GetTeam() == Team.SCPs);
            int flamingos = hubs.Count(h => h.GetTeam() == Team.Flamingos);

            int teamsAlive = 0;
            if (mtfCientificos > 0) teamsAlive++;
            if (chaosClaseD > 0) teamsAlive++;
            if (scps > 0) teamsAlive++;
            if (flamingos > 0) teamsAlive++;

            // Si solo queda 1 equipo real (o ninguno), forzamos el fin
            if (teamsAlive <= 1)
            {
                ev.CanEnd = true;
            }
        }
    }
}
