using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BasicAIPrototype : MonoBehaviour
{
    public Image redOverlay;
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    public GameObject projectile;
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
    public bool isDead = false;

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
        if (target == null)
        {
            Debug.Log("No target to chase.");
            return;
        }

        Debug.Log("Bear is Chasing You!");
        agent.isStopped = false; // Ensure NavMeshAgent is not stopped
        agent.SetDestination(target.transform.position);

        walk = false;
        run = true;

        agent.speed = runSpeed;

        // Check if it's within attack range and ready to attack
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget <= minAttackDistance && !isAttacking)
        {
            StartAttack();
        }
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
            Instantiate(projectile);
            redOverlay.gameObject.SetActive(true);
            StartCoroutine(FadeOutOverlay());
            Chase(); // Immediately resume chasing after attacking
        }
    }

    private IEnumerator FadeOutOverlay()
    {
        Color color = redOverlay.color;
        color.a = 0.3f;
        redOverlay.color = color;

        //Fade out overtime
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime/fadeDuration);
            redOverlay.color = color;
            yield return null;
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