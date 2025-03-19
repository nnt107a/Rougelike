using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : Health
{
    [Header("Player attributes")]
    [SerializeField] public float baseDamage;
    [SerializeField] public float baseCoinMultipler;
    [SerializeField] public float baseCoinDropRate;
    [SerializeField] public float baseCollectableRange;
    public Dictionary<string, float> baseStats;
    public Dictionary<string, float> stats;

    [Header("Player resources")]
    [SerializeField] public Mana mana;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public bool die;
    
    private void Awake()
    {
        base.Awake();
        stats = new Dictionary<string, float>();
        baseStats = new Dictionary<string, float>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats.Add("damage", 0);
        stats.Add("coinMultipler", 1);
        stats.Add("coinDropRate", 1);
        stats.Add("collectableRange", 1);
        baseStats.Add("damage", baseDamage);
        baseStats.Add("coinMultipler", baseCoinMultipler);
        baseStats.Add("coinDropRate", baseCoinDropRate);
        baseStats.Add("collectableRange", baseCollectableRange);
        stats["damage"] = baseDamage;
        stats["coinMultipler"] = baseCoinMultipler;
        stats["coinDropRate"] = baseCoinDropRate;
        stats["collectableRange"] = baseCollectableRange;
    }
    public void ModifyStat(string attr, float value, bool percentage)
    {
        if (percentage)
        {
            stats[attr] += value * baseStats[attr];
        }
        else
        {
            stats[attr] += value;
        }
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
            die = true;
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
        base.Update();
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
