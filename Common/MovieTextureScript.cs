using UnityEngine;
using System.Collections;

public class MovieTextureScript : MonoBehaviour
{
	public MovieTexture movieTexture;

	void Start () {
		renderer.material.mainTexture = movieTexture;
		movieTexture.loop = true;
		movieTexture.Play();
	}
	
}
