using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{

    public ArrowController arrowPrefab;
    private bool shooting = false;
    private float shootingDelay = 0.3f;
    private float currentShootingDelay = 0f;
    private float shootingSpeed = 30f;

    public void startShooting()
    {
        shooting = true;
    }

    public void stopShooting()
    {
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingDelay > 0)
        {
            currentShootingDelay -= Time.deltaTime;
        }


        if (shooting)
        {
            if (currentShootingDelay <= 0)
            {
                currentShootingDelay = shootingDelay;
                createArrow();
            }
        }
    }

    private void createArrow()
    {
        ArrowController newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        newArrow.movementSpeed = shootingSpeed;
    }
}
