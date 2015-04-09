using UnityEngine;
using System.Collections;

public class ButtonHoverScript : MonoBehaviour
{
	//public bool		isImageButton;
	public string		hoverSpriteName;
	private string		normalSprite;
	private UISprite	icon;
	void Awake()
	{
		this.icon = this.gameObject.GetComponent<UISprite>();
		this.normalSprite = this.icon.spriteName;
	}

	void OnHover(bool isHover)
	{
		if(isHover)
		{
			this.icon.spriteName = this.normalSprite + this.hoverSpriteName;
		}
		else
		{
			this.icon.spriteName = this.normalSprite;
		}
	}

}
