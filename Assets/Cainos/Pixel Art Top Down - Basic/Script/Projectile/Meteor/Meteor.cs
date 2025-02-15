using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float damageScale;
    private float damage;
    private Animator anim;
    private PolygonCollider2D collider;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<PolygonCollider2D>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
    }
    public void Activate(float _damage, Vector3 _position)
    {
        gameObject.SetActive(true);
        collider.enabled = false;
        anim.SetTrigger("activate");

        damage = _damage * damageScale;
        transform.position = _position + new Vector3(2.42f, 3.2f, 0);

        transform.GetComponentInChildren<MeteorImpact>().SetDamage(damage);
    }
    public void Impact()
    {
        collider.enabled = true;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
