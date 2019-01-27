using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using DG.Tweening;

public class Parent : MonoBehaviour
{
    public eEntityType eEntityType = eEntityType.ENTITY_PLAYER;

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

    public eAnimationType startAnimation = eAnimationType.ANIMATION_SLEEP;

    public Animator animator;

    public AnimationInfo[] animationInfos;

    public void CaughtPlayer()
    {
		SetAnimationState(eAnimationType.ANIMATION_BUST);
    }

	void Start()
	{
		SetAnimationState(startAnimation);
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

    public bool isPlayerVisible()
    {
        return false;
    }

    public void LookAtPosition(Vector3 position)
    {
        position.y = transform.position.y;

        transform.LookAt(position);
    }
}
