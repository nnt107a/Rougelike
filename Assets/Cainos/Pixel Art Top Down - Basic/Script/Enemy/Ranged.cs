using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Ranged : Enemy
{
    [Header("Ranged")]
    [SerializeField] private float fleeRange;
    [SerializeField] private float fleeDistance;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private ProjectilesSpawner projectilesSpawner;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updatePosition = true;

        gameObject.SetActive(false);
    }
    protected override void Update()
    {
        if (dead)
        {
            return;
        }
        base.Update();

        if (gameObject.activeInHierarchy)
        {
            if (!fleeing)
            {
                navMeshAgent.stoppingDistance = stoppingDistance;
                navMeshAgent.destination = playerTransform.position;
            }
            else
            {
                navMeshAgent.stoppingDistance = 0;
                Vector3 temp = new Vector3(transform.position.x - playerTransform.position.x,
                                           transform.position.y - playerTransform.position.y,
                                           transform.position.z - playerTransform.position.z);
                temp = temp.normalized * fleeRange;
                navMeshAgent.destination = transform.position + temp;
            }
        }

        Vector3 range = new Vector3(transform.position.x - playerTransform.position.x,
                                    transform.position.y - playerTransform.position.y,
                                    transform.position.z - playerTransform.position.z);

        bool playerInRange = range.magnitude <= attackRange;

        if (range.magnitude < fleeDistance)
        {
            fleeing = true;
        }
        else
        {
            fleeing = false;
        }

        if (!hit)
        {
            if (!attacking && !fleeing && HasReachedDestination())
            {
                anim.Play("idle");
            }
            else if (fleeing || !HasReachedDestination())
            {
                anim.Play("walk");
            }
        }

        if (playerTransform.position.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                                               transform.localScale.z);
        }
        else if (playerTransform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                                               transform.localScale.z);
        }

        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            if (playerInRange && !hit)
            {
                Attack1();
                attackTimer = 0;
            }
        }

    }
    private bool HasReachedDestination()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void ActivateAnim()
    {
        if (currentHealth > 0)
        {
            hit = true;
            anim.Play("hurt");
            StartCoroutine(IFrame());
        }
        else
        {
            dead = true;
            navMeshAgent.updatePosition = false;
            anim.Play("death");
        }
    }
    private IEnumerator IFrame()
    {
        Physics2D.IgnoreLayerCollision(10, 6, true);
        Physics2D.IgnoreLayerCollision(10, 7, true);
        yield return new WaitForSeconds(iFrameTime);
        Physics2D.IgnoreLayerCollision(10, 6, false);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        hit = false;
    }
    private void Attack1()
    {
        attacking = true;
        anim.Play("attack");
        StartCoroutine(AttackTimer());
    }
    public void Attack()
    {
        Vector3 targetPosition = playerTransform.position;

        projectilesSpawner.FindProjectiles().transform.position = transform.position;
        Vector2 dir = new Vector2(targetPosition.x - transform.position.x,
                                  targetPosition.y - 0.2f - transform.position.y);
        dir.Normalize();

        projectilesSpawner.FindProjectiles().GetComponent<Arrow>().Activate(dir, attackDamage);
    }
    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(48f / 60);
        attacking = false;
    }
    public override void Deactivate()
    {
        base.Deactivate();
        gameObject.SetActive(false);
    }
    public override Vector2 CalcSpawnPos()
    {
        return new Vector2(playerTransform.position.x + (Random.Range(0, 2) == 0 ? 1 : -1) * Random.Range(minSpawnDistance * 100, maxSpawnDistance * 100) * 1.0f / 100,
                           playerTransform.position.y + (Random.Range(0, 2) == 0 ? 1 : -1) * Random.Range(minSpawnDistance * 100, maxSpawnDistance * 100) * 1.0f / 100);
    }
    public override void Spawn(Vector2 position)
    {
        gameObject.SetActive(true);
        hit = false;
        dead = false;
        currentHealth = startingHealth;
        attackTimer = attackCooldown;
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        transform.position = position;
    }
}
