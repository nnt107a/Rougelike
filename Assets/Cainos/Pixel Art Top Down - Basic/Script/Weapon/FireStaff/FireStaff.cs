using UnityEngine;
using System.Collections;

public class FireStaff : Weapon
{
    [Header ("Primary Skill")]
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float basePrimaryCastTime;
    private float primaryCastTime;

    [Header ("Secondary Skill")]
    [SerializeField] private GameObject[] meteors;
    [SerializeField] private float secondaryRange;
    [SerializeField] private float baseSecondaryCastTime;
    private float secondaryCastTime;

    [Header("E Skill")]
    [SerializeField] private GameObject[] firepaths;
    [SerializeField] private float baseECastTime;
    private float eCastTime;

    private Vector3 mousePosition;
    private void Awake()
    {
        base.Awake();
        primaryCastTime = basePrimaryCastTime;
        secondaryCastTime = baseSecondaryCastTime;
        eCastTime = baseECastTime;
    }
    private void Update()
    {
        base.Update();
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    public override void UpdateCastSpeed()
    {
        primaryCastTime = basePrimaryCastTime * (100f - PlayerAttack.stats["castSpeed"]) / 100f;
        secondaryCastTime = baseSecondaryCastTime * (100f - PlayerAttack.stats["castSpeed"]) / 100f;
        eCastTime = baseECastTime * (100f - PlayerAttack.stats["castSpeed"]) / 100f;
    }
    protected override void Primary()
    {
        mousePosition = Input.mousePosition;
        if ((mousePosition.x - cam.WorldToScreenPoint(player.position).x) * player.localScale.x < 0)
        {
            player.localScale = new Vector3(-player.localScale.x, player.localScale.y,
                                            player.localScale.z);
        }
        anim.SetTrigger("primary");
        casting = true;
        castingIden = (castingIden + 1) % castingLimit;
        StartCoroutine(ActivatePrimaryTimer());
    }
    private void ActivatePrimary()
    {
        base.Primary();

        fireballs[FindFireball()].transform.position = player.position;
        Vector2 dir = new Vector2(mousePosition.x - cam.WorldToScreenPoint(player.position).x,
                                  mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        dir.Normalize();

        fireballs[FindFireball()].GetComponent<Fireball>().Activate(dir, PlayerAttack.stats["damage"]);
    }
    private IEnumerator ActivatePrimaryTimer()
    {
        int tempCastingIden = castingIden;
        yield return new WaitForSeconds(primaryCastTime);
        if (casting && tempCastingIden == castingIden)
        {
            ActivatePrimary();
        }
    }
    protected override void Secondary()
    {
        mousePosition = Input.mousePosition;
        if ((mousePosition.x - cam.WorldToScreenPoint(player.position).x) * player.localScale.x < 0)
        {
            player.localScale = new Vector3(-player.localScale.x, player.localScale.y,
                                            player.localScale.z);
        }
        anim.SetTrigger("secondary");
        casting = true;
        castingIden = (castingIden + 1) % castingLimit;
        StartCoroutine(ActivateSecondaryTimer());
    }
    private int FindMeteor()
    {
        for (int i = 0; i < meteors.Length; i++)
        {
            if (!meteors[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void ActivateSecondary()
    {
        base.Secondary();

        Vector2 target = new Vector2(cam.ScreenToWorldPoint(mousePosition).x, cam.ScreenToWorldPoint(mousePosition).y);
        meteors[FindMeteor()].GetComponent<Meteor>().Activate(PlayerAttack.stats["damage"], calcActualPos(player.position, target, secondaryRange));
    }
    private IEnumerator ActivateSecondaryTimer()
    {
        int tempCastingIden = castingIden;
        yield return new WaitForSeconds(secondaryCastTime);
        if (casting && tempCastingIden == castingIden)
        {
            ActivateSecondary();
        }
    }
    protected override void E()
    {
        mousePosition = Input.mousePosition;
        if ((mousePosition.x - cam.WorldToScreenPoint(player.position).x) * player.localScale.x < 0)
        {
            player.localScale = new Vector3(-player.localScale.x, player.localScale.y,
                                            player.localScale.z);
        }
        anim.SetTrigger("e");
        casting = true;
        castingIden = (castingIden + 1) % castingLimit;
        StartCoroutine(ActivateETimer());
    }
    private int FindFirepath()
    {
        for (int i = 0; i < firepaths.Length; i++)
        {
            if (!firepaths[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void ActivateE()
    {
        base.E();

        Vector2 dir = Vector2.up;
        if (cam.WorldToScreenPoint(player.position).y >= mousePosition.y)
        {
            dir = Vector2.down;
        }
        Vector2 offVector = new Vector2(mousePosition.x - cam.WorldToScreenPoint(player.position).x,
                                        mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        offVector.Normalize();
        float angle = Vector2.Angle(offVector, dir);
        firepaths[FindFirepath()].GetComponent<FirePath>().PreActivate(PlayerAttack.stats["damage"], player.position, angle, offVector);
    }
    private IEnumerator ActivateETimer()
    {
        int tempCastingIden = castingIden;
        yield return new WaitForSeconds(eCastTime);
        if (casting && tempCastingIden == castingIden)
        {
            ActivateE();
        }
    }
}
