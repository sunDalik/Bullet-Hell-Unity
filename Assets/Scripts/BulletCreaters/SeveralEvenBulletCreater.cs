using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeveralEvenBulletCreater : BulletCreater
{
    public int bulletAmount = 1;
    private float initDegree = 0f;
    public float degreeChange = 10f;

    public void Start()
    {
        initDegree = Random.Range(0, 360);
    }

    public override void shoot()
    {
        float newDegree = initDegree;
        for (int i = 0; i < bulletAmount; i++)
        {
            Debug.Log(newDegree);
            BulletController newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            newBullet.transform.eulerAngles = new Vector3(0, newDegree, 0);
            newBullet.movementSpeed = shootingSpeed;
            newDegree += 360 / bulletAmount;
        }
        initDegree += degreeChange;
    }
}
