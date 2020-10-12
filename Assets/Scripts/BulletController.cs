using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float movementSpeed = 0f;
    private float currentLifeTime = 0f;
    private float maxLifeTime = 10f;
    public float strength = 1f;

    // Update is called once per frame
    void Update()
    {
        move();
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void move()
    {
    }
}
