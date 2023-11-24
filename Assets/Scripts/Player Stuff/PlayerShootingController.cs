using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    Vector2 shootDirection;
    // https://docs.unity3d.com/ScriptReference/Time-time.html
    //public float fireRate = 0.3f;
    [SerializeField] FloatValue fireRate;
    float nextFire = 0.0f;
    // 1 - default, 3 - cone thing, 2 - wagon wheel
    int gunType = 1;

    Animator animator;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (shootDirection != Vector2.zero && Time.time > nextFire) {
            nextFire = Time.time + fireRate.RuntimeValue;
            animator.SetFloat("Shoot X", shootDirection.x);
            animator.SetFloat("Shoot Y", shootDirection.y);
            animator.SetBool("isShooting", true);
            //ShootBullet();
            DecideGun();
        } else if (shootDirection == Vector2.zero) {
            animator.SetBool("isShooting", false);
        }
    }

    private void DecideGun() {
        switch(gunType)
        {
            case 1: // default gun
                ShootBullet();
                break;
            case 2: // wagon wheel takes predence over cone-shape
                break;
            case 3: // cone shape
                break;
        }
    }

    void OnFire(InputValue shootDirectionValue) {
        shootDirection = shootDirectionValue.Get<Vector2>();
    }

    void ShootBullet() {
        // Quaternion.identity means no rotation
        GameObject bulletObject = Instantiate(bulletPrefab, rb.position + shootDirection * 0.5f, Quaternion.identity);
        BulletScript bullet = bulletObject.GetComponent<BulletScript>();
        bullet.Shoot(shootDirection);
    }
}
