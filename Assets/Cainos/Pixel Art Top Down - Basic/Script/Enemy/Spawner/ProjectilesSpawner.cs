using UnityEngine;

public class ProjectilesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] projectiles;
    public GameObject FindProjectiles()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return projectiles[i];
            }
        }
        return projectiles[0];
    }
}
