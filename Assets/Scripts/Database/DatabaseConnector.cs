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
        // /// <summary>
        // /// 
        // /// </summary>
        // private ObjectPlacer placer;

        /// <summary>
        /// the Player Object of the active Player
        /// </summary>
        public Player player=null;

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
            Debug.Log("Posted...");
            //placer = FindObjectOfType<ObjectPlacer>();
            //PostToDatabase(new Board("Board:2.6.0.0;6.6.0.0;10.6.0.0"));
            //PostToDatabase(new Board("Board:6.6.0.0;10.6.0.0;16.6.10.0;16.12.10.270;12.12.0.180;8.12.0.180;2.12.10.180;2.6.10.90"));
            //PostToDatabase(new Board("Board:26.14.20.90;22.14.0.180;21.12.30.0;17.12.30.0;18.14.0.180;14.14.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:18.14.20.90;14.14.0.180;13.12.30.0;9.12.30.0;10.14.0.180;6.14.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:202.56.20.90;198.56.0.180;197.54.30.0;193.54.30.0;194.56.0.180;190.56.21.90", "Mission:5;5"));
            //PostToDatabase(new Board("Board:202.56.20.90;198.56.30.180;194.56.30.180;190.56.21.90", "Mission:5;5"));
            // PostToDatabase(new Board("Board:206.56.20.90;198.56.30.180;194.56.30.180;204.50.21.270;188.50.11.180;200.50.15.180;" +
            //                          "196.50.0.0;192.50.18.0;202.56.15.0;190.56.17.180;184.56.10.90;184.62.10.180;202.62.10.270;" +
            //                          "188.62.0.0;192.62.0.0;196.62.0.0;194.44.11.180;200.44.11.90", "Mission:5;10"));
            //PostToDatabase(new Board("Board:44.26.0.0", "Mission:"));
            // PostToDatabase(new Board("Board:56.26.20.90;48.26.30.180;44.26.30.180;54.20.21.270;38.20.11.180;50.20.15.180;" +
            //                          "46.20.0.0;42.20.18.0;52.26.15.0;40.26.17.180;34.26.10.90;34.32.10.180;52.32.10.270;" +
            //                          "38.32.0.0;42.32.0.0;46.32.0.0;44.14.11.180;50.14.11.90", "Mission:5;10"));
            PostToDatabase(new Board("Board:26.12.20.270;30.12.0.0;38.12.30.0;44.18.11.0;40.18.0.180;34.18.11.270;34.12.16.180;" +
                                     "46.12.0.0;54.12.0.0;62.12.0.0;68.12.11.90;68.18.11.0;64.18.0.180;60.18.18.180;56.18.30.180;" +
                                     "50.18.11.270;28.30.21.90;58.22.0.270;58.12.0.0;58.28.10.180;64.28.10.270;64.22.11.180;" +
                                     "68.22.30.0;72.22.0.0;78.22.11.90;78.26.0.270;78.30.30.270;78.36.11.0;74.36.0.180;" +
                                     "70.36.0.180;66.36.0.180;62.36.0.180;58.36.0.180;54.36.0.180;46.36.30.180;42.36.17.180;" +
                                     "40.32.17.90;40.28.0.90;40.22.11.180;44.22.30.0;42.12.17.0;50.12.16.180;50.22.11.90;50.26.0.270;" +
                                     "44.30.30.0;50.30.16.90;50.36.16.0;36.36.11.270;36.30.10.0;32.30.0.180", "Mission:10;6;4;5;12;1;8"));
            //PostToDatabase(new Board("Board:40.16.20.270;46.16.11.90;46.22.10.180;50.22.0.0;54.22.30.0;58.22.0.0;64.22.5.270;" +
            //                         "40.-38.6.90;62.-32.21.270;46.-38.11.90;46.-32.10.180;50.-32.30.0;54.-32.0.0;58.-32.0.0", "Mission:4;3"));

            //marin
            //PostToDatabase(new Board("Board:40.16.20.270;46.16.11.90;46.22.10.180;50.22.0.0;54.22.30.0;58.22.0.0;64.22.5.270;" +
            //                         "40.-38.6.90;62.-32.21.270;46.-38.11.90;46.-32.10.180;50.-32.30.0;54.-32.0.0;58.-32.0.0", "Mission:4;3"));

            //snow
            // PostToDatabase(new Board("Board:32.16.20.270;64.22.5.270;36.16.16.180;40.16.30.0;44.16.17.0;46.22.11.0;36.22.11.270;" +
            //                          "42.22.0.180;50.16.11.90;50.22.10.180;54.22.0.0;58.22.0.0;" + 
            //                          "32.-34.6.90;62.-28.21.270;36.-34.16.180;40.-34.30.0;44.-34.17.0;46.-28.11.0;36.-28.11.270;" +
            // "42.-28.0.180;50.-34.11.90;50.-28.10.180;54.-28.0.0;58.-28.0.0", "Mission:10;9"));
            
            //map3
            // PostToDatabase(new Board("Board:32.16.20.270;36.16.0.0;42.16.11.90;42.20.18.270;46.22.30.0;50.22.0.0;" +
            //                          "42.24.0.270;42.30.5.180;56.22.5.270;" + 
            //     "32.-44.6.90;36.-44.0.0;42.-44.11.90;42.-40.18.270;46.-38.0.0;50.-38.0.0;" +
            //                          "42.-36.30.270;42.-32.21.180;54.-38.21.270;", "Mission:3;5"));
            
            //map4
            // PostToDatabase(new Board("Board:56.26.20.90;48.26.30.180;56.20.5.270;50.20.15.180;" +
            //                          "46.20.30.0;42.20.18.0;52.26.15.0;40.26.17.180;34.26.10.90;34.32.10.180;52.32.10.270;" +
            //                          "38.32.0.0;42.32.0.0;50.14.11.90;" + 
            //                          "56.-34.6.270;48.-34.30.180;44.-34.0.180;54.-40.21.270;50.-40.15.180;" +
            //                          "46.-40.30.0;42.-40.18.0;52.-34.15.0;40.-34.17.180;52.-28.10.270;" +
            //                          "46.-28.0.0;44.-46.11.180", "Mission:6;1;9;0"));
            // PostToDatabase(new Board("Board:56.26.20.90;48.26.30.180;44.26.0.180;56.20.5.270;38.20.11.180;50.20.15.180;" +
            //                          "46.20.30.0;42.20.18.0;52.26.15.0;40.26.17.180;34.26.10.90;34.32.10.180;52.32.10.270;" +
            //                          "38.32.0.0;42.32.0.0;46.32.0.0;44.14.11.180;50.14.11.90;" + 
            //                          "56.-34.6.270;48.-34.30.180;44.-34.0.180;54.-40.21.270;38.-40.11.180;50.-40.15.180;" +
            //                          "46.-40.30.0;42.-40.18.0;52.-34.15.0;40.-34.17.180;34.-34.10.90;34.-28.10.180;52.-28.10.270;" +
            //                          "38.-28.0.0;42.-28.0.0;46.-28.0.0;44.-46.11.180;50.-46.11.90", "Mission:6;1;9;0"));
            
            //map5
            // PostToDatabase(new Board("Board:26.12.20.270;26.30.5.90;36.20.30.270;46.22.30.90;54.22.30.90;" +
            //                          "26.-48.6.90;28.-30.21.90;36.-40.30.270;46.-38.30.90;54.-38.30.90", "Mission:16;5;6;25;12;1"));
            
            //map6
            // PostToDatabase(new Board("Board:26.12.20.270;30.12.0.0;56.18.30.180;38.12.30.0;26.30.5.90;46.36.30.180;" +
            //                          "44.22.30.0;42.12.17.0;44.30.30.0;50.30.16.90;32.30.0.180;" +
            //                          "26.-48.6.90;30.-48.0.0;56.-42.30.180;38.-48.30.0;28.-30.21.90;46.-24.30.180;" +
            //                          "44.-38.30.0;42.-48.17.0;44.-30.30.0;50.-30.16.90;32.-30.0.180", 
            //     "Mission:10;9;12;1;8" + "12;6;9;4;10"));
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
            RestClient.Get<Board>(connectionString + boardname + ".json").Then(response =>
            {
                Debug.Log("Data retrieved");
                board = response;
                //CreateMission();
                BuildFromDB();
            });
        }
        
        /// <summary>
        /// Loads a Board-Object from the database and interpret it for the Missionbuilding
        /// </summary>
        /// @author Bastian Badde
        public void RetrieveFromDatabaseForMission()
        {
            RestClient.Get<Board>(connectionString + boardname + ".json").Then(response =>
            {
                Debug.Log("Data retrieved");
                board = response;
                CreateMission();
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
