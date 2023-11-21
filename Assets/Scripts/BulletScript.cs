using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript: MonoBehaviour {

    Rigidbody2D rb;

    // force of bullet
    public float force = 300f;

    public int damage = 1;

    // Awake is called immediately when bullet is created (instantiate called in PlayerController)
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        // Destroy bullet when it is far enough away from the center of the screen
        if(transform.position.magnitude > 10.0f) {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 direction) {
        rb.AddForce(direction * force);
    }

    // Detect collisions
    void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Projectile Collision with " + other.gameObject);
        //SpringtrapController e = other.collider.GetComponent<SpringtrapController>();
        Enemy e = other.collider.GetComponent<Enemy>();
            if (e != null) {
                e.DecreaseHealth(damage);
            }
        Destroy(gameObject);
    }
}
