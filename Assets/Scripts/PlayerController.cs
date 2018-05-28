using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 8f;
    public GameObject projectile;
    public float projectileSpeed = 8f;
    public float fireRate = 0.5f;
    public float health = 1000f;

    private float padding = 0.3f;

    float xmin, xmax;

    void Start() {
        // Get the distance from the camera to the plain you are projecting to
        float distance = transform.position.z - Camera.main.transform.position.z;
        // Create left and right most edges of the screen by getting the boundaries of the view point
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        // Assigning the x velue of the above vectors to our xmin and xmax and add some padding to it
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
    }

    void Fire() {
        Vector3 startPosition = transform.position + new Vector3(0.2f, 0.25f, 0);
        GameObject bullet = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        // Restrict the player movement to the view point/screen boundaries using Mathf.Clamp
        // define range limit
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        // Force position limit
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        if (bullet)
        {
            health -= bullet.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}