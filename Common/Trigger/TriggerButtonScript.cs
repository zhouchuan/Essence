using UnityEngine;
using System.Collections;

public class TriggerButtonScript : MonoBehaviour
{
	//public string		targetTag;			// Tag name of the collision(interaction) object
	public Animator		targetAnimation;	// Animator attached to the Controlled object by trigger.

	// call when press the button.
	void ButtonOnListener()
	{
		targetAnimation.SetBool ("isButtonPressed",true);
	}

	void ButtonOffListener()
	{
		targetAnimation.SetBool ("isButtonPressed",false);
	}
}
