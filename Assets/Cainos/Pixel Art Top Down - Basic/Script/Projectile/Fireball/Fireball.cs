using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private float damageScale;
    private float lifetimer;
    private float damage;
    private Animator anim;
    private PolygonCollider2D collider;
    private bool hit;
    private Vector2 dir;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        lifetimer += Time.deltaTime;
        if (lifetimer > lifetime)
        {
            Deactivate();
        }
    }
    public void Activate(Vector2 _dir, float _damage)
    {
        hit = false;
        lifetimer = 0;
        dir = _dir;
        gameObject.SetActive(true);
        collider.enabled = true;

        transform.localRotation = Quaternion.identity;
        transform.Translate(new Vector3(dir.x * speed * distance,
                                        dir.y * speed * distance,
                                        0));

        float angle = Vector3.Angle(new Vector3(dir.x, dir.y, 0), new Vector3(1, 0, 0));
        transform.Rotate(new Vector3(0, 0, angle * (dir.y > 0 ? 1 : -1)));

        damage = _damage * damageScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider.enabled = false;
        hit = true;
        anim.SetTrigger("explode");

        if (collision.CompareTag("Destroyable"))
        {
            collision.GetComponent<Destroyable>().TakeDamage(damage);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
