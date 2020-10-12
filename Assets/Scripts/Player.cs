﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 0.05f;
    public BowController weapon;

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
            weapon.startShooting();
        }
        else
        {
            weapon.stopShooting();
        }
    }
}
