using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public static Level Instance = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public abstract void InitLevel();

    public abstract void EndLevel();
}
