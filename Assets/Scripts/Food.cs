using UnityEngine;

public class Food : MonoBehaviour
{
    void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        float x = Mathf.Round(Random.Range(-8f, 8f));
        float y = Mathf.Round(Random.Range(-4f, 4f));
        transform.position = new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<SnakeMovement>().Grow();
            RandomizePosition();
        }
    }
}
