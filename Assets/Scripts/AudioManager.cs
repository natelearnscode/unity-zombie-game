using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0, 1)]
    public float gloabalVolume;

    [Range(0, 1)]
    public float musicVolume;
    
    [Range(0, 1)]
    public float sfxVolume;

    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = gloabalVolume;
    }
}
