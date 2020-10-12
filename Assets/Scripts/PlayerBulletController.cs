using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : BulletController
{
    public float strength = 1f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.damage(strength);
            Destroy(gameObject);
        }
    }
}
