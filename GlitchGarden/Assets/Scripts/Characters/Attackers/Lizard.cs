﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Enemy
{
    protected override void SetBehevior(Defender defender)
    {
        Attack(defender);
    }
}
