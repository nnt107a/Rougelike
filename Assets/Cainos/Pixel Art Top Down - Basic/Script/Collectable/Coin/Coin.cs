using UnityEngine;

public class Coin : Collectable
{
    private CircleCollider2D circleCollider;
    private void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        base.Update();
        if (PlayerCollectable())
        {
            collecting = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinHandler.instance.IncCoin(PlayerAttack.stats["coinMultipler"]);
            gameObject.SetActive(false);
            collecting = false;
        }
    }
}
