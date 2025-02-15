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
    }
    private void Deactivate()
    {
        meteor.Deactivate();
    }
}
