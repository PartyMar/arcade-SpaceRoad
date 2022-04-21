using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherShip : Ship
{
    [SerializeField]
    Collider2D triggerForSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Other")
        {
            other.gameObject.GetComponent<OtherShip>().setSpeed(this.speed);

        }

    }
}
