using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using DG.Tweening;

public class Parent : MonoBehaviour
{
    public eEntityType eEntityType = eEntityType.ENTITY_PLAYER;

	public	Camera		visionCamera;


    [System.Serializable]
    public class AnimationInfo
    {
        public eAnimationType animationType;

        public Transform targetTransform;

        public string animationName = "";

		public	bool	staticPosition = true;

		public	DOTweenPath	path;
    }

    public AnimationInfo GetAnimationInfo(eAnimationType animationType)
    {
        foreach (AnimationInfo animationInfo in animationInfos)
        {
            if (animationInfo.animationType == animationType)
                return animationInfo;
        }

        return null;
    }

    public eAnimationType currentAnimation  = eAnimationType.ANIMATION_SLEEP;

    public Animator animator;

    public AnimationInfo[] animationInfos;

    public void CaughtPlayer()
    {
		transform.DOKill();
		SetAnimationState(eAnimationType.ANIMATION_BUST);
    }

	void Start()
	{
		SetAnimationState(currentAnimation );
	}

	public	void EnableVision()
	{
		StopCoroutine("StartLooking");
		StartCoroutine("StartLooking");
	}

    public void SetAnimationState(eAnimationType animationType)
    {
        AnimationInfo animationInfo = GetAnimationInfo(animationType);
        Transform targetTransform = animationInfo.targetTransform;

        if (targetTransform != null)
            transform.position = targetTransform.position;

		print(animationInfo.animationName);

		if(!animationInfo.staticPosition)
		{
			transform.DOKill();
			transform.DOPath(animationInfo.path.wps.ToArray(), 1f).SetSpeedBased().SetEase(Ease.Linear).SetLookAt(0.1f).SetLoops(-1, LoopType.Restart);
		}

        animator.SetTrigger(animationInfo.animationName);
    }

	IEnumerator StartLooking ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(visionCamera);
			bool visible = GeometryUtility.TestPlanesAABB(planes, GameManager.Instance.GetPlayerBounds());

			if(visible)
			{
				GameManager.Instance.CaughtPlayer();
				break;
			}

			if(GameManager.Instance.GameOver)
			{
				break;
			}
		}
	}

    public void LookAtPosition(Vector3 position)
    {
        position.y = transform.position.y;

        transform.LookAt(position);
    }
}
