using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    public override void move()
    {
        transform.Translate(new Vector3(0, 0, 1) * movementSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.damage();
            Destroy(gameObject);
        }
    }
}
