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

    public bool isPlayer1Turn = true;  // Alternating turns
    private bool isMoving = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject obstaclePrefab;
    public GameObject _ref;

    public int obstacleCount = 10;  // Number of obstacles in the grid

    private bool player1Moved = false;
    private bool player2Moved = false;

    void GeneratePerimeterObstacles()
    {
        // Place obstacles along the outer boundary (perimeter) of the grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // Place obstacles at the edges (perimeter) of the grid
                if (x == 0 || x == gridSize - 1 || z == 0 || z == gridSize - 1)
                {
                    Vector3 perimeterPos = new Vector3(x, 0, z);
                    
                    // Ensure no obstacles are placed on player starting positions
                    if (perimeterPos != player1Pos && perimeterPos != player2Pos)
                    {
                        Instantiate(obstaclePrefab, perimeterPos, Quaternion.identity);
                    }
                }
            }
        }
    }

    void Start()
    {
        // Initialize starting positions
        player1Pos = new Vector3(2, 0, 2);  // Adjusted to be inside the perimeter
        player2Pos = new Vector3(gridSize - 3, 0, gridSize - 3);  // Adjusted to be inside the perimeter

        player1.transform.position = player1Pos;
        player2.transform.position = player2Pos;

        GeneratePerimeterObstacles();  // Generate obstacles around the perimeter
        GenerateRandomObstacles();
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
                Random.Range(1, gridSize-1), 
                0, 
                Random.Range(1, gridSize-1)
            );

            // Ensure obstacles do not spawn on player positions
            while (randomPos == player1Pos || randomPos == player2Pos)
            {
                randomPos = new Vector3(
                    Random.Range(1, gridSize-1), 
                    0, 
                    Random.Range(1, gridSize-1)
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
        if (Input.GetKeyDown(KeyCode.W)) return (_ref.transform.forward);  // Move up
        if (Input.GetKeyDown(KeyCode.S)) return (-_ref.transform.forward);     // Move down
        if (Input.GetKeyDown(KeyCode.A)) return (-_ref.transform.right);     // Move left
        if (Input.GetKeyDown(KeyCode.D)) return (_ref.transform.right);    // Move right
        return Vector3.zero;
    }

    Vector3 GetInputDirectionPlayer2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return (_ref.transform.forward);    // Move up
        if (Input.GetKeyDown(KeyCode.DownArrow)) return (-_ref.transform.forward);     // Move down
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return (-_ref.transform.right);     // Move left
        if (Input.GetKeyDown(KeyCode.RightArrow)) return (_ref.transform.right);   // Move right
        return Vector3.zero;
    }

    bool IsValidMove(Vector3 targetPos)
    {
        if (!Physics.CheckSphere(targetPos, 0.2f, obstacleLayer))  // Smaller sphere check
        {
            return true;
        }else
        return false;
    }

    void EnterBattleScene()
    {
        Debug.Log("Players collided! Entering Battle Scene.");
        SceneManager.LoadScene("BattleScene");  // Ensure you have a scene called "BattleScene"
    }
}