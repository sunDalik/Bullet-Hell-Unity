using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public BulletController bullet;
    public bool shooting = false;
    public float shootingDelay = 0.3f;
    private float currentShootingDelay = 0f;
    public float shootingSpeed = 34f;
    public float ownStrength = 1.7f;

    // Update is called once per frame
    void Update()
    {
        if (currentShootingDelay > 0)
        {
            currentShootingDelay -= Time.deltaTime;
        }


        if (shooting)
        {
            if (currentShootingDelay <= 0)
            {
                currentShootingDelay = shootingDelay;
                createBullet();
            }
        }
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
}
