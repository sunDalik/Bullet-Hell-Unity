using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    player,
    enemy
}

public class BulletController : MonoBehaviour
{
    public float movementSpeed = 0f;
    private float currentLifeTime = 0f;
    private float maxLifeTime = 10f;
    public float strength = 1f; // only used for players' bullets
    public BulletType bulletType = BulletType.enemy;

    // Update is called once per frame
    void Update()
    {
        move();
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void move()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (bulletType == BulletType.player)
        {
            if (other.gameObject.tag == "Enemy")
            {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                enemy.damage(strength);
                Destroy(gameObject);
            }
        }
        else if (bulletType == BulletType.enemy)
        {
            if (other.gameObject.tag == "Player")
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.damage();
                Destroy(gameObject);
            }
        }
    }
}
