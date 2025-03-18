using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    [Header("Enemy")]
    [SerializeField] protected float iFrameTime;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float baseAttackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackOffset;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float minSpawnDistance = 5.0f;
    [SerializeField] protected float maxSpawnDistance = 10.0f;
    [SerializeField] protected SpawnerHandler spawnerHandler;
    [SerializeField] protected GameObject enemy;
    [SerializeField] protected RunHandler runHandler;
    [SerializeField] protected CoinHolder coinHolder;
    protected float attackDamage;

    protected Animator anim;
    protected BoxCollider2D boxCollider;
    protected NavMeshAgent navMeshAgent;
    protected bool hit;
    protected bool dead;
    protected bool fleeing;
    protected bool chasing;
    protected bool attacking;
    protected float attackTimer = 0;
    protected virtual void Awake()
    {
        base.Awake();
        startingHealth = baseHealth * runHandler.HealthModifier();
        currentHealth = startingHealth;
        attackDamage = baseAttackDamage * runHandler.DamageModifier();
        hit = false;
        dead = false;
    }
    protected virtual void Update()
    {
        base.Update();
    }
    public virtual void Spawn(Vector2 position)
    {
    }
    public virtual Vector2 CalcSpawnPos()
    {
        return Vector2.zero;
    }
    public virtual void ActivateAnim()
    {

    }
    public virtual void Deactivate()
    {
        coinHolder.Spawn(transform.position);
        spawnerHandler.DecreaseEnemy(enemy);
    }
}
