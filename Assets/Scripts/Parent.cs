using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Parent : MonoBehaviour
{
    public eEntityType eEntityType = eEntityType.ENTITY_PLAYER;

    [System.Serializable]
    public class AnimationInfo
    {
        public eAnimationType animationType;

        public Transform targetTransform;

        public string animationName = "";
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

    public Animator animator;

    public AnimationInfo[] animationInfos;

    public void CaughtPlayer()
    {
        AnimationInfo animationInfo = GetAnimationInfo(eAnimationType.ANIMATION_BUST);
        Transform targetTransform = animationInfo.targetTransform;

        if (targetTransform != null)
            transform.position = targetTransform.position;

        animator.SetTrigger(animationInfo.animationName);
    }

    public bool isPlayerVisible()
    {
        return false;
    }

	public	void	LookAtPosition(Vector3 position)
	{
		position.y = transform.position.y;

		transform.LookAt(position);
	}
}
