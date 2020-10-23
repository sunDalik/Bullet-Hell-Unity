using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    override public void Start()
    {
        base.Start();
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 90;
        transform.eulerAngles = rotation;
    }

    public override void move()
    {
        transform.Translate(new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime);
    }
}
