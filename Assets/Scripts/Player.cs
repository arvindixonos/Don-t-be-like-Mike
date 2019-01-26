using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Player : MonoBehaviour
{

    public static Player Instance = null;

    public string playerName = "LiveWire";

    public bool isActive = false;

	public	float	moveSpeed = 3f;

	public	float	moveSpeedInc = 0.1f;

	public	Camera		playerCamera = null;

	public	void	CamLookAt(Transform target)
	{
		
	}

	public	void	OnHit(eHomeObject	homeObject)
	{

	}

	void OnDestinationReached()
	{

	}

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	void OnDestroy()
	{
		Instance = null;
	}
}
