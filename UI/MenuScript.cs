using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
	public GameObject menuPanel;
	public GameObject spriteSofa;
	public GameObject menuButton;
	public UISprite leftArrow;
	public UISprite rightArrow;
	public TweenPosition tweenButton;


	public void ShowMenuPanel(bool isShow)
	{
		this.menuPanel.SetActive(isShow);
	}

	public void ShowSpriteSofa(bool isShow)
	{
		this.spriteSofa.SetActive(isShow);
	}

	public void ShowButtons()
	{
		this.menuButton.SetActive(true);
	}

	public void DisableButtons()
	{
		this.menuButton.SetActive(false);
	}

	public void ShowSecondPlayUI(bool isShow)
	{
		this.leftArrow.gameObject.SetActive(isShow);
		this.rightArrow.gameObject.SetActive(isShow);
	}

	public void SetTweenPositionTo(Vector3 toPos)
	{
		this.tweenButton.from = GetTweenObjPos();
		this.tweenButton.to = GetTweenObjPos() + toPos;
		this.tweenButton.enabled = true;
		this.tweenButton.Reset();
		this.leftArrow.GetComponent<BoxCollider>().enabled = false;
		this.rightArrow.GetComponent<BoxCollider>().enabled = false;
		this.leftArrow.color = Color.white;
		this.rightArrow.color = Color.white;
		Invoke("EnableArrowCollider",this.tweenButton.duration);
	}

	public void DisableArrowCollider(bool isLeft)
	{
		this.leftArrow.GetComponent<BoxCollider>().enabled = isLeft;
		this.rightArrow.GetComponent<BoxCollider>().enabled = !isLeft;
	}

	public void EnableArrowCollider()
	{
		if (GetTweenObjPos().x > -200 && GetTweenObjPos().x < 0 )
		{
			this.rightArrow.GetComponent<BoxCollider>().enabled = true;
			this.leftArrow.GetComponent<BoxCollider>().enabled = true;
		}
		else if(GetTweenObjPos().x == -200)
		{
			this.rightArrow.GetComponent<BoxCollider>().enabled = true;
		}
		else if(GetTweenObjPos().x == 0)
		{
			this.leftArrow.GetComponent<BoxCollider>().enabled = true;
		}
	}

	public Vector3 GetTweenObjPos()
	{
		return this.tweenButton.gameObject.transform.localPosition;
	}

}

