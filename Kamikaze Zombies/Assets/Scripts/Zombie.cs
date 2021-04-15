using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Attached to Zombies
public class Zombie : MonoBehaviour
{
    public float range = 10f;
    public float explodingDistance = 1f;
    public Transform target;
    NavMeshAgent agent;
    Animator animator;

    public float eRadius = 5.0f;
    public float aoeRadius = 5.0f;

    public int health = 100;

    public ParticleSystem deathEffect;
    public GameObject deathEffect_GO;

    public GameObject killCounter;
    public GameObject player;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        float d = Vector3.Distance(target.position, transform.position);

        //Setting up animations with Navmesh
        if (d <= range)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("isRunning", true);
            if (d <= agent.stoppingDistance)
            {
                LookAtPlayer();
            }

        }
        else if (d > range)
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
        }

        //Killed
        if (health <= 0)
        {
            Death();
        }

        //KAMIKAZE!!
        if (d <= explodingDistance)
        {
            Death();
        }

    }

    //No collisions with players coz Sucide is triggerd by distance not colliders
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
            }
        
    }

    //Direction, faulty rn...Update to look in direction of movement instead of looking at player
    void LookAtPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    //Kill Zombie
    void Death()
    {
        killCounter.transform.GetComponent<KillCount>().killNumber++;
        deathEffect_GO.SetActive(true);
        GameObject deathEffext = Instantiate(deathEffect_GO, transform.position, Random.rotation);
        DamagePlayer();
        Destroy(gameObject.gameObject);
    }

    //Simulating Explosion Knockback and Damage
    void DamagePlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, eRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb == player.GetComponent<Rigidbody>())
            {
                Vector3 relativePos = gameObject.transform.position - player.transform.position;
                
                float proximity = relativePos.magnitude;
                float effect = 1 - (proximity / aoeRadius);

                int damage = (int)System.Math.Round(effect);
                player.transform.GetComponent<PlayerHealth>().Damage(damage);
            }
        }
    }
}
