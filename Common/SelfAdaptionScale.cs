using UnityEngine;
using System.Collections;

public class SelfAdaptionScale : MonoBehaviour {

	private float scale = 1.0f;  
	private UIRoot root;
	void Awake()
	{
		root = GetComponent<UIRoot>();
		float width = Screen.width * scale;
		float height =  Screen.height * scale;
		float selfAdaptionScale = 1.0f;
		if((float)Screen.width/Screen.height < 4/3)
		{
			selfAdaptionScale = 800.0f / width;
		}
		else
		{
			selfAdaptionScale = 600.0f / height;
		}
		root.manualHeight = (int) (height * selfAdaptionScale);
	}
}
