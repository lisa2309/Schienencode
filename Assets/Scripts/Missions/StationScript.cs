
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
    /// </summary>
    private GameObject panels;

    public int cargoCount;


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
    /// Destroys the object attached to this script as soon as you left click on it 
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void OnMouseDown()
    {
        Debug.Log("is clicked ");
        //if (!isLocalPlayer) return;
        if (!MissionProver.deleteOn && !MissionProver.panelisOpen)
        {
            _prover.currentStation = this._stationNumber;
            OpenPanel();
        }
    }

    
    public void OpenPanel()
    {
        panels = GameObject.FindObjectOfType<Panels>().allpanels;
        if (panels != null)
        {
            foreach (Transform panel in panels.GetComponentInChildren<Transform>())
            {

                if (panel.name != "panel01")
                {
                    panel.gameObject.SetActive(false);
                }
                else
                {
                    if (!panel.gameObject.activeSelf)
                    {
                        MissionProver.panelisOpen = true;
                        panels.SetActive(true);
                        panel.gameObject.SetActive(true);
                    }
                }
            }
            _prover.UpdateStationSettings();

        }
    }

    /// <summary>
    /// 
    /// @author
    /// </summary>
    void Start()
    {
        cargoCount = 1;
        _prover = FindObjectOfType<MissionProver>();
        this._stationNumber = _prover.RegisterNewStation();
        //popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel") as Panel;
        //popUpPanel = GameObject.Find("PopUpPanel");
    }
}
