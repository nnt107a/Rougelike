using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FireStaff : Weapon
{
    [Header ("Primary Skill")]
    [SerializeField] private GameObject[] fireballs;

    [Header ("Secondary Skill")]
    [SerializeField] private GameObject meteor;

    [Header("E Skill")]
    [SerializeField] private GameObject firepath;
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
        base.Primary();

        fireballs[findFireball()].transform.position = player.position;
        Vector2 dir = new Vector2(Input.mousePosition.x - cam.WorldToScreenPoint(player.position).x, 
                                  Input.mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        dir.Normalize();
        fireballs[findFireball()].GetComponent<Fireball>().Activate(dir, playerAttack.damage);
    }
    protected override void Secondary()
    {
        base.Secondary();

        meteor.GetComponent<Meteor>().Activate(playerAttack.damage, cam.ScreenToWorldPoint(Input.mousePosition));
    }
    protected override void E()
    {
        base.E();

        Vector2 dir = Vector2.up;
        if (cam.WorldToScreenPoint(player.position).y >= Input.mousePosition.y)
        {
            dir = Vector2.down;
        }
        Vector2 offVector = new Vector2(Input.mousePosition.x - cam.WorldToScreenPoint(player.position).x,
                                        Input.mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        offVector.Normalize();
        float angle = Vector2.Angle(offVector, dir);
        firepath.GetComponent<FirePath>().PreActivate(playerAttack.damage, player.position, angle, offVector);
    }
}
