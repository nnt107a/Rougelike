using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public static float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
    }
}
