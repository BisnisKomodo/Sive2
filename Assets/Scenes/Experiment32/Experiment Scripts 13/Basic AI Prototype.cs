using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIPrototype : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    private bool isAttacking;
    public float maxhealth = 100f;
    public float health = 100f;
    public bool isDead = false;

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


    [SerializeField] private float knockbackFlyForce = 5f;
    [SerializeField] private float knockbackPushForce = 4f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentWanderTime = wanderWaitTime;
    }

    // private void Update()
    // {
    //     if (health <= 0)
    //     {
    //         agent.SetDestination(transform.position);

    //         Destroy(agent);
    //         anim.SetTrigger("Die");



    //         GetComponent<GatherableObject>().enabled = true;
    //         Destroy(this);
    //         return;
    //     }


    //     UpdateAnimations();

    //     if (target != null)
    //     {
    //         if (Vector3.Distance(target.transform.position, transform.position) > maxChaseDistance)
    //             target = null;

    //         if (!isAttacking)
    //             Chase();
    //     }
    //     else
    //         Wander();
    // }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            HandleDeath();
            return;
        }

        if (!isDead)
        {
            GetComponent<GatherableObject>().enabled = false;
            UpdateAnimations();

            if (health < maxhealth && target == null)
            {
                target = FindObjectOfType<PlayerStats>().transform;
                Chase();
            }
            // Chasing and wandering logic for when the bear is alive
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
    }

    private void HandleDeath()
    {
        isDead = true;
        agent.SetDestination(transform.position);
        Destroy(agent); // Destroy the NavMeshAgent so the bear stops moving
        anim.SetTrigger("Die");

        // Enable gathering once the bear is dead
        GetComponent<GatherableObject>().enabled = true;
        // Disable AI script to stop it from running further
        this.enabled = false;
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
                //Debug.Log("Couldn't find a valid position to wander");
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

    // public void Chase()
    // {
    //     Debug.Log("Bear is Chasing You!");
    //     agent.SetDestination(target.transform.position);

    //     walk = false;

    //     run = true;

    //     agent.speed = runSpeed;

    //     if (Vector3.Distance(target.transform.position, transform.position) <= minAttackDistance && !isAttacking)
    //         StartAttack();
    // }

    // public void Chase()
    // {
    //     if (target == null)
    //     {
    //         //Debug.Log("No target to chase.");
    //         return;
    //     }

    //     //Debug.Log("Bear is Chasing You!");
    //     agent.isStopped = false; // Ensure NavMeshAgent is not stopped
    //     agent.SetDestination(target.transform.position);

    //     walk = false;
    //     run = true;

    //     agent.speed = runSpeed;

    //     // Check if it's within attack range and ready to attack
    //     float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
    //     if (distanceToTarget <= minAttackDistance && !isAttacking)
    //     {
    //         StartAttack();
    //     }

    //     if (agent == null)
    //     {
    //         agent.isStopped = true;
    //     }
    // }

    public void Chase()
    {
        if (target == null)
        {
            //Debug.Log("No target to chase.");
            return;
        }

        //Debug.Log("Bear is Chasing You!");
        if (agent != null) // Check if agent exists before using it
        {
            agent.isStopped = false; // Ensure NavMeshAgent is not stopped
            agent.SetDestination(target.transform.position);

            walk = false;
            run = true;

            agent.speed = runSpeed;

            // Check if it's within attack range and ready to attack
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget <= minAttackDistance && !isAttacking)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, minAttackDistance))
            {
                BuildingHealth barricade = hit.collider.GetComponent<BuildingHealth>();
                if (barricade != null)
                {
                    StartAttack(); // Attack the barricade
                    return;
                }
            }

            StartAttack(); // Attack the target if no barricade is in the way
        }
        }
        // else
        // {
        //     //Debug.LogWarning("NavMeshAgent is missing or destroyed!");
        // }
    }

    public void StartAttack()
    {
        isAttacking = true;

        if (!canMoveWhileAttacking)
            agent.SetDestination(transform.position);

        anim.SetTrigger("Attack");
    }

    // public void FinishAttack()
    // {
    //     if (Vector3.Distance(target.transform.position, transform.position) > maxAttackDistance)
    //         return;

    //     target.GetComponent<PlayerStats>().health -= damage;

    //     isAttacking = false;

    // }

    public void FinishAttack()
    {
        // Ensure that the attack has finished before checking distance
        isAttacking = false;

        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        // Check if the player is still within attack range
        if (distanceToTarget > maxAttackDistance)
        {
            Chase(); // Resume chasing if the player moved out of range
        }
        else
        {
            // Deal damage if still within range
            //target.GetComponent<PlayerStats>().health -= damage;

            PlayerStats playerStats = target.GetComponent<PlayerStats>();
            if (playerStats != null)
            {

                if (playerStats.armor > 0)
                {
                    playerStats.armor -= damage;
                    //if armor is gone, damage goes to health
                    if (playerStats.armor < 0)
                    {
                        playerStats.health += playerStats.armor; // Add because armor is negative
                        playerStats.armor = 0;
                    }
                }
                else
                {
                    playerStats.health -= damage;
                }

                //playerStats.health -= damage; // Directly decrease health
                playerStats.redOverlay.gameObject.SetActive(true);
                StartCoroutine(playerStats.FadeOverlayOut()); // Call the red overlay fade function


                //Knockback function
                PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    Vector3 knockbackDirection = (target.transform.position - transform.position).normalized;

                    knockbackDirection.y = knockbackFlyForce;
                    playerMovement.isKnockedback = true;
                    playerMovement.knockbackDirection = knockbackDirection * knockbackPushForce; // Pass the knockback direction
                }
            }


            // Look for barricades if the target is blocked
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, maxAttackDistance))
        {
            BuildingHealth barricade = hit.collider.GetComponent<BuildingHealth>();
            if (barricade != null)
            {
                barricade.TakeDamage(damage); 
                return; 
            }
        }

        Chase(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            target = other.transform;
        }
    }
}