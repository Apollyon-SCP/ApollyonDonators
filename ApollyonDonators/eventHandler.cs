using ApollyonDonators.PetsSystem;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using PlayerRoles;
using System.Linq;

namespace ApollyonDonators
{
    public class eventHandler
    {
        public void OnPlayerLeft(PlayerLeftEventArgs ev)
        {
            if (ev.Player == null) return;

            if (PetSystem.PlayerHavePet(ev.Player))
            {
                PetSystem.RemovePetFromPlayer(ev.Player);
            }

            if (HatSystem.HatSystem.PlayerHaveHat(ev.Player))
            {
                HatSystem.HatSystem.RemoveHatFromPlayer(ev.Player);
            }
        }

        public void OnPlayerDied(PlayerDeathEventArgs ev) 
        {
            if (ev.Player == null) return;

            if (PetSystem.PlayerHavePet(ev.Player))
            {
                PetSystem.RemovePetFromPlayer(ev.Player);
            }

            if (HatSystem.HatSystem.PlayerHaveHat(ev.Player))
            {
                HatSystem.HatSystem.RemoveHatFromPlayer(ev.Player);
            }
        }

        public void OnRoundEndingCheck(RoundEndingConditionsCheckEventArgs ev)
        {
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

            if (teamsAlive <= 1)
            {
                ev.CanEnd = true;
            }
        }
    }
}
