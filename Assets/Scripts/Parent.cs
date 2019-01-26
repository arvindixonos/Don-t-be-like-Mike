using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Parent : MonoBehaviour
{
    public eEntityType eEntityType = eEntityType.ENTITY_PLAYER;

	public	float	visionRange = 10f;

	public	void	CaughtPlayer()
	{

	}

	public	bool	isPlayerVisible()
	{
		return false;
	}
}
