using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public BulletController bullet;
    public bool shooting = false;
    public float shootingDelay = 0.3f;
    public float currentShootingDelay = 0f;
    public float shootingSpeed = 34f;
    public float ownStrength = 1.7f;
    bool tilted = false;
    private float tiltDelay = 0;
    float tiltAngle = 25;

    // Update is called once per frame
    void Update()
    {
        if (currentShootingDelay > 0)
        {
            currentShootingDelay -= Time.deltaTime;
        }

        if (tiltDelay >= shootingDelay / 2 && tilted)
        {
            untilt();
        }

        if (shooting)
        {
            if (currentShootingDelay <= 0)
            {
                currentShootingDelay = shootingDelay;
                createBullet();
                tilt();
            }
        }

        tiltDelay += Time.deltaTime;
    }

    public virtual void createBullet()
    {
        BulletController newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.movementSpeed = shootingSpeed;
        newBullet.strength = ownStrength;
    }

    public void setVisibility(bool visibility)
    {
        foreach (Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer)
            {
                renderer.enabled = visibility;
            }
        }
    }

    void tilt()
    {
        if (!tilted)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y += tiltAngle;
            Vector3 currentVelocity = Vector3.one;
            transform.eulerAngles = rotation;
            tiltDelay = 0;
            tilted = true;
        }
    }

    void untilt()
    {
        if (tilted)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y -= tiltAngle;
            transform.eulerAngles = rotation;
            tilted = false;
        }
    }
}
