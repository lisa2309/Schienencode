using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Music Volume Setting.
/// </summary>
/// @author Ronja Haas & Anna-Lisa Müller

public class MusicPlayer : MonoBehaviour
{

    public AudioSource audioSource;
    public static float musicVolume = 0.5f;

    /// <summary>
    /// Start Music when play mode is active.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void Start()
    {
        audioSource.Play();
    }
	
	/// <summary>
    /// Update the Volume when the slider is changed.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
	public void UpdateVolume(float volume)
	{
		audioSource.volume = volume;
	}

}