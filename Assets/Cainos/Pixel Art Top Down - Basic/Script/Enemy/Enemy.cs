using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

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
    [SerializeField] protected ExpHolder expHolder;
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

    [Header("Attributes")]
    [SerializeField] protected float baseDef;
    [SerializeField] protected float defCap;
    protected Dictionary<string, float> stats;
    protected Dictionary<string, float> baseStats;
    protected virtual void Awake()
    {
        base.Awake();
        dodge = 0;
        boxCollider = GetComponent<BoxCollider2D>();
        baseStats = new Dictionary<string, float>();
        stats = new Dictionary<string, float>();
        startingHealth = baseHealth * runHandler.HealthModifier();
        currentHealth = startingHealth;
        attackDamage = baseAttackDamage * runHandler.DamageModifier();
        baseStats.Add("health", startingHealth);
        baseStats.Add("damage", attackDamage);
        baseStats.Add("def", baseDef);
        stats.Add("health", startingHealth);
        stats.Add("damage", attackDamage);
        stats.Add("def", baseDef);
        def = stats["def"];
        hit = false;
        dead = false;
    }
    protected virtual void Update()
    {
        base.Update();
    }
    public virtual void Spawn(Vector2 position)
    {
        startingHealth = baseStats["health"] * runHandler.HealthModifier();
        attackDamage = baseStats["damage"] * runHandler.DamageModifier();
        stats["health"] = startingHealth;
        stats["damage"] = attackDamage;
        stats["def"] = Mathf.Min(baseStats["def"] * runHandler.DefModifier(), defCap);
        def = stats["def"];
        boxCollider.enabled = true;
        hit = false;
        dead = false;
        currentHealth = startingHealth;
        attackTimer = attackCooldown;
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
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
        coinHolder.Spawn(transform.position - new Vector3(0.1f, 0, 0));
        expHolder.Spawn(transform.position + new Vector3(0.1f, 0, 0));
        spawnerHandler.DecreaseEnemy(enemy);
    }
}
