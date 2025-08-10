using UnityEngine;
using System.Collections.Generic;
using TMPro; // For UI Text

public class SnakeMovement : MonoBehaviour
{
    public GameObject snakeSegmentPrefab;
    public float moveRate = 0.2f;
    private float moveTimer;
    private Vector2 moveDirection = Vector2.right;
    private List<Transform> segments = new List<Transform>();

    public GameObject gameOverUI; // Assign in Inspector
    public TextMeshProUGUI scoreText; // Assign in Inspector
    public TextMeshProUGUI gameOverText; // Assign in Inspector
    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        segments.Add(this.transform);
        UpdateScoreUI();

        // Hide GameOver UI by default
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        Time.timeScale = 1f; // Ensure game starts running
    }

    void Update()
    {
        if (isGameOver) return; // Stop movement after Game Over

        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2.down) moveDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector2.up) moveDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector2.right) moveDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector2.left) moveDirection = Vector2.right;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveRate)
        {
            Move();
            moveTimer = 0f;
        }
    }

    void Move()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x + moveDirection.x),
            Mathf.Round(transform.position.y + moveDirection.y),
            0.0f
        );
    }

    public void Grow()
    {
        GameObject segment = Instantiate(snakeSegmentPrefab);
        segment.transform.position = segments[segments.Count - 1].position;
        segments.Add(segment.transform);

        score += 10;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Border") || collision.CompareTag("SnakeSegment"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Show Game Over UI
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over hogya apka toh";
        }

        Time.timeScale = 0f; // Pause game
    }
}
