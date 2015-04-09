using UnityEngine;
using System.Collections;

public class ESNPC : MonoBehaviour 
{

	public enum MoveDirection
	{
		Vertical,
		Horiziontal,
		Line
	}
	public MoveDirection moveDir;
	public AudioSource audioSource;
	private Vector3 oriPos;
	private Vector3 toPos;
	private Vector3 toWards;
//	private bool isInverse = false;
	private int moveLegth;
	private float speed = 0.07f;

	// Use this for initialization
	void Start () 
	{
		oriPos = transform.position;
		if(Random.Range(-1,1)> 0 )
		{
			moveLegth = (int)Random.Range(10,16);
		}
		else
		{
			moveLegth = -(int)Random.Range(10,16);
		}

		switch(moveDir)
		{
		case MoveDirection.Vertical:
			toPos = new Vector3(moveLegth + oriPos.x, oriPos.y, oriPos.z);
			break;
		case MoveDirection.Horiziontal:
			toPos = new Vector3(oriPos.x, oriPos.y, moveLegth + oriPos.z);
			break;
		case MoveDirection.Line:
			toPos = new Vector3(moveLegth + oriPos.x, oriPos.y, moveLegth + oriPos.z);
			break;
		}

		toWards = (toPos - oriPos).normalized * speed ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += toWards;

		if( IsGotPos(oriPos) )
		{
//			isInverse = false;
			toWards = (toPos - oriPos).normalized * speed;
		}
		if(IsGotPos(toPos))
		{
//			isInverse = true;
			toWards = -(toPos - oriPos).normalized * speed;
		}

	}

	private bool IsGotPos(Vector3 targetPos)
	{
		return (targetPos - transform.position).magnitude < 0.1;
	}

	public void Speak()
	{
		if(!audioSource.isPlaying)
		{
			audioSource.Play();
		}
	}
	 
}
