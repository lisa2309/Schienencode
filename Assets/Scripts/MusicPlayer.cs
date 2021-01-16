using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioSource AudioSource;
    public static float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //leiser
        if (Input.GetKey(KeyCode.O))
        {
            musicVolume -= 0.03f;
        }

        //lauter
        if (Input.GetKey(KeyCode.P))
        {
            musicVolume += 0.03f; 
        }
        AudioSource.volume = musicVolume;
    }

}