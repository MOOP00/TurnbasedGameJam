using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EachBox : MonoBehaviour
{
    public GridBehavior gridBehavior;
    public AdvantageSystem advantageSystem;
    public int randomEffect;
    public TextMeshPro _text;
    public GameObject ghost;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        gridBehavior = GameObject.Find("GridManager").GetComponent<GridBehavior>();
        advantageSystem = GameObject.Find("GridManager").GetComponent<AdvantageSystem>();
        randomEffect = Random.Range(0, 3);

        switch (randomEffect)
        {
            case 0:
                _text.text = "+Health";
                break;
            case 1:
                _text.text = "+Steps";
                break;
            case 2:
                _text.text = "+Attack";
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == gridBehavior.player1 || other.gameObject == gridBehavior.player2)
        {
            bool isPlayer1 = other.gameObject == gridBehavior.player1;
            ApplyRandomEffect(isPlayer1);
            Instantiate(ghost, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void ApplyRandomEffect(bool isPlayer1)
    {
        switch (randomEffect)
        {
            case 2:
                advantageSystem.ModifyAttack(isPlayer1, Random.Range(5,11));  // Gain 6 more steps this round
                break;
            case 1:
                gridBehavior.stepLeft += Random.Range(3,10);  // Gain 6 more steps this round
                break;
            case 0:
                advantageSystem.ModifyHealth(isPlayer1, Random.Range(1,4));  // Gain -1 advantage
                break;
        }
    }
}