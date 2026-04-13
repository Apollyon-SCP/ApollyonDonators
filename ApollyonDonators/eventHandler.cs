using LabApi.Events.Arguments.PlayerEvents;
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
    }
}
