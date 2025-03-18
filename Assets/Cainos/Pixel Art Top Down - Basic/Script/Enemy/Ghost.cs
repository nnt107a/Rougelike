using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Ghost : Enemy
{
    [Header("Ghost")]
    [SerializeField] private float fleeRange;
    [SerializeField] private float fleeDistance;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private ProjectilesSpawner projectilesSpawner;
    private string horSide;
    private string verSide;
    private SpriteRenderer spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        range = range.normalized;

        verSide = "";
        horSide = "";

        if (range.x > 1f / 3)
        {
            verSide = "W";
        }
        else if (range.x < -1f / 3)
        {
            verSide = "E";
        }
        
        if (range.y > 1f / 3)
        {
            horSide = "S";
        }
        else if (range.y < -1f / 3)
        {
            horSide = "N";
        }

        if (!attacking)
        {
            anim.Play("idle_" + horSide + verSide);
        }

        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            if (playerInRange && !hit)
            {
                Attack1(verSide, horSide);
                attackTimer = 0;
            }
        }

    }
    public override void ActivateAnim()
    {
        if (currentHealth > 0)
        {
            hit = true;
            StartCoroutine(IFrame());
        }
        else
        {
            dead = true;
            navMeshAgent.updatePosition = false;
            anim.Play("die");
        }
    }
    private IEnumerator IFrame()
    {
        Physics2D.IgnoreLayerCollision(10, 6, true);
        Physics2D.IgnoreLayerCollision(10, 7, true);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                                         spriteRenderer.color.b, 0.2f);
        yield return new WaitForSeconds(iFrameTime);
        Physics2D.IgnoreLayerCollision(10, 6, false);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                                         spriteRenderer.color.b, 1);
        hit = false;
    }
    private void Attack1(string verSide, string horSide)
    {
        attacking = true;
        anim.Play("attack_" + horSide + verSide);
        StartCoroutine(AttackTimer());
    }
    public void Attack()
    {
        Vector3 targetPosition = playerTransform.position;

        projectilesSpawner.FindProjectiles().transform.position = transform.position;
        Vector2 dir = new Vector2(targetPosition.x - transform.position.x,
                                  targetPosition.y - 0.1f - transform.position.y);
        dir.Normalize();

        projectilesSpawner.FindProjectiles().GetComponent<MagicBullet>().Activate(dir, attackDamage);
    }
    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(88f / 60);
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
