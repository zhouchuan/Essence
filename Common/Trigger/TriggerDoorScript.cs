// Attach this script to the button object
using UnityEngine;
using System.Collections;
using Common;

public class TriggerDoorScript : MonoBehaviour {

	public string	triggerTag;			// Tag of the collision object
	public Animator doorAnimator;		// Animator which attached to the door object.
	public Animator buttonAnimator;		// Animator which attached to the button object.
	private bool	isDoorOpen = false;
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == triggerTag) 
		{
			SetOpenStatus();
		}
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.tag == triggerTag) 
		{
			SetCloseStatus();
		}
	}

	/* Use these functions by SendMessage */
	void SetOpenStatus()
	{
		if(!this.isDoorOpen)
		{
			doorAnimator.SetBool ("hasPlayer", true);
			doorAnimator.SetBool ("isDoorOpen", false);
			this.isDoorOpen = true;
		}
	}

	void SetStayStatus()
	{
		Debug.Log ("stay");
		doorAnimator.SetBool ("isDoorOpen",true);
		doorAnimator.SetBool ("hasPlayer",true);
	}

	void SetCloseStatus()
	{
		Debug.Log ("Closed");
		doorAnimator.SetBool ("isDoorOpen",true);
		doorAnimator.SetBool ("hasPlayer",false);
	}

	void SetNoneStatus()
	{ 
		this.doorAnimator.SetBool ("isDoorOpen",false);
		this.doorAnimator.SetBool ("hasPlayer",false);
		this.isDoorOpen = false;
	}

	void InvokeCloseDoor()
	{
		Invoke ("SetCloseStatus", GameCommon.DoorCDTime);
	}

//	void Update()
//	{
//		Debug.Log (isDoorOpen);
//	}
}
