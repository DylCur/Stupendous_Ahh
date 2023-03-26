using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionController : MonoBehaviour
{

    [Tooltip("Obstacle Layermask ")] [SerializeField] LayerMask obstacleMask;
    
    void OnCollisionEnter(Collision other){
        Debug.Log("Collided");

        if (((1 << other.gameObject.layer) & obstacleMask) != 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
        }
    }

    



}
