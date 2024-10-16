using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FNonCollide : MonoBehaviour
{
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 8);
    }
}
