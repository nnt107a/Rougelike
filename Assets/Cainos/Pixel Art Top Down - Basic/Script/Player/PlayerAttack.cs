using UnityEngine;
using System.Collections;

public class PlayerAttack : Health
{
    [Header("Player attributes")]
    [SerializeField] public float damage;

    [Header("Player resources")]
    [SerializeField] public Mana mana;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ActivateAnim()
    {
        if (currentHealth > 0)
        {
            anim.Play("hit");
            StartCoroutine(IFrame());
        }
        else
        {
            anim.Play("die");
        }
    }
    private IEnumerator IFrame()
    {
        Physics2D.IgnoreLayerCollision(23, 8, true);
        Physics2D.IgnoreLayerCollision(23, 9, true);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                                         spriteRenderer.color.b, 0.2f);
        yield return new WaitForSeconds(28f / 60);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                                         spriteRenderer.color.b, 1);
        Physics2D.IgnoreLayerCollision(23, 8, false);
        Physics2D.IgnoreLayerCollision(23, 9, false);
    }
    private void Update()
    {
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
