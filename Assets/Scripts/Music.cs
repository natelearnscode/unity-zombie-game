using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music.volume = AudioManager.instance.musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = AudioManager.instance.musicVolume;
    }
}
