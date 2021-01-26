using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Mission
    {
        public int[] cargos;
        public int[] cargoCounters;

        public Mission(int[] cargo)
        {
            this.cargos = cargo;
            this.cargoCounters = new int[cargo.Length];
            Debug.Log("First element of cc: " + cargoCounters[0]);
            //cargo.CopyTo(cargoCounters, cargo.Length);
            // int i = 0;
            // foreach (int c in cargoCounters)
            // {
            //     cargoCounters[i++] = 0;
            // }
        }

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