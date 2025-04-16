using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public float shieldValue;
    public float shieldDuration;
    public float expireTime;
    private Health health;
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public Shield(float _shieldValue, float _shieldDuration, GameObject _health)
    {
        shieldValue = _shieldValue;
        shieldDuration = _shieldDuration;
        expireTime = TimeHandler.timer + shieldDuration;
        health = _health.GetComponent<Health>();
    }
}
