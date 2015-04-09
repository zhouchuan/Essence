using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {

	public GameObject credit;
	public MovieTexture texture;

	void Start()
	{
		credit.GetComponent<UITexture>().mainTexture = texture;
	}

	public void ShowCredit()
	{
		this.gameObject.SetActive(true);
		this.texture.Play();
		this.texture.loop = true;
	}

	public void HideCredit()
	{
		this.gameObject.SetActive(false);
		this.texture.Stop();
		this.texture.loop = true;
	}
}
