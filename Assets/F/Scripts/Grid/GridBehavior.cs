using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridBehavior : MonoBehaviour
{
    public int gridSize = 10;
    public float moveSpeed = 0.2f;
    public LayerMask obstacleLayer;

    public Vector3 player1Pos;
    public Vector3 player2Pos;

    private bool isPlayer1Turn = true;
    private bool isMoving = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject obstaclePrefab;
    public GameObject _ref;

    public int obstacleCount = 10;

    private bool player1Moved = false;
    private bool player2Moved = false;

    public FDiceTopFaceChecker diceTopFaceChecker;  // Reference to the dice checker
    public FRandomThrow randomThrow;
    public int stepsToMove = 0;  // Steps determined by the dice roll

    void Start()
    {
        player1Pos = new Vector3(2, 0, 2);
        player2Pos = new Vector3(gridSize - 3, 0, gridSize - 3);

        player1.transform.position = player1Pos;
        player2.transform.position = player2Pos;

        GeneratePerimeterObstacles();
        GenerateRandomObstacles();
    }

    void Update()
    {
        if (!isMoving)
        {
            // Wait for the player to roll the dice
            if (stepsToMove == 0 && !diceTopFaceChecker.moving)
            {
                stepsToMove = diceTopFaceChecker.value;
                Debug.Log("Rolled a " + stepsToMove);
            }

            // Player 1 moves using WASD
            if (isPlayer1Turn && stepsToMove > 0 && !player1Moved)
            {
                Vector3 moveDirection = GetInputDirectionPlayer1();

                if (moveDirection != Vector3.zero)
                {
                    StartCoroutine(MovePlayer(player1, player1Pos, moveDirection));
                }
            }
            // Player 2 moves using Arrow Keys
            else if (!isPlayer1Turn && stepsToMove > 0 && !player2Moved)
            {
                Vector3 moveDirection = GetInputDirectionPlayer2();

                if (moveDirection != Vector3.zero)
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

    IEnumerator MovePlayer(GameObject player, Vector3 playerPos, Vector3 direction)
    {
        isMoving = true;

        Vector3 targetPos = playerPos + direction;

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

            // Update player position
            if (isPlayer1Turn)
            {
                player1Pos = targetPos;
                player1Moved = true;
            }
            else
            {
                player2Pos = targetPos;
                player2Moved = true;
            }

            // Decrement steps left
            stepsToMove--;

            // If no steps left, switch turns
            if (stepsToMove <= 0)
            {
                isPlayer1Turn = !isPlayer1Turn;
                stepsToMove = 0;  // Reset for next turn (after a dice roll)
                randomThrow.ThrowObject();
            }

            playerPos = targetPos;
        }
        isMoving = false;
    }

    Vector3 GetInputDirectionPlayer1()
    {
        if (Input.GetKeyDown(KeyCode.W)) return (_ref.transform.forward);
        if (Input.GetKeyDown(KeyCode.S)) return (-_ref.transform.forward);
        if (Input.GetKeyDown(KeyCode.A)) return (-_ref.transform.right);
        if (Input.GetKeyDown(KeyCode.D)) return (_ref.transform.right);
        return Vector3.zero;
    }

    Vector3 GetInputDirectionPlayer2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) return (_ref.transform.forward);
        if (Input.GetKeyDown(KeyCode.DownArrow)) return (-_ref.transform.forward);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return (-_ref.transform.right);
        if (Input.GetKeyDown(KeyCode.RightArrow)) return (_ref.transform.right);
        return Vector3.zero;
    }

    bool IsValidMove(Vector3 targetPos)
    {
        if (!Physics.CheckSphere(targetPos, 0.2f, obstacleLayer))
        {
            return true;
        }
        return false;
    }

    void EnterBattleScene()
    {
        Debug.Log("Players collided! Entering Battle Scene.");
        SceneManager.LoadScene("BattleScene");
    }

    void GeneratePerimeterObstacles()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                if (x == 0 || x == gridSize - 1 || z == 0 || z == gridSize - 1)
                {
                    Vector3 perimeterPos = new Vector3(x, 0, z);

                    if (perimeterPos != player1Pos && perimeterPos != player2Pos)
                    {
                        Instantiate(obstaclePrefab, perimeterPos, Quaternion.identity);
                    }
                }
            }
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
}
