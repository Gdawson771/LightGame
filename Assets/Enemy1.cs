using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy1 : MonoBehaviour
{
    public AIPath aIPath;
    public PlayerController player;
    public float grabPlayer = 2f;

    // Update is called once per frame
    void Update()
    {
        // check is in light
        if(Vector2.Distance(transform.position, player.transform.position) < grabPlayer) 
            player.Death();

    }


}
