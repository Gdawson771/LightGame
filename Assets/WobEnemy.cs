using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    private bool detonation;
    private float detonationBegin;
    public float detonationLength;
    public float shakeSpeed;
    void Start()
    {
        detonationBegin = 0;
        detonationLength = 3f;
        shakeSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 2f)
        {
            detonationBegin = Time.time;
        }
        if (detonationBegin != 0)
        {
            if(detonationBegin + detonationLength > Time.time) {
                Explode();
            }
        }
        // increasingly shake for 3 seconds
        // after three seconds play sounds 
        // explode
        // explosion kills player if in certain radius
        // and knocks back all character in outer radius
    }

    private void Explode() {
        Debug.Log(" de");
    }

    private void Detonate()
    {

    }
}
