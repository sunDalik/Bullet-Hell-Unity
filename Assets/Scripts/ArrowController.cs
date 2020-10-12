using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float movementSpeed = 0f;
    private float currentLifeTime = 0f;
    private float maxLifeTime = 10f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime);
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
