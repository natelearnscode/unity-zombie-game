using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public float damage = 10f;
    public AudioClip[] walkAudioClips;
    public AudioClip[] attackAudioClips;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource walkAudioSource;
    private AudioSource voiceAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        walkAudioSource = audioSources[0];
        voiceAudioSource = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Idle Walk Run", speedPercent);
        if ((Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance))
        {
            AttackPlayer();
            animator.SetBool("isAttacking", true);

            //if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            //{
            //    AttackPlayer();
            //    animator.SetBool("isAttacking", true);
            //}
            //else
            //{
            //    animator.SetBool("isAttacking", false);
            //}
        }
        else
        {
            if (!walkAudioSource.isPlaying && !PauseMenu.GameIsPaused)
            {
                walkAudioSource.clip = walkAudioClips[Random.Range(0, walkAudioClips.Length)];
                walkAudioSource.volume = 0.2f;
                walkAudioSource.Play();
            }
            animator.SetBool("isAttacking", false);
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking Player");
        if(!voiceAudioSource.isPlaying && !PauseMenu.GameIsPaused)
        {
            voiceAudioSource.clip = attackAudioClips[Random.Range(0, attackAudioClips.Length)];
            voiceAudioSource.Play();
        }

        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        playerManager.TakeDamage(damage);
    }
}
