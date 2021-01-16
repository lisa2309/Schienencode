using System;
using System.Collections.Generic;
using DefaultNamespace;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Database
{
    
    public class DatabaseConnector : MonoBehaviour
    {
        private ObjectPlacer placer;
        //public String boardName;
        public int boardNumber = 0;
        public string boardname;
        private Board board;

        public GameObject CurveR;

        public GameObject CurveL;

        public GameObject Straight;
        //public InputField inputBoardName;

        //private String connectionstring = "https://schienencode-default-rtdb.europe-west1.firebasedatabase.app/";
        private String connectionstring = "https://swt-p-ss20-profcollector.firebaseio.com/schienencode/";

        public void OnGetSubmit()
        {
            //boardname = inputBoardName.text;
            boardname = "board0";
            RetrieveFromDatabase();
            Debug.Log("Retrieved: " + board.BoardString);
        }

        public void BuildFromDB()
        {
            RetrieveFromDatabase();
            if (board is null)
            {
                Debug.Log("No board yet.");
                return;
            }
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
                Instantiate(GetObject(coordinates[2]), new Vector3(x, 0, z), Quaternion.Euler(0, rot, 0));
            }
        }

        private GameObject GetObject(string code)
        {
            GameObject obj;
            switch (code)
            {
                case "0":
                    //obj = GameObject.Find("Straight270Final");
                    obj = Straight;
                    break;
                case "10":
                    obj = CurveL;
                    break;
                case "11":
                    obj = CurveR;
                    break;
                default:
                    obj = null;
                    break;
            }
            return obj;
        }
        
        public void OnSubmit()
        {
            placer = FindObjectOfType<ObjectPlacer>();
            
            //PostToDatabase(new Board("Board:2.6.0.0;6.6.0.0;10.6.0.0"));
            PostToDatabase(new Board("Board:6.6.0.0;10.6.0.0;16.6.10.0;16.12.10.270;12.12.0.180;8.12.0.180;2.12.10.180;2.6.10.90"));
            
        }
        
        

        public void RetrieveFromDatabase()
        {
            RestClient.Get<Board>(connectionstring + boardname + ".json").Then(response =>
            {
                board = response;
            });
        }

        public void PostToDatabase(Board board)
        {
            RestClient.Put(connectionstring + "board" + boardNumber++ + ".json", board);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
