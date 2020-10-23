using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBoltController : BulletController
{
    public int sinSign = 1;
    private float x = 0;

    public override void move()
    {
        x += Time.deltaTime * 17;
        Vector3 movementVector = new Vector3(-1, 0, 0);
        movementVector.z = Mathf.Sin(x) * sinSign * 0.85f;
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);
    }
}
