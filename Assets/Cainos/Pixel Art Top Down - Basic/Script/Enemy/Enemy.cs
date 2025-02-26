using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    [SerializeField] protected float iFrameTime;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackOffset;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float minSpawnDistance = 5.0f;
    [SerializeField] protected float maxSpawnDistance = 10.0f;
    [SerializeField] protected SpawnerHandler spawnerHandler;

    protected Animator anim;
    protected BoxCollider2D boxCollider;
    protected NavMeshAgent navMeshAgent;
    protected bool hit;
    protected bool dead;
    protected float attackTimer = 0;
    protected void Awake()
    {
        base.Awake();
        hit = false;
        dead = false;
    }
    protected void Update()
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
    protected void Deactivate()
    {
        spawnerHandler.DecreaseEnemy();
    }
}
