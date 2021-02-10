using System;
using UnityEngine;

/// <summary>
/// 
/// @author
/// </summary>
namespace DefaultNamespace
{
    public class Mission
    {
        /// <summary>
        /// 
        /// </summary>
        public int[] cargos;

        /// <summary>
        /// 
        /// </summary>
        public int[] cargoCounters;

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// <param name="cargo"></param>
        public Mission(int[] cargo)
        {
            this.cargos = cargo;
            this.cargoCounters = new int[cargo.Length];
            Debug.Log("First element of cc: " + cargoCounters[0]);
        }

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// <returns></returns>
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