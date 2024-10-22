using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    private bool isAttacking;

    public float health = 100f;

    [Header("Attack Settings")]
    public float damage;
    public float maxChaseDistance;
    public float minAttackDistance = 1.5f;
    public float maxAttackDistance = 2.5f;

    [Header("Movement")]
    private float currentWanderTime;
    public float wanderWaitTime = 10f;
    public bool canMoveWhileAttacking;
    [Space]
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float wanderRange = 5f;




    public bool walk;
    public bool run;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentWanderTime = wanderWaitTime;
    }

    bool hasDied;

    private void Update()
    {
        if (health <= 0)
        {
            agent.SetDestination(transform.position);

            Destroy(agent);
            anim.SetTrigger("Die");



            GetComponent<GatherableObject>().enabled = true;
            Destroy(this);
            return;
        }


        UpdateAnimations();

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > maxChaseDistance)
                target = null;

            if (!isAttacking)
                Chase();
        }
        else
            Wander();
    }

    public void UpdateAnimations()
    {
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    // public void Wander()
    // {
    //     Debug.Log("Bear is Roaming");
    //     if (currentWanderTime >= wanderWaitTime)
    //     {
    //         Vector3 wanderPos = transform.position;

    //         wanderPos.x += Random.Range(-wanderRange, wanderRange);
    //         wanderPos.z += Random.Range(-wanderRange, wanderRange);

    //         currentWanderTime = 0;

    //         agent.speed = walkSpeed;

    //         agent.SetDestination(wanderPos);

    //         walk = true;
    //         run = false;
    //     }
    //     else
    //     {
    //         if (agent.isStopped)
    //         {
    //             Debug.Log("Bear is Idling");
    //             currentWanderTime += Time.deltaTime;

    //             walk = false;
    //             run = false;
    //         }
    //     }
    // }

    public void Wander()
    {

        if (currentWanderTime >= wanderWaitTime)
        {
            // Get a random position in the wander range
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += transform.position;

            NavMeshHit hit;
            // Sample a position on the NavMesh that is valid
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1))
            {
                Vector3 wanderPos = hit.position;

                // Reset the wander timer
                currentWanderTime = 0;

                // Make sure the agent is moving
                agent.isStopped = false;
                agent.speed = walkSpeed;
                agent.SetDestination(wanderPos);

                walk = true;
                run = false;
            }
            else
            {
                Debug.Log("Couldn't find a valid position to wander");
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                currentWanderTime += Time.deltaTime;

                // Stop walking when the bear has reached its destination
                walk = false;
                run = false;
            }
        }
    }

    public void Chase()
    {
        Debug.Log("Bear is Chasing You!");
        agent.SetDestination(target.transform.position);

        walk = false;

        run = true;

        agent.speed = runSpeed;

        if (Vector3.Distance(target.transform.position, transform.position) <= minAttackDistance && !isAttacking)
            StartAttack();
    }

    public void StartAttack()
    {
        isAttacking = true;

        if (!canMoveWhileAttacking)
            agent.SetDestination(transform.position);

        anim.SetTrigger("Attack");
    }

    public void FinishAttack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > maxAttackDistance)
            return;

        target.GetComponent<PlayerStats>().health -= damage;

        isAttacking = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            target = other.transform;
    }
}
