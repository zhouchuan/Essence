/* ZC : Common */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerPressureScript : MonoBehaviour
{
	public string			targetTag;			// Tag name of the collision(interaction) object
	//public Animator			targetAnimation;	// Animator attached to the Controlled object by trigger.
	//public string			targetStateName;	// StateName(targetAnimation) which I want to play.
	public Animator			selfAnimation;		// Animator attached to the button itself ;
	//public string			selfStateName;		// StateName(selfAnimation) which I want to play.
	public List<TweenPostion3D>	targetPlatformList;

	private List<Vector3>	fromPosList = new List<Vector3>();
	private List<Vector3>	toPosList = new List<Vector3>();
	private List<bool>		arrivalCountList = new List<bool>();
	private bool			isPlayInverse = false;
	private bool			isTirggerAnimation = false;


	void Awake()
	{

		for(int i = 0; i < targetPlatformList.Count; i++)
		{
			fromPosList.Add(targetPlatformList[i].fromPos);
			toPosList.Add(targetPlatformList[i].toPos);
			arrivalCountList.Add(false);
		}
	}

	void OnTriggerEnter(Collider collision) 
	{
		if((collision.gameObject.tag == "Player" || collision.gameObject.tag == targetTag) && !isTirggerAnimation)
		{
			ButtonOnStatus();
		}
	}

	void OnTriggerStay(Collider collision)
	{
		if((collision.gameObject.tag == "Player" || collision.gameObject.tag == targetTag)  && isTirggerAnimation)
		{
			ButtonStayStatus();
		}
	}

	void OnTriggerExit(Collider collision)
	{
		if(collision.gameObject.tag == targetTag || collision.gameObject.tag == "Player" )
		{
			ButtonOffStatus();
		}
	}


	void Update()
	{
		if(isPlayInverse)
		{
			bool isAllTrue = true;
			for(int i = 0; i < targetPlatformList.Count; i++)
			{
				targetPlatformList[i].TweenPos();
				isAllTrue = isAllTrue && (targetPlatformList[i].transform.localPosition == targetPlatformList[i].toPos);
			}
			isPlayInverse = !isAllTrue;
		}
	}


	void NoBoxStatus()
	{
		selfAnimation.SetBool("isPressed",false);
		selfAnimation.SetBool("hasBox",false);
	}

	void ButtonOnStatus()
	{
		isPlayInverse = false;
		isTirggerAnimation = true;
		selfAnimation.SetBool("isPressed",false);
		selfAnimation.SetBool("hasBox", true);
		for(int i = 0; i < targetPlatformList.Count; i++)
		{
			targetPlatformList[i].fromPos = targetPlatformList[i].gameObject.transform.localPosition;
			targetPlatformList[i].toPos = toPosList[i];
			targetPlatformList[i].StartTime = Time.time;
		}
	}

	void ButtonStayStatus()
	{
		selfAnimation.SetBool("isPressed",true);
		selfAnimation.SetBool("hasBox", true);
		for(int i = 0; i < targetPlatformList.Count; i++)
		{
			targetPlatformList[i].TweenPos();
		}
	}

	void ButtonOffStatus()
	{
		isPlayInverse = true;
		isTirggerAnimation = false;
		selfAnimation.SetBool("isPressed",true);
		selfAnimation.SetBool("hasBox",false);

		for(int i = 0; i < targetPlatformList.Count; i++)
		{
			targetPlatformList[i].fromPos = targetPlatformList[i].gameObject.transform.localPosition;
			targetPlatformList[i].toPos = fromPosList[i];
			targetPlatformList[i].StartTime = Time.time;
			arrivalCountList[i] = false; 	//reset
		}
	}
}
