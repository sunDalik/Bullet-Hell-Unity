using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletCreater : BulletCreater
{
    public int bulletAmount = 1;
    public float spreadAngle = 15f;

    public override void shoot()
    {
        float degreeOffset;

        int bulletLeftToSpawn = bulletAmount;
        if (bulletAmount % 2 == 1)
        {
            BulletController newBullet = Instantiate(bullet, transform.position, transform.rotation);
            afterBulletCreation(newBullet);
            bulletLeftToSpawn--;
            degreeOffset = spreadAngle;
        }
        else
        {
            degreeOffset = spreadAngle / 2;
        }

        for (int i = 0; i < bulletLeftToSpawn; i += 2)
        {
            BulletController newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            newBullet.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + degreeOffset, 0);
            afterBulletCreation(newBullet);

            BulletController newBullet2 = Instantiate(bullet);
            newBullet2.transform.position = transform.position;
            newBullet2.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - degreeOffset, 0);
            afterBulletCreation(newBullet2);

            degreeOffset += spreadAngle;
        }
    }
}
