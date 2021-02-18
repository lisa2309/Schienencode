using System;
using System.Collections.Generic;
using UnityEngine;


namespace DefaultNamespace
{
    /* created by: SWT-P_WS_2021_Schienencode */
    /// <summary>
    /// This class consists of two encrypted strings which define the a gameboard in the game.
    /// </summary>
    /// @author Bastian Badde
    public class Board
    {
        /// <summary>
        /// Encrypted String to define a gameboard.
        /// It always should look like this: "Board:a1.b1.c1.d1;a2.b2.b3.b4"
        /// with "a1", "a2"... as the x-coordinate
        /// with "b1", "b2"... as the z-coordinate
        /// with "c1", "c2"... as the prefab-code as defined in the DatabasConnector-class
        /// and with "d1", "d2"... as the rotation-value (either 0, 90, 180, or 270)
        /// so each group of abcd defines an actual gameobject to instantiate.
        /// </summary>
        public string BoardString;

        /// <summary>
        /// Encrypted String to define a gameboard.
        /// It always should look like this: "Mission:a0;a1;a2"
        /// with each "ax" as a CargoValue which should be reached at the TrainStation with stationNumber x.
        /// </summary>
        public string MissionString;

        /// <summary>
        /// constructor to define a Board
        /// </summary>
        /// <param name="bs">to set the BoardString</param>
        /// <param name="ms">to set the MissionString</param>
        /// @author Bastian Badde
        public Board(string bs, string ms)
        {
            BoardString = bs;
            MissionString = ms;
        }
    }
}