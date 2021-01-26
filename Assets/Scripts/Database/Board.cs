using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// @author 
/// </summary>
namespace DefaultNamespace
{
    public class Board
    {
        /// <summary>
        /// 
        /// </summary>
        public string BoardString;

        /// <summary>
        /// 
        /// </summary>
        public string MissionString;

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="ms"></param>
        public Board(string bs, string ms)
        {
            BoardString = bs;
            MissionString = ms;
        }

        // public BoardComponent[] BoardList;
        //
        // public Board(BoardComponent[] list)
        // {
        //     if (list.Length != 64)
        //     {
        //         Debug.Log("Throw Exception");
        //         throw new Exception("List must contain 64 Components!");
        //     }
        //     this.BoardList = list;
        // }
    }
}