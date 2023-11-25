using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class BulletSpawnPoint
{
    public GameObject spawnPoint;
    public Vector2 direction;
}

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    Vector2 shootDirection;
    // https://docs.unity3d.com/ScriptReference/Time-time.html
    //public float fireRate = 0.3f;
    [SerializeField] FloatValue fireRate;
    float nextFire = 0.0f;
    // 1 - default, 2 - cone thing, 3 - wagon wheel
    int gunType = 1;

    Animator animator;
    Rigidbody2D rb;

    private Vector2 SWDirection = new Vector2(-0.71f, -0.71f);
    private Vector2 SEDirection = new Vector2(0.71f, -0.71f);
    // private Vector2 NWDirection = new Vector2(0.71f, -0.71f);
    private Vector2 NEDirection = new Vector2(0.71f, 0.71f);

    [SerializeField] List<BulletSpawnPoint> pointsToSpawnAt = new List<BulletSpawnPoint>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(shootDirection);
        if (shootDirection != Vector2.zero && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate.RuntimeValue;
            animator.SetFloat("Shoot X", shootDirection.x);
            animator.SetFloat("Shoot Y", shootDirection.y);
            animator.SetBool("isShooting", true);
            //ShootBullet();
            DecideGun();
        } else if (shootDirection == Vector2.zero)
        {
            animator.SetBool("isShooting", false);
        }
    }

    private void DecideGun() {
        switch(gunType)
        {
            case 1: // default gun
                ShootBullet();
                break;
            case 2: // cone shape
                ChooseConePattern();
                break;
            case 3: // wagon wheel
                ShootAllDirections();
                break;
        }
    }

    void OnFire(InputValue shootDirectionValue)
    {
        shootDirection = shootDirectionValue.Get<Vector2>();
    }

    void ShootBullet()
    {
        // Quaternion.identity means no rotation
        GameObject bulletObject = Instantiate(bulletPrefab, rb.position + shootDirection * 0.5f, Quaternion.identity);
        BulletScript bullet = bulletObject.GetComponent<BulletScript>();
        bullet.Shoot(shootDirection);
    }

    void ChooseConePattern()
    {
        if (shootDirection.Equals(Vector2.up))
        {

        } else if (shootDirection.Equals(Vector2.down))
        {

        } else if (shootDirection.Equals(Vector2.left))
        {

        } else if (shootDirection.Equals(Vector2.right))
        {

        } else if (shootDirection.Equals(SEDirection))
        {

        } else if (shootDirection.Equals(SWDirection))
        {

        } else if (shootDirection.Equals(NEDirection))
        {

        } else
        {

        }
        ShootConePattern();
    }

    private void ShootConePattern()
    {

    }

    private void ShootAllDirections()
    {
        for (int i = 0; i < pointsToSpawnAt.Count; i++)
        {
            Vector2 bulletDirection = pointsToSpawnAt[i].direction;
            Vector2 spawnPosition = pointsToSpawnAt[i].spawnPoint.transform.position;
            GameObject bulletObject = Instantiate(bulletPrefab, spawnPosition + bulletDirection * 0.5f, Quaternion.identity);
            BulletScript bullet = bulletObject.GetComponent<BulletScript>();
            bullet.Shoot(bulletDirection);
        }
    }

}
