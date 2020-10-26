using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth = 5f;
    private float health;
    public bool rotatingToPlayer = true;
    public ParticleSystem onDamageParticles;
    public float noticeRange = 20f;
    private BulletCreater[] bulletCreaters;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        bulletCreaters = GetComponents<BulletCreater>();
        decideShooting(GameObject.FindWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null) return;
        if (rotatingToPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
        decideShooting(player);
    }

    public void damage(float strength)
    {
        if (onDamageParticles)
        {
            Instantiate(onDamageParticles, transform.position, new Quaternion());
        }
        if (health <= 0) return;
        health -= strength;
        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    void decideShooting(GameObject player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= noticeRange)
        {
            setShootingMode(true);
        }
        else
        {
            setShootingMode(false);
        }
    }

    void setShootingMode(bool shooting)
    {
        foreach (BulletCreater bc in bulletCreaters)
        {
            bc.shooting = shooting;
        }
    }
}
