using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingBulletCreater : BulletCreater
{
    public override void shoot()
    {
        BulletController newBullet = Instantiate(bullet, transform.position, transform.rotation);
        afterBulletCreation(newBullet);
    }
}
