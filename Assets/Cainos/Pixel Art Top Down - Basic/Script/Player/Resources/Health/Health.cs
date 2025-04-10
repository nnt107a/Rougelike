using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] public float baseHealth;
    public float startingHealth;
    [SerializeField] public float baseHealthRegen;
    public float healthRegenerateSpeed;
    protected float def;
    protected float dodge;
    public float currentHealth { get; protected set; }
    protected void Awake()
    {
        startingHealth = baseHealth;
        currentHealth = startingHealth;
        healthRegenerateSpeed = baseHealthRegen;
        if (healthRegenerateSpeed != 0)
        {
            StartCoroutine(RecoverHealth());
        }
    }
    private IEnumerator RecoverHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth + healthRegenerateSpeed, 0, startingHealth);
        yield return new WaitForSeconds(1);
        StartCoroutine(RecoverHealth());
    }
    protected void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float damage, float critRate, float critDamage, float defPen)
    {
        if (Random.Range(0f, 100f) < dodge)
        {
            return;
        }
        float tempDamage = damage * (100f - Mathf.Max(def - defPen, 0)) / 100f;
        float finalDamage = tempDamage * (Random.Range(0f, 100f) < critRate ? critDamage / 100f : 1f);
        currentHealth = Mathf.Clamp(currentHealth - finalDamage, 0, startingHealth);
    }
}
