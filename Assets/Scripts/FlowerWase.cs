using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class FlowerWase : HomeObject {

    public override void OnHit(eEntityType entityType)
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerEnter (Collider otherObject)
    {
        if(otherObject.tag == "Player")
        {
            GameManager.Instance.CaughtPlayer();
        }
    }
}
