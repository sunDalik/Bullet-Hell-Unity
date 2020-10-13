using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.08f;
    public float height = 12f;
    public float zOffset = -2f;
    private Vector3 velocity = Vector3.zero;
    private float currentShakeScreenDuration = 0;
    private float shakeIntensity = 0.2f;

    void Start()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.x = 90;
        transform.eulerAngles = rotation;
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 mousePos = player.transform.position; // default value in case we don't detect mouse position
        Plane playerPlane = new Plane(Vector3.up, player.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            mousePos = ray.GetPoint(hitDistance);
        }

        float playerPosInfluence = 1.5f;
        Vector3 newPos = new Vector3((mousePos.x + playerPos.x * playerPosInfluence) / (1 + playerPosInfluence), player.position.y + height, (mousePos.z + playerPos.z * playerPosInfluence) / (1 + playerPosInfluence));
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        if (currentShakeScreenDuration > 0)
        {
            shakeCamera();
            currentShakeScreenDuration -= Time.deltaTime;
        }
    }

    public void shake(float duration = 0.2f)
    {
        currentShakeScreenDuration = duration;
    }

    private void shakeCamera()
    {
        Vector3 newPos = transform.position;
        newPos.x += Random.Range(-shakeIntensity, shakeIntensity);
        newPos.z += Random.Range(-shakeIntensity, shakeIntensity);
        transform.position = newPos;
    }
}
