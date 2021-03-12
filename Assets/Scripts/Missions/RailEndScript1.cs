using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RailEndScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private MissionProver prover;

    /// <summary>
    /// 
    /// @author
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("------Collision with:" + other.name);
        if (prover.mission.IsComplete()) prover.SetFinalText("Gewonnen!!");
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>    
    void Start()
    {
        prover = FindObjectOfType<MissionProver>();
    }
}
