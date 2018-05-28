using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject projectile;
    public float projectileSpeed = 8f;
    public float health = 150f;
    public float shotsPerSecond = 1f;

    void Update(){

        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability){
            Fire();
        }
    }

    void Fire() {
        Vector3 startPosition = transform.position + new Vector3(-0.2f, -0.6f, 0);
        GameObject bullet = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        if (bullet) {
            health -= bullet.GetDamage();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
