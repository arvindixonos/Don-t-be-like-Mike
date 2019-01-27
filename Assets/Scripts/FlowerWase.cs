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
            SoundManager.Instance.PlaySound(eSoundType.SOUND_FLOWER_WASE_BREAK, eSoundSourceType.SOUND_SOURCE_GENERAL,
            0.1f, 0f);
            
            GameManager.Instance.CaughtPlayer();
        }
    }
}
