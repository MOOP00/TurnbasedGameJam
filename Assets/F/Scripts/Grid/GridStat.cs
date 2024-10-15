using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visited = -1;
    public int x = 0;
    public int y = 0;
    public GridBehavior gb;
    public bool start = false;
    public bool end = false;
    public GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
        gb = GameObject.Find("GridManager").GetComponent<GridBehavior>();
    }

    void Update()
    {
        /*end = false;
        start = false;
        if(gb.startX == x && gb.startY == y)
        {
            start = true;
        }
        if(gb.endX == x && gb.endY == y)
        {
            end = true;
            //Player.transform.position = transform.position;
        }*/
    }
}
