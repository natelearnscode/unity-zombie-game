using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public float ammo = 50f;
    public float maxAmmo = 50f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioClip[] shootingAudioClips;
    public AudioClip[] reloadAudioClips;
    public GameObject impactEffect;

    public TextMeshProUGUI ammoCountUI;
    private float nextTimeToFire = 0f;
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = AudioManager.instance.sfxVolume;
        ammoCountUI.text = "Ammo: " + ammo;

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammo > 0 && !PauseMenu.GameIsPaused)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootingAudioClips[Random.Range(0, shootingAudioClips.Length)], AudioManager.instance.sfxVolume);

        muzzleFlash.Play();
        ammo--;
        // play muzzle flash here

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // we've shot something
            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
            // instantiate impact effect
            // destroy newly instantiated impact effect game object after a few seconds
        }
    }

    void Reload()
    {
        audioSource.clip = reloadAudioClips[Random.Range(0, reloadAudioClips.Length)];
        audioSource.Play();

        ammo = maxAmmo;
    }
}
