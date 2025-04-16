using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] public float baseHealth;
    public float startingHealth;
    [SerializeField] public float baseHealthRegen;
    [SerializeField] public bool isPlayer;
    public float healthRegenerateSpeed;
    protected float def;
    protected float dodge;
    public PriorityQueue<Shield> shieldQueue = new();
    public float shieldValue;
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
    public void AddShield(float _shieldValue, float _shieldDuration)
    {
        if (shieldValue == startingHealth * 2)
        {
            return;
        }
        Shield tmp = new(Mathf.Min(startingHealth * 2 - shieldValue, _shieldValue), _shieldDuration, gameObject);
        shieldValue += Mathf.Min(startingHealth * 2 - shieldValue, _shieldValue);
        shieldQueue.Enqueue(tmp, tmp.expireTime);
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
        while (shieldQueue.Count > 0)
        {
            if (shieldQueue.Peek().expireTime <= TimeHandler.timer)
            {
                shieldValue -= shieldQueue.Peek().shieldValue;
                shieldQueue.Dequeue();
            }
            else
            {
                break;
            }
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
        if (finalDamage <= shieldValue)
        {
            shieldValue -= finalDamage;
            while (shieldQueue.Count != 0)
            {
                Shield tmp = shieldQueue.Dequeue();
                if (finalDamage < tmp.shieldValue)
                {
                    tmp.shieldValue -= finalDamage;
                    shieldQueue.Enqueue(tmp, tmp.expireTime);
                    break;
                }
                else
                {
                    finalDamage -= tmp.shieldValue;
                }
            }
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - (finalDamage - shieldValue), 0, startingHealth);
        shieldValue = 0;
        while (shieldQueue.Count != 0)
        {
            shieldQueue.Dequeue();
        } 
    }
}
