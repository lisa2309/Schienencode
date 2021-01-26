
using UnityEngine;

/// <summary>
/// 
/// @author 
/// </summary>
public class StationScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private int _stationNumber;

    /// <summary>
    /// 
    /// </summary>
    private MissionProver _prover;

    /// <summary>
    /// 
    /// @author
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("+++++++++++Collision with:" + other.name);
        _prover.RaiseCounter(_stationNumber);
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>
    void Start()
    {
        _prover = FindObjectOfType<MissionProver>();
        this._stationNumber = _prover.RegisterNewStation();
    }
}
