using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridBehavior : MonoBehaviour
{
    public int gridSize = 10;  // Size of the grid (10x10x10 grid)
    public float moveSpeed = 0.2f;  // Speed of player movement
    public LayerMask obstacleLayer;  // Layer for obstacles
    public int maxStep = 5;  // Maximum steps a player can take in a single turn
    public int stepLeft;  // Steps left for the current player

    public Vector3 player1Pos;
    public Vector3 player2Pos;

    private bool isPlayer1Turn = true;  // Alternating turns
    private bool isMoving = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject obstaclePrefab;

    public int obstacleCount = 10;  // Number of obstacles in the grid

    private bool player1Moved = false;
    private bool player2Moved = false;

    void Start()
    {
        // Initialize starting positions
        player1Pos = new Vector3(0, 0, 0);
        player2Pos = new Vector3(gridSize - 1, 0, gridSize - 1);

        player1.transform.position = player1Pos;
        player2.transform.position = player2Pos;

        GenerateRandomObstacles();  // Generate obstacles randomly on the grid

        // Set initial steps
        stepLeft = maxStep;
    }

    void Update()
    {
        if (!isMoving)
        {
            // Player 1 moves using WASD
            if (isPlayer1Turn && !player1Moved)
            {
                Vector3 moveDirection = GetInputDirectionPlayer1();

                if (moveDirection != Vector3.zero && stepLeft > 0)
                {
                    StartCoroutine(MovePlayer(player1, player1Pos, moveDirection));
                }
            }
            // Player 2 moves using Arrow Keys
            else if (!isPlayer1Turn && !player2Moved)
            {
                Vector3 moveDirection = GetInputDirectionPlayer2();

                if (moveDirection != Vector3.zero && stepLeft > 0)
                {
                    StartCoroutine(MovePlayer(player2, player2Pos, moveDirection));
                }
            }
        }

        // Reset movement flags after both players have moved
        if (player1Moved && player2Moved)
        {
            player1Moved = false;
            player2Moved = false;
        }

        // Check if the two players have collided
        if (Vector3.Distance(player1.transform.position, player2.transform.position) < 0.1f)
        {
            EnterBattleScene();
        }
    }

    void GenerateRandomObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(0, gridSize), 
                0, 
                Random.Range(0, gridSize)
            );

            // Ensure obstacles do not spawn on player positions
            while (randomPos == player1Pos || randomPos == player2Pos)
            {
                randomPos = new Vector3(
                    Random.Range(0, gridSize), 
                    0, 
                    Random.Range(0, gridSize)
                );
            }

            Instantiate(obstaclePrefab, randomPos, Quaternion.identity);
        }
    }

    IEnumerator MovePlayer(GameObject player, Vector3 playerPos, Vector3 direction)
    {
        isMoving = true;

        Vector3 targetPos = playerPos + direction;

        // Ensure movement is within grid boundaries and avoid obstacles
        if (IsValidMove(targetPos))
        {
            float t = 0;
            Vector3 startPos = playerPos;

            while (t < 1f)
            {
                t += Time.deltaTime / moveSpeed;
                player.transform.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }

            // Update player position and steps left
            if (isPlayer1Turn)
            {
                player1Pos = targetPos;
                if(maxStep<=0)
                    player1Moved = true;
            }
            else
            {
                player2Pos = targetPos;
                if(maxStep<=0)
                    player2Moved = true;
            }

            // Decrement steps left
            stepLeft--;

            // If no steps left, switch turns
            if (stepLeft <= 0)
            {
                isPlayer1Turn = !isPlayer1Turn;
                stepLeft = maxStep;  // Reset steps for the next player
            }

            playerPos = targetPos;
        }
        isMoving = false;
    }

    Vector3 GetInputDirectionPlayer1()
    {
        if (Input.GetKeyDown(KeyCode.W)) return Vector3.forward;  // Move up
        if (Input.GetKeyDown(KeyCode.S)) return Vector3.back;     // Move down
        if (Input.GetKeyDown(KeyCode.A)) return Vector3.left;     // Move left
        if (Input.GetKeyDown(KeyCode.D)) return Vector3.right;    // Move right
        return Vector3.zero;
    }

    Vector3 GetInputDirectionPlayer2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector3.forward;    // Move up
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector3.back;     // Move down
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector3.left;     // Move left
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector3.right;   // Move right
        return Vector3.zero;
    }

    bool IsValidMove(Vector3 targetPos)
    {
        // Check if the target position is within grid bounds and not blocked by an obstacle
        if (targetPos.x >= 0 && targetPos.x < gridSize && targetPos.z >= 0 && targetPos.z < gridSize)
        {
            // Ensure the target position is not occupied by an obstacle
            if (!Physics.CheckSphere(targetPos, 0.4f, obstacleLayer))
            {
                return true;
            }
        }
        return false;
    }

    void EnterBattleScene()
    {
        Debug.Log("Players collided! Entering Battle Scene.");
        SceneManager.LoadScene("BattleScene");  // Ensure you have a scene called "BattleScene"
    }
}
