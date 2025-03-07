using UnityEngine;

public class PlayerAttack : Health
{
    [Header("Player attributes")]
    [SerializeField] public float damage;

    [Header("Player resources")]
    [SerializeField] public Mana mana;
    
    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
    }
}
