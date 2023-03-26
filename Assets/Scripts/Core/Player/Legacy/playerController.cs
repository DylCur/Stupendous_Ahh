using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("Lane Parameters")]

    [Range(1, 3)] public int currentLane = 1;
    int startingZ;



    [Header("Movement Parameters")]

    public float playerSpeed = 5f;
    public float forewardSpeed = 2f;

    Rigidbody rb;

    [Header("Debug")]

    [SerializeField] float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(forewardSpeed, rb.velocity.y, -horizontalInput * playerSpeed);
    }
}
