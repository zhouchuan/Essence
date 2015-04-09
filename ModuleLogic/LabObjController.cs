using UnityEngine;
using System.Collections;

public class LabObjController : MonoBehaviour {

	public GameObject labStart;
	public GameObject labTest1;
	public GameObject labTest2;
	public GameObject labTest3;
	public GameObject labTest4;
	public GameObject labEnd1;
	public GameObject labEnd2;
	public GameObject moveableBox;

	public void ShowLabStart(bool isShow)
	{
		this.labStart.SetActive (isShow);
	}

	public void ShowLabTest1(bool isShow)
	{
		this.labTest1.SetActive (isShow);
	}

	public void ShowLabTest2(bool isShow)
	{
		this.labTest2.SetActive (isShow);
		this.moveableBox.SetActive (isShow);
	}

	public void ShowLabTest3(bool isShow)
	{
		this.labTest3.SetActive (isShow);
	}

	public void ShowLabTest4(bool isShow)
	{
		this.labTest4.SetActive (isShow);
	}

	public void ShowLabEnd1(bool isShow)
	{
		this.labEnd1.SetActive (isShow);
	}

	public void ShowLabEnd2(bool isShow)
	{
		this.labEnd2.SetActive (isShow);
	}

}
