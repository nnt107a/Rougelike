using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected PlayerAttack playerAttack;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask playerLayer;
    protected bool collecting;
    public float value = 1f;
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void Update()
    {
        if (collecting)
        {
            Vector3 tmp = new Vector3(playerAttack.transform.position.x - transform.position.x,
                                      playerAttack.transform.position.y - 0.2f - transform.position.y,
                                      0);
            tmp = tmp.normalized;
            transform.Translate(tmp * Time.deltaTime * speed);
        }
    }
    public void Spawn(Vector3 position, float dropRate, float _value)
    {
        if (Random.Range(0f, 1f) <= dropRate)
        {
            gameObject.SetActive(true);
            transform.position = position;
            value = _value;
        }
    }
    protected bool PlayerCollectable()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, PlayerAttack.stats["collectableRange"], Vector2.zero, 0f, playerLayer);
        return hit.collider != null;
    }
}
