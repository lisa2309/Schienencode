﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 
/// @author
/// </summary>
namespace Database
{
    
    public class DatabaseConnector : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        private ObjectPlacer placer;

        /// <summary>
        /// 
        /// </summary>
        public Player player=null;
        //public String boardName;

        /// <summary>
        /// 
        /// </summary>
        public int boardNumber = 0;

        /// <summary>
        /// 
        /// </summary>
        public string boardname;

        /// <summary>
        /// 
        /// </summary>
        private Board board;

        /// <summary>
        /// 
        /// </summary>
        private MissionProver _prover;

        /// <summary>
        /// 
        /// </summary>
        public GameObject curveR;

        /// <summary>
        /// 
        /// </summary>
        public GameObject curveL;

        /// <summary>
        /// 
        /// </summary>
        public GameObject straight;
        
        /// <summary>
        /// 
        /// </summary>
        public GameObject railStart;

        /// <summary>
        /// 
        /// </summary>
        public GameObject railEnd;
        
        /// <summary>
        /// 
        /// </summary>
        public GameObject station;

        //public InputField inputBoardName;

        /// <summary>
        /// 
        /// </summary>
        private Mission _mission;

        //private String connectionString = "https://schienencode-default-rtdb.europe-west1.firebasedatabase.app/";

        /// <summary>
        /// 
        /// </summary>
        private String connectionString = "https://swt-p-ss20-profcollector.firebaseio.com/schienencode/";


        /// <summary>
        /// 
        /// @author
        /// </summary>
        public void OnGetSubmit()
        {
            //boardname = inputBoardName.text;
            boardname = "board0";
            RetrieveFromDatabase();
            Debug.Log("Retrieved: " + board.BoardString);
        }
        
        /// <summary>
        /// 
        /// @author
        /// </summary>
        public void OnPutSubmit()
        {
            
            //placer = FindObjectOfType<ObjectPlacer>();
            //PostToDatabase(new Board("Board:2.6.0.0;6.6.0.0;10.6.0.0"));
            //PostToDatabase(new Board("Board:6.6.0.0;10.6.0.0;16.6.10.0;16.12.10.270;12.12.0.180;8.12.0.180;2.12.10.180;2.6.10.90"));
            //PostToDatabase(new Board("Board:26.14.20.90;22.14.0.180;21.12.30.0;17.12.30.0;18.14.0.180;14.14.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:18.14.20.90;14.14.0.180;13.12.30.0;9.12.30.0;10.14.0.180;6.14.21.90", "Mission:5;5"));
            PostToDatabase(new Board("Board:202.56.20.90;198.56.0.180;197.54.30.0;193.54.30.0;194.56.0.180;190.56.21.90", "Mission:5;5"));

        }

        /// <summary>
        /// 
        /// Varibles:
        /// boardInfo:
        /// structures:
        /// coordinates:
        /// x:
        /// z:
        /// rot:
        /// @author
        /// </summary>
        public void BuildFromDB()
        {
            // RetrieveFromDatabase();
            // if (board is null)
            // {
            //     Debug.Log("No board yet.");
            //     return;
            // }
            string boardInfo = board.BoardString.Split(':')[1];
            string[] structures = boardInfo.Split(';');
            string[] coordinates;
            float x, z, rot;
            foreach (string structure in structures)
            {
                coordinates = structure.Split('.');
                x = float.Parse(coordinates[0]);
                z = float.Parse(coordinates[1]);
                rot = float.Parse(coordinates[3]);
                // placer.gameObject = GetObject(coordinates[2]);
                // placer.PlaceObjectNearPointManual(new Vector3(x, 0 , z));
                if(player != null){
                    player.anrufen(GetObject(coordinates[2]).name, new Vector3(x, 0, z), rot);
                }
                // var skeleton = Instantiate(GetObject(coordinates[2]), new Vector3(x, 0, z), Quaternion.Euler(0, rot, 0));
                // skeleton.name = skeleton.name.Replace("(Clone)","").Trim();
                
            }
        }

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// Varibles:
        /// obj:
        /// <param name="code"></param>
        /// <returns></returns>
        private GameObject GetObject(string code)
        {
            GameObject obj;
            switch (code)
            {
                case "0":
                    //obj = GameObject.Find("Straight270Final");
                    obj = straight;
                    break;
                case "10":
                    obj = curveL;
                    break;
                case "11":
                    obj = curveR;
                    break;
                case "20":
                    obj = railStart;
                    break;
                case "21":
                    obj = railEnd;
                    break;
                case "30":
                    obj = station;
                    break;
                default:
                    obj = null;
                    break;
            }
            return obj;
        }

        /// <summary>
        /// 
        /// @author
        /// </summary>
        public void RetrieveFromDatabase()
        {
            RestClient.Get<Board>(connectionString + boardname + ".json").Then(response =>
            {
                board = response;
                CreateMission();
                BuildFromDB();
            });
        }

        /// <summary>
        /// 
        /// Variables:
        /// missionInfo:
        /// cargos:
        /// cargosInt:
        /// i:
        /// @author
        /// </summary>
        private void CreateMission()
        {
            string missionInfo = board.MissionString.Split(':')[1];
            string[] cargos = missionInfo.Split(';');
            int[] cargosInt = new int[cargos.Length];
            int i = 0;
            foreach (string cargo in cargos)
            {
                cargosInt[i] = Int32.Parse(cargos[i]);
                i++;
            }
            _prover.SetMission(new Mission(cargosInt));
        }

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// <param name="board"></param>
        public void PostToDatabase(Board board)
        {
            RestClient.Put(connectionString +  boardname + ".json", board);
        }

        /// <summary>
        /// 
        /// @author
        /// </summary>
        void Start()
        {
            _prover = FindObjectOfType<MissionProver>();
        }

    }
}
