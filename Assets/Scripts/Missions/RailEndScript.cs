using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RailEndScript : MonoBehaviour
{
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
        //Debug.Log("------Collision with:" + other.name);
        if (_prover.mission.IsComplete()) _prover.SetFinalText("Gewonnen!!");
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
