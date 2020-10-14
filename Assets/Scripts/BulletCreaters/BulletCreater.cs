using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreater : MonoBehaviour
{
    public BulletController bullet;
    public bool shooting = false;
    public float shootingDelay = 0.7f;
    private float currentShootingDelay = 0f;
    public float shootingSpeed = 10f;
    public float bulletLifeTime = 8f;

    // Update is called once per frame
    void Update()
    {
        if (currentShootingDelay > 0)
        {
            currentShootingDelay -= Time.deltaTime;
        }
        if (currentShootingDelay <= 0)
        {
            currentShootingDelay = shootingDelay;
            shoot();
        }
    }

    public virtual void shoot()
    {
    }

    protected void afterBulletCreation(BulletController newBullet)
    {
        newBullet.movementSpeed = shootingSpeed;
        newBullet.maxLifeTime = bulletLifeTime;

        Vector3 newPosition = newBullet.transform.position;
        newPosition.y = 0.5f;
        newBullet.transform.position = newPosition;
    }
}
