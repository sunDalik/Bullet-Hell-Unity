using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.15f;
    public float height = 12f;
    public float zOffset = -2f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
         Vector3 rotation = transform.eulerAngles;
         rotation.x = 70;
         transform.eulerAngles = rotation;
    }

    void Update()
    {
        Vector3 pos = new Vector3(player.position.x, player.position.y + height, player.position.z + zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
    }
}
