using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGeneration : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Wall")] public GameObject wall;
    [Tooltip("Short wall")] public GameObject shortWall;

    [Header("Wall properties")]
    [Tooltip("An integer to limit the amount of walls at once")] public int maxWalls = 100;
    [Tooltip("Minimum X")] public int minX = -2;
    [Tooltip("Mid X")] public int midX = 0;
    [Tooltip("Maximum X")] public int maxX = 2;
    [SerializeField] private float wallOverlapRadius = 1.0f;
    [SerializeField] private LayerMask shortWallLayerMask;
    [SerializeField] private LayerMask wallLayerMask;




    [Header("Other")]
    [SerializeField] Transform player;
    

    private float prevWallZ;

    private int[] xPositions = new int[] { -2, 0, 2 };

    // Start is called before the first frame update
    void Start()
    {
        prevWallZ = player.position.z;

        for (int i = 0; i < maxWalls; i++)
        {
            int wallChoice = Random.Range(1, 3);
            int x = xPositions[Random.Range(0, xPositions.Length)];

            float z = Random.Range(prevWallZ + 10f, prevWallZ + 30f);

            if (wallChoice == 1 && CanSpawnWall(x, z))
            {
                Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                prevWallZ = z;
            }
            else if (CanSpawnWall(x, z))
            {
                Instantiate(shortWall, new Vector3(x, 0, z), Quaternion.identity);
                prevWallZ = z;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool CanSpawnWall(int xPos, float zPos)
{
    // Check if there's already a wall at this position
    Collider[] colliders = Physics.OverlapSphere(new Vector3(xPos, 0, zPos), wallOverlapRadius, wallLayerMask);
    if (colliders.Length > 0)
    {
        return false;
    }

    // Check if there's a short wall to the left or right
    int shortWallsToTheLeft = 0;
    int shortWallsToTheRight = 0;
    for (int i = 1; i <= 3; i++)
    {
        Collider[] leftColliders = Physics.OverlapSphere(new Vector3(xPos - i, 0, zPos), wallOverlapRadius, shortWallLayerMask);
        if (leftColliders.Length > 0)
        {
            shortWallsToTheLeft++;
        }
        else
        {
            break;
        }
    }
    for (int i = 1; i <= 3; i++)
    {
        Collider[] rightColliders = Physics.OverlapSphere(new Vector3(xPos + i, 0, zPos), wallOverlapRadius, shortWallLayerMask);
        if (rightColliders.Length > 0)
        {
            shortWallsToTheRight++;
        }
        else
        {
            break;
        }
    }
    if (shortWallsToTheLeft >= 3 || shortWallsToTheRight >= 3)
    {
        return false;
    }

    return true;
}

}
