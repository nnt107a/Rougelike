using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected PlayerAttack playerAttack;
    [SerializeField] protected Camera cam;
    [SerializeField] protected Animator anim;
    protected int castingIden = 0;
    protected int castingLimit = 10;
    public bool casting;

    [Header("Primary Skill")]
    [SerializeField] protected Image primaryIcon;
    [SerializeField] protected Image primaryDisplay;
    [SerializeField] protected Sprite primarySprite;
    [SerializeField] protected float basePrimaryCooldown;
    [SerializeField] protected float primaryManaCost;
    protected float primaryTimer = Mathf.Infinity;
    protected float primaryCooldown;
    
    [Header("Secondary Skill")]
    [SerializeField] protected Image secondaryIcon;
    [SerializeField] protected Image secondaryDisplay;
    [SerializeField] protected Sprite secondarySprite;
    [SerializeField] protected float baseSecondaryCooldown;
    [SerializeField] protected float secondaryManaCost;
    protected float secondaryTimer = Mathf.Infinity;
    protected float secondaryCooldown;

    [Header("E Skill")]
    [SerializeField] protected Image eIcon;
    [SerializeField] protected Image eDisplay;
    [SerializeField] protected Sprite eSprite;
    [SerializeField] protected float baseECooldown;
    [SerializeField] protected float eManaCost;
    protected float eTimer = Mathf.Infinity;
    protected float eCooldown;
    protected void Awake()
    {
        primaryCooldown = basePrimaryCooldown;
        primaryIcon.sprite = primarySprite;
        primaryDisplay.sprite = primarySprite;
        primaryDisplay.fillAmount = 1 - Mathf.Min(primaryTimer / primaryCooldown, 1);
        secondaryCooldown = baseSecondaryCooldown;
        secondaryIcon.sprite = secondarySprite;
        secondaryDisplay.sprite = secondarySprite;
        secondaryDisplay.fillAmount = 1 - Mathf.Min(secondaryTimer / secondaryCooldown, 1);
        eCooldown = baseECooldown;
        eIcon.sprite = eSprite;
        eDisplay.sprite = eSprite;
        eDisplay.fillAmount = 1 - Mathf.Min(eTimer / eCooldown, 1);
    }
    protected void Update()
    {
        var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("primary") || stateInfo.IsName("secondary") || stateInfo.IsName("e"))
        {
            anim.speed = 1f + PlayerAttack.stats["castSpeed"] / 100f;
        }
        else
        {
            anim.speed = 1f;
        }
        if (Input.GetMouseButtonDown(0) && primaryTimer >= primaryCooldown && playerAttack.mana.currentMana >= primaryManaCost)
        {
            Primary();
        }
        else
        {
            primaryTimer += Time.deltaTime;
        }

        primaryDisplay.fillAmount = 1 - Mathf.Min(primaryTimer / primaryCooldown, 1);
        
        if (Input.GetMouseButtonDown(1) && secondaryTimer >= secondaryCooldown && playerAttack.mana.currentMana >= secondaryManaCost)
        {
            Secondary();
        }
        else
        {
            secondaryTimer += Time.deltaTime;
        }

        secondaryDisplay.fillAmount = 1 - Mathf.Min(secondaryTimer / secondaryCooldown, 1);

        if (Input.GetKeyDown(KeyCode.E) && eTimer >= eCooldown && playerAttack.mana.currentMana >= eManaCost)
        {
            E();
        }
        else
        {
            eTimer += Time.deltaTime;
        }

        eDisplay.fillAmount = 1 - Mathf.Min(eTimer / eCooldown, 1);
    }
    public void UpdateCooldown()
    {
        primaryCooldown = basePrimaryCooldown * (100 - PlayerAttack.stats["cooldown"]) / 100f;
        secondaryCooldown = baseSecondaryCooldown * (100 - PlayerAttack.stats["cooldown"]) / 100f;
        eCooldown = baseECooldown * (100 - PlayerAttack.stats["cooldown"]) / 100f;
    }
    public virtual void UpdateCastSpeed()
    {
        
    }
    protected virtual void Primary()
    {
        primaryTimer = 0;
        playerAttack.mana.UseMana(primaryManaCost);
    }
    protected virtual void Secondary()
    {
        secondaryTimer = 0;
        playerAttack.mana.UseMana(secondaryManaCost);
    }
    protected virtual void E()
    {
        eTimer = 0;
        playerAttack.mana.UseMana(eManaCost);
    }

    protected Vector2 calcActualPos(Vector2 pos, Vector2 target, float range)
    {
        Vector2 dir = new Vector2(target.x - pos.x, target.y - pos.y);
        Vector2 tmpDir = new Vector2(dir.x, dir.y);
        dir.Normalize();

        Vector2 tmpPos = Vector2.zero;
        if (tmpDir.magnitude > range)
        {
            tmpPos.x = pos.x + dir.x * range;
            tmpPos.y = pos.y + dir.y * range;
        }
        else
        {
            tmpPos.x = target.x;
            tmpPos.y = target.y;
        }
        return tmpPos;
    }
}
