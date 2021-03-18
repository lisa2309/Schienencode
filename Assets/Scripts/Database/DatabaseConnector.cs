using System;
using System.Collections.Generic;
using DefaultNamespace;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;


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

        /// <summary>
        /// The Player Object of the active Player
        /// </summary>
        public Player player = null;

        /// <summary>
        /// The name of the board in the Firebase-DB
        /// </summary>
        public string boardName;

        /// <summary>
        /// The current selected Board-object
        /// </summary>
        private Board board;

        /// <summary>
        /// MissionProver object of the scene for organisation
        /// </summary>
        private MissionProver prover;
        
        /// <summary>
        /// The current selected Mission-object
        /// </summary>
        private Mission mission;

        /// <summary>
        /// The ConnectionString to the FirebaseDB
        /// </summary>
        private readonly String connectionString = "https://swt-p-ss20-profcollector.firebaseio.com/schienencode/";
        
        
        /// <summary>
        /// Hard coded method to post data to the FirebaseDB
        /// </summary>
        /// @author bastian Badde
        public void OnPutSubmit()
        {
            Debug.Log("Posted...");
        }

        /// <summary>
        /// Builds a board out of a Board-object which is loaded from the Firebase-DB an instantiates it.
        /// </summary>
        /// boardInfo: Relevant Boardstring data
        /// structures: Relevant Boardstring data for each gameobject to be instantiated
        /// coordinates: Coordinates as described in the Board-class
        /// x: x-coordinate
        /// z: z-coordinate
        /// rot: Rotation-value
        /// @author Bastian Badde
        public void BuildFromDB()
        {
            MissionProver.buildOnDB = true;
            string boardInfo = board.boardString.Split(':')[1];
            string[] structures = boardInfo.Split(';');
            string[] coordinates;
            float x, z, rot;
            foreach (string structure in structures)
            {
                coordinates = structure.Split('.');
                x = float.Parse(coordinates[0]);
                z = float.Parse(coordinates[1]);
                rot = float.Parse(coordinates[3]);
                if(player != null){
                    player.Call(GetObjectName(coordinates[2]), new Vector3(x, 0, z), rot, false);
                }
            }
            MissionProver.buildOnDB = false;
        }
        
        
        /// <summary>
        /// Transforms a encrypted string into the name of the relevant gameobject
        /// </summary>
        /// <param name="code">Encrypted string</param>
        /// <returns>Name of the relevant gameobject</returns>
        /// @author Bastian Badde
        private string GetObjectName(string code)
        {
            string name;
            switch (code)
            {
                case "0":
                    name = "Straight270Final";
                    break;
                case "5":
                    name = "TunnelIn";
                    break;
                case "6":
                    name = "TunnelOut";
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

        /// <summary>
        /// Loads a Board-Object from the database and interpret it for the Boardbuilding
        /// </summary>
        /// @author Bastian Badde
        public void RetrieveFromDatabase()
        {
            RestClient.Get<Board>(connectionString + boardName + ".json").Then(response =>
            {
                Debug.Log("BoardData retrieved");
                board = response;
                BuildFromDB();
                Debug.Log("Register All on DB");
                player.RegisterAll();
            });
        }
        
        /// <summary>
        /// Loads a Board-Object from the database and interpret it for the Missionbuilding
        /// </summary>
        /// @author Bastian Badde
        public void RetrieveFromDatabaseForMission()
        {
            RestClient.Get<Board>(connectionString + boardName + ".json").Then(response =>
            {
                Debug.Log("MissionData retrieved");
                board = response;
                CreateMission();
            });
        }

        /// <summary>
        /// interprets the MissionString of the current seleted Board-object and displays it
        /// </summary> 
        /// missionInfo: Relevant data of the MissionString
        /// cargos: String-values of the missionInfo
        /// cargosInt: Cargos transformed into int-values
        /// @author
        private void CreateMission()
        {
            string missionInfo = board.missionString.Split(':')[1];
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
            prover.SetMission(new Mission(cargosInt));
            prover.switchValue.AddOptions(ddStrings);
            prover.whileSwitchValue.AddOptions(ddStrings);
        }

        /// <summary>
        /// Uploads a Board-Object to the database.
        /// </summary>
        /// <param name="board">Board-object to be uploaded</param>
        public void PostToDatabase(Board board)
        {
            RestClient.Put(connectionString +  boardName + ".json", board);
        }

        /// <summary>
        /// Initialises the MissionProver-object
        /// </summary>
        /// @author Bastian Badde
        void Start()
        {
            prover = FindObjectOfType<MissionProver>();
        }

    }
}
