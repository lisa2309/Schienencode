using System;
using UnityEngine;

namespace DefaultNamespace
{
    /* created by: SWT-P_WS_2021_Schienencode */
    /// <summary>
    /// This class describes the state of an in-game-mission.
    /// </summary>
    /// @author Bastian Badde
    public class Mission
    {
        /// <summary>
        /// Array of CargoValues which should be reached to complete the mission.
        /// Relevant CargoValue accessible by: cargos[stationNumber]
        /// </summary>
        public int[] cargos;

        /// <summary>
        /// Array of CargoValues which are currently reached in the game.
        /// Relevant CargoValue accessible by: cargoCounters[stationNumber]
        /// </summary>
        public int[] cargoCounters;

        /// <summary>
        /// constructor of a Mission-object. Setting all cargoCounters-values on 0
        /// </summary>
        /// <param name="cargo">array to initialize cargos with</param>
        /// @author Bastian Badde 
        public Mission(int[] cargo)
        {
            this.cargos = cargo;
            this.cargoCounters = new int[cargo.Length];
        }

        /// <summary>
        /// returns the state of the mission-completion
        /// </summary>
        /// <returns>true if mission is completed, false if not</returns>
        /// @author Bastian Badde
        public bool IsComplete()
        {
            int i = 0;
            foreach (int v in cargos)
            {
                if (v != cargoCounters[i]) return false;
                i++;
            }
            return true;
        }
    }
}