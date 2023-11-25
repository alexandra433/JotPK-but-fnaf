using System.Collections;
using System.Collections.Generic;
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
    // 1 - default, 3 - cone thing, 2 - wagon wheel
    int gunType = 1;

    Animator animator;
    Rigidbody2D rb;

    public BulletSpawnPoint NSpot;
    public BulletSpawnPoint ESpot;
    public BulletSpawnPoint WSpot;
    public BulletSpawnPoint SSpot;
    public BulletSpawnPoint NESpot;
    public BulletSpawnPoint NWSpot;
    public BulletSpawnPoint SESpot;
    public BulletSpawnPoint SWSpot;

    private Vector2 SWDirection = new Vector2(-0.71f, -0.71f);
    private Vector2 SEDirection = new Vector2(0.71f, -0.71f);
    // private Vector2 NWDirection = new Vector2(0.71f, -0.71f);
    private Vector2 NEDirection = new Vector2(0.71f, 0.71f);

    List<BulletSpawnPoint> pointsToSpawnAt = new List<BulletSpawnPoint>();

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
            case 2: // wagon wheel takes predence over cone-shape
                break;
            case 3: // cone shape
                ChooseConePattern();
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
            pointsToSpawnAt.Add(NSpot);
            pointsToSpawnAt.Add(NESpot);
            pointsToSpawnAt.Add(NWSpot);
        } else if (shootDirection.Equals(Vector2.down))
        {
            pointsToSpawnAt.Add(SSpot);
            pointsToSpawnAt.Add(SESpot);
            pointsToSpawnAt.Add(SWSpot);
        } else if (shootDirection.Equals(Vector2.left))
        {
            pointsToSpawnAt.Add(WSpot);
            pointsToSpawnAt.Add(SWSpot);
            pointsToSpawnAt.Add(NWSpot);
        } else if (shootDirection.Equals(Vector2.right))
        {
            pointsToSpawnAt.Add(ESpot);
            pointsToSpawnAt.Add(SESpot);
            pointsToSpawnAt.Add(NESpot);
        } else if (shootDirection.Equals(SEDirection))
        {
            pointsToSpawnAt.Add(SSpot);
            pointsToSpawnAt.Add(SESpot);
            pointsToSpawnAt.Add(ESpot);
        } else if (shootDirection.Equals(SWDirection))
        {
            pointsToSpawnAt.Add(SSpot);
            pointsToSpawnAt.Add(SWSpot);
            pointsToSpawnAt.Add(WSpot);
        } else if (shootDirection.Equals(NEDirection))
        {
            pointsToSpawnAt.Add(NSpot);
            pointsToSpawnAt.Add(NESpot);
            pointsToSpawnAt.Add(ESpot);
        } else
        {
            pointsToSpawnAt.Add(NSpot);
            pointsToSpawnAt.Add(NWSpot);
            pointsToSpawnAt.Add(WSpot);
        }
        ShootConePattern();
    }

    private void ShootConePattern()
    {
        for (int i = 0; i < pointsToSpawnAt.Count; i++)
        {
            // Debug.Log(pointsToSpawnAt[i].direction);
            Vector2 currentShootDirection = pointsToSpawnAt[i].direction;
            GameObject bulletObject = Instantiate(bulletPrefab, rb.position + currentShootDirection * 0.5f, Quaternion.identity);
            BulletScript bullet = bulletObject.GetComponent<BulletScript>();
            bullet.Shoot(currentShootDirection);
        }
        pointsToSpawnAt.Clear();
    }
}
