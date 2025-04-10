using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : Health
{
    [Header("Player attributes")]
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseCoinMultipler;
    [SerializeField] private float baseExpMultipler;
    [SerializeField] private float baseCoinDropRate;
    [SerializeField] private float baseCollectableRange;
    [SerializeField] private float baseDef;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseCritRate;
    [SerializeField] private float baseCritDamage;
    public Dictionary<string, float> baseStats;
    public static Dictionary<string, float> stats;

    [Header("Attributes Limit")]
    [SerializeField] private float defCap;
    [SerializeField] private float cooldownCap;
    [SerializeField] private float castSpeedCap;
    [SerializeField] private float defPenCap;
    [SerializeField] private float dodgeCap;

    [Header("Player resources")]
    [SerializeField] public Mana mana;
    [SerializeField] public ExpBar exp;
    [SerializeField] private GameObject weapon;

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
        baseStats.Add("speed", baseSpeed);
        baseStats.Add("critRate", baseCritRate);
        baseStats.Add("critDamage", baseCritDamage);
        stats.Add("damage", baseDamage);
        stats.Add("health", baseHealth);
        stats.Add("healthRegen", baseHealthRegen);
        stats.Add("mana", mana.baseMana);
        stats.Add("manaRegen", mana.baseManaRegen);
        stats.Add("coinMultipler", baseCoinMultipler);
        stats.Add("expMultipler", baseExpMultipler);
        stats.Add("coinDropRate", baseCoinDropRate);
        stats.Add("collectableRange", baseCollectableRange);
        stats.Add("speed", baseSpeed);
        stats.Add("def", baseDef);
        stats.Add("critRate", baseCritRate);
        stats.Add("critDamage", baseCritDamage);
        stats.Add("cooldown", 0);
        stats.Add("castSpeed", 0);
        stats.Add("defPen", 0);
        stats.Add("dodge", 0);
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
        switch (attr)
        {
            case "health":
                startingHealth = stats["health"];
                currentHealth = Mathf.Clamp(currentHealth + (percentage ? value * baseStats[attr] : value), 0, startingHealth);
                break;
            case "healthRegen":
                healthRegenerateSpeed = stats["healthRegen"];
                break;
            case "mana":
                mana.startingMana = stats["mana"];
                mana.IncreaseMana((percentage ? value * baseStats[attr] : value));
                break;
            case "manaRegen":
                mana.manaRegenerateSpeed = stats["manaRegen"];
                break;
            case "def":
                stats["def"] = Mathf.Min(stats["def"], defCap);
                def = stats["def"];
                break;
            case "cooldown":
                stats["cooldown"] = Mathf.Min(stats["cooldown"], cooldownCap);
                weapon.GetComponent<Weapon>().UpdateCooldown();
                break;
            case "castSpeed":
                stats["castSpeed"] = Mathf.Min(stats["castSpeed"], castSpeedCap);
                weapon.GetComponent<Weapon>().UpdateCastSpeed();
                break;
            case "critRate":
                stats["critRate"] = Mathf.Min(stats["critRate"], 100f);
                break;
            case "defPen":
                stats["defPen"] = Mathf.Min(stats["defPen"], defPenCap);
                break;
            case "dodge":
                stats["dodge"] = Mathf.Min(stats["dodge"], dodgeCap);
                dodge = stats["dodge"];
                break;
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
