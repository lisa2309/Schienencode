using System;
using System.Collections.Generic;
using DefaultNamespace;
using Proyecto26;
using UnityEngine;

namespace Database
{
    public class DatabaseConnector : MonoBehaviour
    {
        public String boardName;
        public int boardNumber = 0;

        private String connectionstring = "https://schienencode-default-rtdb.europe-west1.firebasedatabase.app/";
        
        
        public void OnSubmit()
        {
            boardNumber++;
            List<BoardComponent> list = new List<BoardComponent>();
            ushort i = 0;
            while (i < 64)
            {
                if (i % 4 == 0)
                {
                    list.Add(new Tunnel(i, Convert.ToUInt16(i/4)));
                }
                else
                {
                    list.Add(new BoardComponent(i));
                }
                //list.Add(new BoardComponent(i));
                i++;
            }
            Debug.Log("In Submit new new!!!");
            Debug.Log("List: " + list.ToString());
            i = 0;
            foreach (var com in list)
            {
                // Debug.Log("Comp " + list[i].Identification);
                // if (com is Tunnel) Debug.Log("CompT " + ((Tunnel) list[i]).PairID);
                //if (com is Tunnel) RestClient.Put(connectionstring + "board" + boardNumber + "/c " + i + ".json", com);
                //else 
                RestClient.Put(connectionstring + "board" + boardNumber + "/c " + i + ".json", com);
                i++;
            }
            
            //RestClient.Put(connectionstring + boardNumber + ".json", new BoardComponent(Convert.ToUInt16(boardNumber)));
            //RestClient.Put(connectionstring +  boardNumber + ".json", new Board(list.ToArray()));
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
