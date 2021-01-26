
using UnityEngine;


public class StationScript : MonoBehaviour
{
    private int _stationNumber;
    private MissionProver _prover;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("+++++++++++Collision with:" + other.name);
        _prover.RaiseCounter(_stationNumber);
    }

    void Start()
    {
        _prover = FindObjectOfType<MissionProver>();
        this._stationNumber = _prover.RegisterNewStation();
    }
}
