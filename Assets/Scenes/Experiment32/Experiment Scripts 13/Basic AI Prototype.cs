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
    private bool isAttacking;

    public float health = 100f;
    public float damage;
    public float maxChaseDistance;
    public float minAttackDistance = 1.5f;
    public float maxAttackDistance = 2.5f;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float wanderRange = 5f;
    public float wanderWaitTime = 10f;
    private float currentWanderTime;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float jumpDuration = 1.5f;
    public float obstacleDetectionDistance = 2f;

    private bool walk;
    private bool run;
    private bool isJumping = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentWanderTime = wanderWaitTime;
    }

    private void Update()
    {
        if (health <= 0)
        {
            HandleDeath();
            return;
        }

        UpdateAnimations();

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (distanceToTarget > maxChaseDistance)
            {
                target = null;
            }
            else if (!isJumping && !isAttacking)
            {
                if (ShouldJump())
                {
                    StartCoroutine(JumpOverObstacle());
                }
                else
                {
                    Chase();
                }
            }
        }
        else
        {
            Wander();
        }
    }

    private bool ShouldJump()
    {
        if (Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            return true;
        }
        return false;
    }

    private IEnumerator JumpOverObstacle()
    {
        isJumping = true;
        agent.isStopped = true;
        
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 jumpTarget = startPosition + Vector3.up * jumpHeight;

        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(startPosition, jumpTarget, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startPosition = transform.position;
        jumpTarget = new Vector3(startPosition.x, 0, startPosition.z);

        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(startPosition, jumpTarget, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isJumping = false;
        agent.isStopped = false;
    }

    public void Chase()
    {
        if (target == null) return;

        agent.SetDestination(target.position);
        agent.speed = runSpeed;

        walk = false;
        run = true;

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= minAttackDistance && !isAttacking)
        {
            StartAttack();
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

    private void HandleDeath()
    {
        agent.isStopped = true;
        anim.SetTrigger("Die");
        this.enabled = false;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
    }

    public void StartAttack()
    {
        isAttacking = true;
        agent.isStopped = true;
        anim.SetTrigger("Attack");
    }

    public void FinishAttack()
    {
        isAttacking = false;
        Chase();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            target = other.transform;
        }
    }
}