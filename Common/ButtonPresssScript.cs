using UnityEngine;
using System.Collections;

public class ButtonPresssScript : MonoBehaviour {

	private TweenScale scaleEffect;

	void Awake()
	{
		this.scaleEffect = this.GetComponent<TweenScale>();
		this.scaleEffect.enabled = false;
	}

	void OnPress(bool isPress)
	{
		if(isPress)
		{
			this.scaleEffect.enabled = true;
		}
		else
		{
			this.gameObject.transform.localScale = Vector3.one;
			this.scaleEffect.enabled = false;
			this.scaleEffect.Reset();
		}
	}
}
