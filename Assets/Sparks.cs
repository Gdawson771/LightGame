using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    private float born;
    private float life = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        born = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(born + life < Time.time)
            Destroy(gameObject);
    }
}
