using UnityEngine;
using System.Collections;

public class FireStaff : Weapon
{
    [Header ("Primary Skill")]
    [SerializeField] private GameObject[] fireballs;

    [Header ("Secondary Skill")]
    [SerializeField] private GameObject meteor;

    [Header("E Skill")]
    [SerializeField] private GameObject firepath;

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
        StartCoroutine(ActivatePrimaryTimer());
    }
    private void ActivatePrimary()
    {
        base.Primary();

        fireballs[findFireball()].transform.position = player.position;
        Vector2 dir = new Vector2(mousePosition.x - cam.WorldToScreenPoint(player.position).x,
                                  mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        dir.Normalize();

        fireballs[findFireball()].GetComponent<Fireball>().Activate(dir, playerAttack.damage);
    }
    private IEnumerator ActivatePrimaryTimer()
    {
        yield return new WaitForSeconds(32.0f / 60);
        ActivatePrimary();
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
        StartCoroutine(ActivateSecondaryTimer());
    }
    private void ActivateSecondary()
    {
        base.Secondary();

        meteor.GetComponent<Meteor>().Activate(playerAttack.damage, cam.ScreenToWorldPoint(mousePosition));
    }
    private IEnumerator ActivateSecondaryTimer()
    {
        yield return new WaitForSeconds(32.0f / 60);
        ActivateSecondary();
    }
    protected override void E()
    {
        mousePosition = Input.mousePosition;
        if ((mousePosition.x - cam.WorldToScreenPoint(player.position).x) * player.localScale.x < 0)
        {
            player.localScale = new Vector3(-player.localScale.x, player.localScale.y,
                                            player.localScale.z);
        }

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
        firepath.GetComponent<FirePath>().PreActivate(playerAttack.damage, player.position, angle, offVector);
    }
}
