using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour {

	public GameObject	logoPanel;
	public GameObject	logo;
	public GameObject	leftSpeaker;
	public GameObject	rightSpeaker;
	public GameObject	studioName;
	public UILabel		captionLabel;

	public void ShowStudio(bool isShow)
	{
		this.studioName.SetActive(isShow);
	}

	public void ShowLogoPanel(bool isShow)
	{
		this.logoPanel.SetActive(isShow);
	}

	public void ShowGameLogo(bool isShow)
	{
		this.logo.SetActive(isShow);
		//logoTweenAlpha.enabled = isShow;
	}

	public void ShowLeftSpeaker(bool isShow)
	{
		this.leftSpeaker.SetActive(isShow);
	}

	public void ShowRightSpeaker(bool isShow)
	{
		this.rightSpeaker.SetActive(isShow);
	}

	public void SetCaptions(string captionText)
	{
		this.captionLabel.text = captionText;
	}

	public void ShowCaptions(bool isShow)
	{
		this.captionLabel.gameObject.SetActive(isShow);
	}
}
