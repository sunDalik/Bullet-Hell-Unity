using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBoltShooter : Shooter
{
    private float shootingSpeed = 20f;
    private float ownStrength = 0.3f;

    void Start()
    {
        shootingDelay = 0.15f;
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
