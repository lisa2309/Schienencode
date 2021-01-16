using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Board
    {
        public string BoardString;

        public Board(string bs)
        {
            BoardString = bs;
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