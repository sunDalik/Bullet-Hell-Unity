using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBoltShooter : Shooter
{
    void Start()
    {
        shootingDelay = 0.2f;
        shootingSpeed = 18f;
        ownStrength = 0.45f;
    }

    public override void createBullet()
    {
        for (int i = -1; i <= 1; i += 2)
        {
            MagicalBoltController newBullet = (MagicalBoltController)Instantiate(bullet, transform.position, transform.rotation);
            newBullet.movementSpeed = shootingSpeed;
            newBullet.strength = ownStrength;
            newBullet.sinSign = i;
        }
    }
}
