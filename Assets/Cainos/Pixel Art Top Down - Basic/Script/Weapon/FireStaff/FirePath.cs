using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class FirePath : MonoBehaviour
{
    [SerializeField] private float damageScale;
    [SerializeField] private int numberOfAnim;
    [SerializeField] private float offset;
    private int animCounter = 0;
    private float damage;
    private Animator anim;
    private float angle;
    private Vector2 dir;
    [SerializeField] private Collider2D[] colliders;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
    }
    private void ReplayAnimation()
    {
        Activate(damage, transform.position, angle, dir);
    }
    public void PreActivate(float _damage, Vector3 _position, float _angle, Vector2 _dir)
    {
        damage = _damage * damageScale;
        transform.position = _position;
        angle = _angle;
        dir = _dir;

        transform.rotation = Quaternion.identity;
        transform.Translate(new Vector3(dir.x * offset,
                                        dir.y * offset,
                                        0));

        transform.Rotate(new Vector3(0, 0, (dir.x * dir.y > 0 ? -angle : angle)));

        Activate(_damage, _position, _angle, _dir);
    }
    private void Activate(float _damage, Vector3 _position, float _angle, Vector2 _dir)
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                                           transform.localScale.z);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        gameObject.SetActive(true);
        animCounter++;
        if (animCounter > numberOfAnim)
        {
            Deactivate();
            return;
        }
        anim.SetTrigger("activate");

        StartCoroutine(ModifyColliders());
    }
    private void ModifyCollider(int step)
    {
        if (step < colliders.Length)
        {
            colliders[step].enabled = true;
        }
        if (step - 4 >= 0 && step - 4 < colliders.Length)
        {
            colliders[step - 4].enabled = false;
        }
    }
    private IEnumerator ModifyColliders()
    {
        for (int i = 0; i < 8; i++)
        {
            ModifyCollider(i);
            yield return new WaitForSeconds(7.0f / 60);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable"))
        {
            collision.GetComponent<Destroyable>().TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>()?.TakeDamage(damage);
            collision.GetComponent<Enemy>()?.ActivateAnim();
        }
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        animCounter = 0;
    }
}
