using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : Health
{
    [Header("Player attributes")]
    [SerializeField] public float baseDamage;
    [SerializeField] public float baseCoinMultipler;
    [SerializeField] public float baseExpMultipler;
    [SerializeField] public float baseCoinDropRate;
    [SerializeField] public float baseCollectableRange;
    [SerializeField] public float baseDef;
    public Dictionary<string, float> baseStats;
    public Dictionary<string, float> stats;

    [Header("Player resources")]
    [SerializeField] public Mana mana;
    [SerializeField] public ExpBar exp;

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
        baseStats.Add("damage", baseDamage);
        baseStats.Add("health", baseHealth);
        baseStats.Add("healthRegen", baseHealthRegen);
        baseStats.Add("mana", mana.baseMana);
        baseStats.Add("manaRegen", mana.baseManaRegen);
        baseStats.Add("coinMultipler", baseCoinMultipler);
        baseStats.Add("expMultipler", baseExpMultipler);
        baseStats.Add("coinDropRate", baseCoinDropRate);
        baseStats.Add("collectableRange", baseCollectableRange);
        baseStats.Add("def", baseDef);
        stats.Add("damage", baseDamage);
        stats.Add("health", baseHealth);
        stats.Add("healthRegen", baseHealthRegen);
        stats.Add("mana", mana.baseMana);
        stats.Add("manaRegen", mana.baseManaRegen);
        stats.Add("coinMultipler", baseCoinMultipler);
        stats.Add("expMultipler", baseExpMultipler);
        stats.Add("coinDropRate", baseCoinDropRate);
        stats.Add("collectableRange", baseCollectableRange);
        stats.Add("def", baseDef);
        def = stats["def"];
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
        if (attr == "health")
        {
            startingHealth = stats["health"];
            currentHealth = Mathf.Clamp(currentHealth + (percentage ? value * baseStats[attr] : value), 0, startingHealth);
        }
        if (attr == "healthRegen")
        {
            healthRegenerateSpeed = stats["healthRegen"];
        }
        if (attr == "mana")
        {
            mana.startingMana = stats["mana"];
            mana.IncreaseMana((percentage ? value * baseStats[attr] : value));
        }
        if (attr == "manaRegen")
        {
            mana.manaRegenerateSpeed = stats["manaRegen"];
        }
        def = stats["def"];
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
