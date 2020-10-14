using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth = 5f;
    private float health;
    public bool rotatingToPlayer = true;
    public ParticleSystem onDamageParticles;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        startShootingAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatingToPlayer)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }

    public void damage(float strength)
    {
        if (onDamageParticles) onDamageParticles.Play();
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

    void startShootingAll()
    {
        BulletCreater[] bulletCreaters = GetComponents<BulletCreater>();
        foreach (BulletCreater bc in bulletCreaters)
        {
            bc.shooting = true;
        }
    }
}
