using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] public float baseHealth;
    public float startingHealth;
    [SerializeField] public float baseHealthRegen;
    public float healthRegenerateSpeed;
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
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
}
