using UnityEngine;
using System.Collections;

public class UICaption : MonoBehaviour {

	public UILabel captions;

	public void ShowCaption(bool isShow)
	{
		captions.gameObject.SetActive(isShow);
	}

	public void SetCaptionText(string capationText)
	{
		captions.text = capationText;
	}
}
