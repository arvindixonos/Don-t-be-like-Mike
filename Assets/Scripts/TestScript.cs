using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public	bool	visible;
	public	Collider playerCollider;
	private	bool	looking;
	private Plane[] planes;

	void Start()
    {
		StartCoroutine("StartLooking");
    }

	IEnumerator StartLooking ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			visible = GeometryUtility.TestPlanesAABB(planes, playerCollider.bounds);
		}
	}
}
