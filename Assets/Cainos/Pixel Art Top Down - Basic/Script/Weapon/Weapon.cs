using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] protected float primaryCooldown;
    [SerializeField] protected float primaryManaCost;
    protected float primaryTimer = Mathf.Infinity;
    
    [Header("Secondary Skill")]
    [SerializeField] protected Image secondaryIcon;
    [SerializeField] protected Image secondaryDisplay;
    [SerializeField] protected Sprite secondarySprite;
    [SerializeField] protected float secondaryCooldown;
    [SerializeField] protected float secondaryManaCost;
    protected float secondaryTimer = Mathf.Infinity;

    [Header("E Skill")]
    [SerializeField] protected Image eIcon;
    [SerializeField] protected Image eDisplay;
    [SerializeField] protected Sprite eSprite;
    [SerializeField] protected float eCooldown;
    [SerializeField] protected float eManaCost;
    protected float eTimer = Mathf.Infinity;
    protected void Awake()
    {
        primaryIcon.sprite = primarySprite;
        primaryDisplay.sprite = primarySprite;
        primaryDisplay.fillAmount = 1 - Mathf.Min(primaryTimer / primaryCooldown, 1);
        secondaryIcon.sprite = secondarySprite;
        secondaryDisplay.sprite = secondarySprite;
        secondaryDisplay.fillAmount = 1 - Mathf.Min(secondaryTimer / secondaryCooldown, 1);
        eIcon.sprite = eSprite;
        eDisplay.sprite = eSprite;
        eDisplay.fillAmount = 1 - Mathf.Min(eTimer / eCooldown, 1);
    }
    protected void Update()
    {
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
