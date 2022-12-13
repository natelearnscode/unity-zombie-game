using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float round = 0;
    public float maxRoundCoolDown = 5f;
    public GameObject[] spawners;
    public TextMeshProUGUI roundTextElem;
    public AudioSource newRoundSound;

    public float numberOfEnemiesAlive;
    public Transform zombieCountTracker;

    private float roundCoolDown = 5f;
    private float spawnCoolDown = 5f;
    private float maxSpawnCoolDown = 5f;

    private bool isInCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        roundCoolDown = maxRoundCoolDown/2;
        isInCoolDown = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(isInCoolDown)
        {
            roundTextElem.text = "Next Round in " + roundCoolDown.ToString("F0") + " sec";
            roundCoolDown -= Time.deltaTime;

            if(roundCoolDown < 0)
            {
                spawnCoolDown = maxSpawnCoolDown;
                isInCoolDown = false;
                NextWave();
            }

        }
        else
        {
            roundTextElem.text = "Round: " + round.ToString();
            spawnCoolDown -= Time.deltaTime;

            numberOfEnemiesAlive = zombieCountTracker.childCount;
            if (numberOfEnemiesAlive <= 0 && spawnCoolDown < 0)
            {
                roundCoolDown = maxRoundCoolDown;
                isInCoolDown = true;
            }

        }


    }

    void NextWave()
    {
        round++;
        newRoundSound.Play();
        Debug.Log("Starting Next Round");
        float enemiesToBeSpawned = round * 2;
        float leftToBeSpawned = enemiesToBeSpawned;
        for(int i = 0; i < enemiesToBeSpawned; i++)
        {
            Invoke("SpawnEnemy", i/2.0f);
        }

        numberOfEnemiesAlive = enemiesToBeSpawned;
    }

    void SpawnEnemy()
    {
        int randomSpawer = Random.Range(0, spawners.Length - 1);
        spawners[randomSpawer].GetComponent<ZombieSpawner>().SpawnEnemy();
    }
}
