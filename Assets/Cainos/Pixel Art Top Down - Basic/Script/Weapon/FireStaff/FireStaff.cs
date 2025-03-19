using UnityEngine;
using System.Collections;

public class FireStaff : Weapon
{
    [Header ("Primary Skill")]
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float primaryCastTime;

    [Header ("Secondary Skill")]
    [SerializeField] private GameObject meteor;
    [SerializeField] private float secondaryRange;
    [SerializeField] private float secondaryCastTime;

    [Header("E Skill")]
    [SerializeField] private GameObject firepath;
    [SerializeField] private float eCastTime;

    private Vector3 mousePosition;
    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        base.Update();
    }
    private int findFireball()
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

        fireballs[findFireball()].transform.position = player.position;
        Vector2 dir = new Vector2(mousePosition.x - cam.WorldToScreenPoint(player.position).x,
                                  mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        dir.Normalize();

        fireballs[findFireball()].GetComponent<Fireball>().Activate(dir, playerAttack.stats["damage"]);
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
    private void ActivateSecondary()
    {
        base.Secondary();

        Vector2 target = new Vector2(cam.ScreenToWorldPoint(mousePosition).x, cam.ScreenToWorldPoint(mousePosition).y);
        meteor.GetComponent<Meteor>().Activate(playerAttack.stats["damage"], calcActualPos(player.position, target, secondaryRange));
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
        firepath.GetComponent<FirePath>().PreActivate(playerAttack.stats["damage"], player.position, angle, offVector);
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
