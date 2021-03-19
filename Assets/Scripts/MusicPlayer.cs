using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* created by: SWT-P_WS_2021_Schienencode */
/// <summary>
/// Music Volume Setting.
/// </summary>
/// @source: https://www.youtube.com/watch?v=-xvoJ7Q4vw0
/// Modified by: Ronja Haas & Anna-Lisa Müller
public class MusicPlayer : MonoBehaviour
{
    /// <summary>
    /// This is the audioSource file with the music
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Start Music when play mode is active.
    /// </summary>
    /// @author Ronja Haas & Anna-Lisa Müller 
    void Start()
    {
        audioSource.Play();
        audioSource.volume = 0.2f;
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