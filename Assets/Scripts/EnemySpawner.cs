using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 13;
    public float height = 4f;
    public float enemySpeed = 1f;

    private bool movingRight = true;
    private float xmax;
    private float xmin;

    // Use this for initialization
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));
        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        // for every item in the transform (enemyformation), do something
        foreach (Transform child in transform)
        {
            // spawn with correct rotation and position
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.Euler(0, 0, -90)) as GameObject;
            // REPARENT >make sure each enemy is moved under the EnemySpawn/enemyformation gameobject
            enemy.transform.parent = child;
        }
    }
    // Create new gizmo so we can see how the formation will move.
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update() {
        if(movingRight)
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        //Reset movement when formation hits game edge
        if (leftEdgeOfFormation <xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }
}
