using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class HomeObject : MonoBehaviour
{
    public abstract void OnHit(eEntityType entityType);
    
}
