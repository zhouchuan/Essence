using UnityEngine;
using System.Collections;

public class TriggerPlotScript : MonoBehaviour {
	
//	public string		plotBoolName;		// The bool variable I want to change.
	public ESLabScript	responseScript;		// The script which will response to my change.
	public string		functionName;		// The function I need call;
	public float		delayTime;
	public bool			isDestroy = false;
//	public string		targetTag;			// Tag name of the collision(interaction) object
//	public Animator		targetAnimation;	// Animator attached to the Controlled object by trigger.
//	public string		targetStateName;	// StateName(targetAnimation) which I want to play.
//	private bool		isTirggerAnimation = false;

	// send message
	void OnTriggerPlot() 
	{
		if (functionName != "") 
		{
			responseScript.Invoke(functionName, delayTime );
		}
	}

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Player")
		{
			OnTriggerPlot();
			if(isDestroy)
			{
				Destroy(this);
			}
		}
	}
}
