using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public float health = 100f;
    public float maxHealth = 100f;
    public float maxInvicibilityCoolDown = 1f;
    public AudioClip[] takeDamageAudioClips;
    public AudioClip deathAudio;

    public GameObject[] weapons;
    public Slider slider;

    private int currentWeapon = 0;

    private float invicibilityCoolDown;
    private AudioSource damageAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        invicibilityCoolDown = maxInvicibilityCoolDown;
        damageAudioSource = gameObject.AddComponent<AudioSource>();
        damageAudioSource.loop = false;
        damageAudioSource.playOnAwake = false;
        damageAudioSource.volume = AudioManager.instance.sfxVolume;

    }

    // Update is called once per frame
    void Update()
    {
        damageAudioSource.volume = AudioManager.instance.sfxVolume;
        invicibilityCoolDown -= Time.deltaTime;

        slider.value = health / maxHealth;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // switch active weapon
            weapons[currentWeapon].SetActive(false);
            weapons[0].SetActive(true);
            currentWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[currentWeapon].SetActive(false);
            weapons[1].SetActive(true);
            currentWeapon = 1;
        }
    }

    public void TakeDamage(float amount)
    {
        if(invicibilityCoolDown < 0)
        {
            if (!damageAudioSource.isPlaying)
            {
                damageAudioSource.clip = takeDamageAudioClips[Random.Range(0, takeDamageAudioClips.Length)];
                damageAudioSource.Play();
            }

            health -= amount;
            if (health < 0f)
            {
                damageAudioSource.clip = deathAudio;
                damageAudioSource.Play();
                
                // player is dead
                health = 0f;
            }
            invicibilityCoolDown = maxInvicibilityCoolDown;
        }

    }
}
