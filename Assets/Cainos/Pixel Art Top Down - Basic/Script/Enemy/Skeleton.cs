using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Skeleton : Enemy
{
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;
    private void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();
        circleCollider = GetComponentInChildren<CircleCollider2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updatePosition = true;

        capsuleCollider.enabled = false;
        circleCollider.enabled = false;

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (dead)
        {
            return;
        }
        base.Update();

        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            if (PlayerInRange() && !hit)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Attack_1();
                }
                else
                {
                    Attack_2();
                }
                attackTimer = 0;
            }
        }

        if (gameObject.activeInHierarchy)
        {
            navMeshAgent.destination = playerTransform.position;
        }

        if (transform.position.x > playerTransform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                                               transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                                               transform.localScale.y, transform.localScale.z);
        }
    }
    public override void ActivateAnim()
    {
        if (currentHealth > 0)
        {
            hit = true;
            anim.Play("hit");
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
        yield return new WaitForSeconds(iFrameTime);
        Physics2D.IgnoreLayerCollision(10, 6, false);
        Physics2D.IgnoreLayerCollision(10, 7, false);
        hit = false;
    }
    private bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * attackOffset,
                                            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y * 2, boxCollider.bounds.size.z),
                                            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void Attack_1()
    {
        anim.Play("attack_1");
        StartCoroutine(Attack1());
    }
    private void Attack_2()
    {
        anim.Play("attack_2");
        StartCoroutine(Attack2());
    }
    private IEnumerator Attack1()
    {
        yield return new WaitForSeconds(0.5f);
        circleCollider.enabled = true;
        yield return new WaitForSeconds(0.5f / 6);
        circleCollider.enabled = false;
    }
    private IEnumerator Attack2()
    {
        yield return new WaitForSeconds(0.5f);
        capsuleCollider.enabled = true;
        yield return new WaitForSeconds(0.5f / 6);
        capsuleCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable"))
        {
            collision.GetComponent<Destroyable>().TakeDamage(attackDamage);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
            collision.GetComponent<PlayerAttack>().ActivateAnim();
        }
    }
    public void Deactivate()
    {
        base.Deactivate();
        capsuleCollider.enabled = false;
        circleCollider.enabled = false;
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
        attackTimer = 0;
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        transform.position = position;
    }
}
