using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    public override void move()
    {
        transform.Translate(new Vector3(0, 0, 1) * movementSpeed * Time.deltaTime);
    }
}
