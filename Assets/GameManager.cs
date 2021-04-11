using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public float score = 0;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void incrementScore() {
        score += 1;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
