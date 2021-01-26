using UnityEngine;


    public class RailEndScript : MonoBehaviour
    {
        private MissionProver _prover;

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("------Collision with:" + other.name);
            if (_prover.mission.IsComplete()) _prover.SetFinalText("Gewonnen!!");
        }

        void Start()
        {
            _prover = FindObjectOfType<MissionProver>();
        }
    }
