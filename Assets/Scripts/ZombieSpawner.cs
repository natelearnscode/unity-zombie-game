using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public float round = 1;
    public bool isActive = true;
    public float spawnerInterval = 10f;
    public GameObject zombie;
    public Transform zombieCountTracker;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnEnemy(spawnerInterval, zombie));

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnEnemy()
    {
        SpawnEnemy(zombie);
    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newEnemy.GetComponent<ZombieController>().enabled = true;
        newEnemy.GetComponent<NavMeshAgent>().enabled = true;
        newEnemy.tag = "Zombie";
        newEnemy.transform.parent = zombieCountTracker;
    }
}
