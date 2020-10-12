using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth = 5f;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damage(float strength)
    {
        Debug.Log("hit");
        if (health <= 0) return;
        health -= strength;
        if (health <= 0)
        {
            die();
        }
        else
        {
            
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
