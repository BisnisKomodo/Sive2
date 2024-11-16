using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BasicAIFlee : MonoBehaviour
{
    public Image redOverlay;
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    private bool isAttacking;

    public float health = 100f;
    private bool isDead = false;

    [Header("Attack Settings")]
    public float damage;
    public float maxFleeDistance = 10f; // Adjust this to determine how far the AI should run
    public float minFleeDistance = 5f;  // Adjust this for how close before AI flees

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

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            HandleDeath();
            return;
        }

        if (!isDead)
        {
            UpdateAnimations();
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(target.position, transform.position);
                if (distanceToTarget < minFleeDistance)
                {
                    Flee();
                }
                else
                {
                    Wander();
                }
            }
            else
            {
                Wander();
            }
        }
    }

    private void HandleDeath()
    {
        isDead = true;
        agent.SetDestination(transform.position);
        Destroy(agent);
        anim.SetTrigger("Die");

        GetComponent<GatherableObject>().enabled = true;
        this.enabled = false;
    }

    public void UpdateAnimations()
    {
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    public void Flee()
    {
        if (target == null)
            return;

        Debug.Log("AI is fleeing from the player!");

        // Calculate direction away from target
        Vector3 fleeDirection = transform.position - target.position;
        Vector3 fleeDestination = transform.position + fleeDirection.normalized * maxFleeDistance;

        // Ensure fleeDestination is within the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeDestination, out hit, maxFleeDistance, NavMesh.AllAreas))
        {
            fleeDestination = hit.position; // Use the valid NavMesh position
            agent.SetDestination(fleeDestination);
            agent.speed = runSpeed; // Ensure the agent is set to run speed

            walk = false;
            run = true;
        }
        else
        {
            Debug.LogWarning("Flee destination is not on the NavMesh!");
        }
    }

    public void Wander()
    {
        if (currentWanderTime >= wanderWaitTime)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1))
            {
                Vector3 wanderPos = hit.position;

                currentWanderTime = 0;
                agent.isStopped = false;
                agent.speed = walkSpeed;
                agent.SetDestination(wanderPos);

                walk = true;
                run = false;
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                currentWanderTime += Time.deltaTime;
                walk = false;
                run = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            target = other.transform;
            Debug.Log("Player detected!");
        }
    }
}
