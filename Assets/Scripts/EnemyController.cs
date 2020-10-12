using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth = 5f;
    private float health;
    private float shootingDelay = 0.5f;
    private float currentShootingDelay = 0f;
    private float shootingSpeed = 10f;
    public BulletController bullet;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) return;

        GameObject player = GameObject.FindWithTag("Player");
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);

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

    public void damage(float strength)
    {
        if (health <= 0) return;
        health -= strength;
        if (health <= 0)
        {
            die();
        }
        else
        {
            Debug.Log("hit");
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    void shoot()
    {
        BulletController newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.movementSpeed = shootingSpeed;
    }
}
