using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;

    public void SpawnFood()
    {
        float x = Mathf.Round(Random.Range(-8f, 8f));
        float y = Mathf.Round(Random.Range(-8f, 8f));
        Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    void Start()
    {
        SpawnFood();
    }
}
