using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coins : MonoBehaviour
{

    

    [Header("Coin Settings")]

    [SerializeField] TMP_Text coinText;
    [Tooltip("Your total amount of coins")] public int totalCoins;
    [Tooltip("The amount of coins per coin")] public int coinMultiplier = 1;
    bool cooldown;


    // Start is called before the first frame update
    void Start()
    {
        coinText.text = ($"Coins: {totalCoins}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Collision (Coin)");
        
        if(other.tag == "Coin"){
            Debug.Log("Coin");
            totalCoins += coinMultiplier;
            Destroy(other);
            coinText.text = ($"Coins: {totalCoins}");
        }
    }
}
