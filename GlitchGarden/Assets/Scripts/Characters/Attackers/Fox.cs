using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Enemy
{
    
    

    protected override void SetBehevior(Defender defender)
    {

        var grave = defender.GetComponent<Grave>();
        if (grave)
        {
            Jump();
        }
        else
        {
            Attack(defender);           
        }
    }
}
