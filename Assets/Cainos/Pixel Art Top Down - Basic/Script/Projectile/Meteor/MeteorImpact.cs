using UnityEngine;

public class MeteorImpact : MonoBehaviour
{
    [SerializeField] private Meteor meteor;
    private float damage;
    private void Impact()
    {
        meteor.Impact();
    }
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable"))
        {
            collision.GetComponent<Destroyable>().TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Skeleton>())
            {
                collision.GetComponent<Skeleton>().TakeDamage(damage);
                collision.GetComponent<Skeleton>().ActivateAnim();
            }
        }
    }
    private void Deactivate()
    {
        meteor.Deactivate();
    }
}
