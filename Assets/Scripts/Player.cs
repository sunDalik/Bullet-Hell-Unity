﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;

    public Shooter weapon1;
    public Shooter weapon2;
    Shooter activeWeapon;

    public CameraScript cameraObject;
    private int maxHealth = 6;
    private int health;
    private const float IFrameTime = 0.7f;
    private float currentIFrame = 0;
    private const float IFlashPeriod = IFrameTime / 3f;
    private float currentIFlashTime = 0f;
    public ParticleSystem onDamageParticles;

    public ParticleSystem walkingParticles;
    private const float walkingParticlesDelay = 0.1f;
    private float currentWalkingParticlesDelay = walkingParticlesDelay;

    private const float dashDelay = 0.8f;
    private float currentDashDelay = dashDelay;
    private bool dashing = false;
    private const float dashTime = 0.185f;
    private float currentDashTime = 0f;
    private const float dashSpeed = 38f;
    private Vector3 dashDir = Vector3.zero;

    private bool inWater = false;

    public PlayerHUD HUD;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        activeWeapon = weapon1;
        weapon2.setVisibility(false);
        redrawHealth();
        cameraObject.centerCameraOnPlayer(0);
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

        Vector3 oldPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Input.GetMouseButton(1) && currentDashDelay >= dashDelay)
        {
            dash();
        }

        if (dashing)
        {
            if (currentDashTime < dashTime)
            {
                float slices = 6;
                for (int i = 0; i < slices; i++)
                {
                    float movMultiplier = dashSpeed * getSpeedMultiplier() * Time.deltaTime / slices;
                    Vector3 destination = transform.position;
                    destination.x += dashDir.x * movMultiplier;
                    destination.z += dashDir.z * movMultiplier;
                    bool intersection = Physics.Linecast(transform.position, destination);
                    if (intersection)
                    {
                        currentDashTime = dashTime;
                        break;
                    };
                    transform.position = destination;
                    emitWalkingParticles(oldPos, true);
                }
                currentDashTime += Time.deltaTime;

            }
            else
            {
                dashing = false;
            }
        }
        else
        {
            Vector3 position = transform.position;
            Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            position.z += movementVector.z * movementSpeed * getSpeedMultiplier() * Time.deltaTime;
            position.x += movementVector.x * movementSpeed * getSpeedMultiplier() * Time.deltaTime;
            transform.position = position;

            if (transform.position.z != oldPos.z || transform.position.x != oldPos.x)
            {
                if (currentWalkingParticlesDelay >= walkingParticlesDelay)
                {
                    emitWalkingParticles(oldPos);
                    currentWalkingParticlesDelay = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            activeWeapon.setVisibility(false);
            activeWeapon.shooting = false;
            if (activeWeapon == weapon1)
            {
                activeWeapon = weapon2;
            }
            else
            {
                activeWeapon = weapon1;
            }
            activeWeapon.setVisibility(true);
            if (activeWeapon.currentShootingDelay < activeWeapon.shootingDelay / 2)
            {
                activeWeapon.currentShootingDelay = activeWeapon.shootingDelay / 2;
            }
        }

        if (Input.GetMouseButton(0))
        {
            activeWeapon.shooting = true;
        }
        else
        {
            activeWeapon.shooting = false;
        }

        updateIFrames();
        currentWalkingParticlesDelay += Time.deltaTime;
        currentDashDelay += Time.deltaTime;
    }

    public void damage()
    {
        if (currentIFrame > 0) return;
        health--;
        cameraObject.shake();
        if (onDamageParticles)
        {
            Instantiate(onDamageParticles, transform.position, new Quaternion());
        }
        if (health <= 0)
        {
            die();
        }
        else
        {
            currentIFrame = IFrameTime;
        }
        redrawHealth();
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
        //GetComponent<Renderer>().enabled = false;
    }

    void dash()
    {
        float zMovement = Input.GetAxisRaw("Vertical");
        float xMovement = Input.GetAxisRaw("Horizontal");
        if (zMovement == 0 && xMovement == 0) return;
        dashDir = new Vector3(xMovement, 0, zMovement).normalized;
        dashing = true;
        currentDashDelay = 0;
        currentDashTime = 0;
    }

    void emitWalkingParticles(Vector3 oldPos, bool dash = false)
    {
        if (inWater) return;
        List<int> angleYs = new List<int>();
        float ZMovement = Math.Sign(transform.position.z - oldPos.z);
        float XMovement = Math.Sign(transform.position.x - oldPos.x);
        if (ZMovement > 0)
        {
            angleYs.Add(180);
        }
        else if (ZMovement < 0)
        {
            angleYs.Add(0);
        }

        if (XMovement > 0)
        {
            angleYs.Add(-90);
        }
        else if (XMovement < 0)
        {
            angleYs.Add(90);
        }

        float angleY = 0;
        foreach (int number in angleYs)
        {
            angleY += number;
        }
        angleY /= angleYs.Count;

        if (ZMovement > 0 && XMovement > 0)
        {
            angleY = 225;
        }

        //no idea what I am doing
        //Basically I determine the angle of walking particles emission here but I couldnt come up with a formula and thus its messy

        ParticleSystem particleSystem = Instantiate(walkingParticles, transform.position, Quaternion.Euler(0, angleY, 0));
        if (dash)
        {
            particleSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 1) });
        }

        IEnumerator DestroyParticles()
        {
            yield return new WaitForSeconds(2f);
            Destroy(particleSystem);
        }

        StartCoroutine(DestroyParticles());
    }

    void redrawHealth()
    {
        foreach (Transform child in HUD.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 1; i <= maxHealth; i += 2)
        {
            CanvasRenderer heartSprite = HUD.fullHeartSprite;
            if (health == i)
            {
                heartSprite = HUD.halfHeartSprite;
            }
            else if (health < i)
            {
                heartSprite = HUD.emptyHeartSprite;
            }

            CanvasRenderer newHeartSprite = Instantiate(heartSprite, HUD.transform);

            RectTransform newHeartRect = newHeartSprite.GetComponent<RectTransform>();
            Vector3 pos = newHeartRect.anchoredPosition;
            pos.x = newHeartRect.rect.width / 2 + 7 + newHeartRect.rect.width * 1.03f * ((i - 1) / 2);
            pos.y = -newHeartRect.rect.height / 2 - 5;
            newHeartRect.anchoredPosition = pos;
        }
    }

    float getSpeedMultiplier()
    {
        if (inWater) return 0.6f;
        else return 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            inWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            inWater = false;
        }
    }

    public bool isDead()
    {
        return health <= 0;
    }
}
