using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Camera cam;

    [Header("Primary Skill")]
    [SerializeField] protected Image primaryIcon;
    [SerializeField] protected Image primaryDisplay;
    [SerializeField] protected Sprite primarySprite;
    [SerializeField] protected float primaryCooldown;
    protected float primaryTimer = Mathf.Infinity;
    protected void Awake()
    {
        primaryIcon.sprite = primarySprite;
        primaryDisplay.sprite = primarySprite;
        primaryDisplay.fillAmount = 1 - Mathf.Min(primaryTimer / primaryCooldown, 1);
    }
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0) && primaryTimer >= primaryCooldown)
        {
            Primary();
        }
        else
        {
            primaryTimer += Time.deltaTime;
        }

        primaryDisplay.fillAmount = 1 - Mathf.Min(primaryTimer / primaryCooldown, 1);
    }
    protected virtual void Primary()
    {
        Debug.Log("Primary on parent");
    }
}
