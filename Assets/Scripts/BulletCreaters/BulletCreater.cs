using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreater : MonoBehaviour
{
    public BulletController bullet;
    public bool shooting = false;
    private float shootingDelay = 0.7f;
    private float currentShootingDelay = 0f;
    protected float shootingSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingDelay > 0)
        {
            currentShootingDelay -= Time.deltaTime;
        }
        if (currentShootingDelay <= 0)
        {
            currentShootingDelay = shootingDelay;
            shoot();
        }
    }

    public virtual void shoot()
    {
    }
}
