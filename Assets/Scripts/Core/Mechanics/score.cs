using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{


    
    [Header("Score UI")]
    [Tooltip("Score text")] public TMP_Text scoreText;

    [Header("Score Settings")]
    [Tooltip("Score Holder")] public int playerScore;
    [Tooltip("Multiplier for the score")] public int scoreMultiplier;
    [Tooltip("Cooldown Between Gaining Points")] public float timeBetweenScoring = 0.1f;
    


    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(addScore());
        
    }

    IEnumerator addScore(){
        while(true){
            playerScore += scoreMultiplier;
            // Debug.Log($"Player Score : {playerScore}");
            scoreText.text = ($"Score : {playerScore}");
            yield return new WaitForSeconds(timeBetweenScoring);
        }
       
        
    }
}
