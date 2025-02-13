using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FireStaff : Weapon
{
    [SerializeField] protected float damage;
    [SerializeField] private GameObject[] fireballs;
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
        primaryTimer = 0;

        fireballs[findFireball()].transform.position = player.position;
        Vector2 dir = new Vector2(Input.mousePosition.x - cam.WorldToScreenPoint(player.position).x, 
                                  Input.mousePosition.y - cam.WorldToScreenPoint(player.position).y);
        dir.Normalize();
        fireballs[findFireball()].GetComponent<Fireball>().Activate(dir, damage);
    }
}
