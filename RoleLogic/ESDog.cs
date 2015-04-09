using UnityEngine;
using System.Collections;

public class ESDog : MonoBehaviour {

	public Vector3		oriPos;
	public Transform	target;
	public float		dogSpeed;
	public float		judgeRange;
	public bool			isWolf;
	public AudioSource	audioBark;

	public bool	IsFollow{set; get;}

	// Update is called once per frame
	void Update () 
	{
		if(!PlayerInRange() && IsFollow)
		{
			Follow();
		}
	}

	void Follow()
	{
		Vector3 wantedPos = new Vector3();
		Vector3 currentPos = transform.position;
		wantedPos.x = Mathf.Lerp(currentPos.x, target.position.x, Time.deltaTime);
		wantedPos.y = Mathf.Lerp(currentPos.y, target.position.y, Time.deltaTime);
		wantedPos.z = Mathf.Lerp(currentPos.z, target.position.z, Time.deltaTime);
		transform.position = wantedPos;
	}

	public bool PlayerInRange()
	{
		return ((transform.position - target.position).magnitude < judgeRange);
	}

	public bool PlayerInRange(float range)
	{
		return ((transform.position - target.position).magnitude < range);
	}

	public void SetDogPosition(Vector3 newPos)
	{
		transform.position = newPos;
	}

	public void PlayAnimalSound(bool isLoop)
	{
		audioBark.loop = isLoop;
		if(!audioBark.isPlaying)
		{
			audioBark.Play();
		}
	}

	public void ResetDog(Vector3 originalPos)
	{
		transform.position = originalPos;
	}

	public void StopAnimalSound()
	{
		audioBark.Stop();
	}

}
