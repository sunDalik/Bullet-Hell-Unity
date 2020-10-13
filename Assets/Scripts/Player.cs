using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    public Shooter weapon;
    public CameraScript cameraObject;
    private int maxHealth = 5;
    private int health;
    private const float IFrameTime = 0.7f;
    private float currentIFrame = 0;
    private const float IFlashPeriod = IFrameTime / 3f;
    private float currentIFlashTime = 0f;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        Vector3 position = transform.position;
        position.z += Input.GetAxisRaw("Vertical") * movementSpeed;
        position.x += Input.GetAxisRaw("Horizontal") * movementSpeed;
        transform.position = position;

        if (Input.GetMouseButton(0))
        {
            weapon.shooting = true;
        }
        else
        {
            weapon.shooting = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            damage();
        }

        updateIFrames();
    }

    public void damage()
    {
        if (currentIFrame > 0) return;
        health--;
        cameraObject.shake();
        if (health <= 0)
        {
            die();
        }
        else
        {
            currentIFrame = IFrameTime;
        }
    }

    void updateIFrames()
    {
        currentIFrame -= Time.deltaTime;
        currentIFlashTime -= Time.deltaTime;
        if (currentIFrame > 0)
        {
            if (currentIFlashTime <= 0) currentIFlashTime = IFlashPeriod;
            if (currentIFlashTime >= IFlashPeriod / 2)
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
