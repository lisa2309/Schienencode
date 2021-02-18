using System;
using System.Collections.Generic;
using DefaultNamespace;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Database
{
    /* created by: SWT-P_WS_2021_Schienencode */
    /// <summary>
    /// This class is a major control-instance. It communicates with the Firebase-database and organizes Board- and
    /// Mission-objects to be loaded from or to the database.
    /// </summary>
    /// @author Bastian Badde
    public class DatabaseConnector : MonoBehaviour
    {
        // /// <summary>
        // /// 
        // /// </summary>
        // private ObjectPlacer placer;

        /// <summary>
        /// the Player Object of the active Player
        /// </summary>
        public Player player=null;

        // /// <summary>
        // /// 
        // /// </summary>
        // public int boardNumber = 0;

        /// <summary>
        /// the name of the board in the Firebase-DB
        /// </summary>
        public string boardname;

        /// <summary>
        /// the current selected Board-object
        /// </summary>
        private Board board;

        /// <summary>
        /// MissionProver object of the scene for organisation
        /// </summary>
        private MissionProver _prover;

        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject curveR;
        //
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject curveL;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject switchL0;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject switchL1;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject switchR0;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject switchR1;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject straight;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject railStart;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject railEnd;
        //
        // /// <summary>
        // /// 
        // /// </summary>
        // public GameObject station;

        //public InputField inputBoardName;

        /// <summary>
        /// the current selected Mission-object
        /// </summary>
        private Mission _mission;

        //private String connectionString = "https://schienencode-default-rtdb.europe-west1.firebasedatabase.app/";

        /// <summary>
        /// the ConnectionString to the FirebaseDB
        /// </summary>
        private readonly String connectionString = "https://swt-p-ss20-profcollector.firebaseio.com/schienencode/";
        
        
        /// <summary>
        /// hard coded method to post data to the FirebaseDB
        /// </summary>
        /// @author bastian Badde
        public void OnPutSubmit()
        {
            //placer = FindObjectOfType<ObjectPlacer>();
            //PostToDatabase(new Board("Board:2.6.0.0;6.6.0.0;10.6.0.0"));
            //PostToDatabase(new Board("Board:6.6.0.0;10.6.0.0;16.6.10.0;16.12.10.270;12.12.0.180;8.12.0.180;2.12.10.180;2.6.10.90"));
            //PostToDatabase(new Board("Board:26.14.20.90;22.14.0.180;21.12.30.0;17.12.30.0;18.14.0.180;14.14.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:18.14.20.90;14.14.0.180;13.12.30.0;9.12.30.0;10.14.0.180;6.14.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:202.56.20.90;198.56.0.180;197.54.30.0;193.54.30.0;194.56.0.180;190.56.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:202.56.20.90;198.56.30.180;194.56.30.180;190.56.21.90", "Mission:5;5"));
            PostToDatabase(new Board("Board:206.56.20.90;198.56.30.180;194.56.30.180;204.50.21.270;188.50.11.180;200.50.15.180;" +
                                     "196.50.0.0;192.50.18.0;202.56.15.0;190.56.17.180;184.56.10.90;184.62.10.180;202.62.10.270;" +
                                     "188.62.0.0;192.62.0.0;196.62.0.0;194.44.11.180;200.44.11.90", "Mission:5;10"));

        }

        /// <summary>
        /// Builds a board out of a Board-object which is loaded from the Firebase-DB an instantiates it.
        /// </summary>
        /// Variables:
        /// boardInfo: relevant Boardstring data
        /// structures: relevant Boardstring data for each gameobject to be instantiated
        /// coordinates: coordinates as described in the Board-class
        /// x: x-coordinate
        /// z: z-coordinate
        /// rot: rotation-value
        /// @author Bastian Badde
        public void BuildFromDB()
        {
            MissionProver.buildOnDB = true;
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
                    //player.anrufen(GetObject(coordinates[2]).name, new Vector3(x, 0, z), rot);
                    player.anrufen(GetObjectName(coordinates[2]), new Vector3(x, 0, z), rot);
                }
                // var skeleton = Instantiate(GetObject(coordinates[2]), new Vector3(x, 0, z), Quaternion.Euler(0, rot, 0));
                // skeleton.name = skeleton.name.Replace("(Clone)","").Trim();
            }
            MissionProver.buildOnDB = false;
        }
        
        
        /// <summary>
        /// transforms a encrypted string into the name of the relevant gameobject
        /// </summary>
        /// <param name="code">encrypted string</param>
        /// <returns>name of the relevant gameobject</returns>
        ///  /// @author Bastian Badde
        private string GetObjectName(string code)
        {
            string name;
            switch (code)
            {
                case "0":
                    name = "Straight270Final";
                    break;
                case "10":
                    name = "CurveL0Final";
                    break;
                case "11":
                    name = "CurveR0Final";
                    break;
                case "15":
                    name = "SwitchL0Final";;
                    break;
                case "16":
                    name = "SwitchL1Final";
                    break;
                case "17":
                    name = "SwitchR0Final";
                    break;
                case "18":
                    name = "SwitchR1Final";
                    break;
                case "20":
                    name = "RailStart";
                    break;
                case "21":
                    name = "RailEnd";
                    break;
                case "30":
                    name = "TrainStation";
                    break;
                default:
                    name = null;
                    break;
            }
            return name;
        }

        //
        // /// <summary>
        // /// 
        // /// @author
        // /// </summary>
        // /// Variables:
        // /// obj:
        // /// <param name="code"></param>
        // /// <returns></returns>
        // private GameObject GetObject(string code)
        // {
        //     GameObject obj;
        //     switch (code)
        //     {
        //         case "0":
        //             //obj = GameObject.Find("Straight270Final");
        //             obj = straight;
        //             break;
        //         case "10":
        //             obj = curveL;
        //             break;
        //         case "11":
        //             obj = curveR;
        //             break;
        //         case "15":
        //             obj = switchL0;
        //             break;
        //         case "16":
        //             obj = switchL1;
        //             break;
        //         case "17":
        //             obj = switchR0;
        //             break;
        //         case "18":
        //             obj = switchR1;
        //             break;
        //         case "20":
        //             obj = railStart;
        //             break;
        //         case "21":
        //             obj = railEnd;
        //             break;
        //         case "30":
        //             obj = station;
        //             break;
        //         default:
        //             obj = null;
        //             break;
        //     }
        //     return obj;
        // }

        /// <summary>
        /// Loads a Board-Object from the database and interpret it.
        /// </summary>
        /// @author Bastian Badde
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
        /// interprets the MissionString of the current seleted Board-object and displays it
        /// </summary> 
        /// Variables:
        /// missionInfo: relevant data of the MissionString
        /// cargos: string-values of the missionInfo
        /// cargosInt: cargos transformed into int-values
        /// @author
        private void CreateMission()
        {
            string missionInfo = board.MissionString.Split(':')[1];
            string[] cargos = missionInfo.Split(';');
            int[] cargosInt = new int[cargos.Length];
            int i = 0;
            List<string> ddStrings = new List<string>();
            foreach (string cargo in cargos)
            {
                ddStrings.Add("C" + (i +1));
                cargosInt[i] = Int32.Parse(cargos[i]);
                i++;
            }
            _prover.SetMission(new Mission(cargosInt));
            _prover.ddSwitchValue.AddOptions(ddStrings);
            _prover.ddWhileSwitchValue.AddOptions(ddStrings);
        }

        /// <summary>
        /// Uploads a Board-Object to the database.
        /// </summary>
        /// <param name="board">Board-object to be uploaded</param>
        public void PostToDatabase(Board board)
        {
            RestClient.Put(connectionString +  boardname + ".json", board);
        }

        /// <summary>
        /// Initialises the MissionProver-object
        /// </summary>
        /// @author Bastian Badde
        void Start()
        {
            _prover = FindObjectOfType<MissionProver>();
        }

    }
}
