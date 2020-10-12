﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : PlayerBulletController
{
    public override void move()
    {
        transform.Translate(new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime);
    }
}
