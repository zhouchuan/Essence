using UnityEngine;
using System.Collections;

public class ESTipsScript : MonoBehaviour
{
	public GameObject forwardTip;
	public GameObject leftTip;
	public GameObject rightTip;

	public void ShowForwardTip(bool isShow)
	{
		this.forwardTip.SetActive (isShow);
	}

	public void ShowLeftTip(bool isShow)
	{
		this.leftTip.SetActive (isShow);
	}

	public void ShowRightTip(bool isShow)
	{
		this.rightTip.SetActive (isShow);
	}

}
