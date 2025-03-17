using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected PlayerAttack playerAttack;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask playerLayer;
    protected bool collecting;
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
    public void Spawn(Vector3 position, float dropRate)
    {
        if (Random.Range(0, 1) <= dropRate)
        {
            gameObject.SetActive(true);
            transform.position = position;
        }
    }
    protected bool PlayerCollectable()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, playerAttack.collectableRange, Vector2.zero, 0f, playerLayer);
        return hit.collider != null;
    }
}
