using UnityEngine;
using System.Collections;

public class TweenPostion3D : MonoBehaviour {

	public Vector3	fromPos;
	public Vector3	toPos;
	public float	speed;
	//public float	duration;
	public float	delay;
	public bool		isLoop;

	private float	distTotal = 0;
	private float	startTime = 0;

	// set when I want to start the tween, remeber to set from & to Pos first
	public float StartTime
	{
		set
		{
			distTotal = (fromPos-toPos).magnitude;
			//moveSpeed = distTotal / duration;
			startTime = delay;

			if(delay > 0) { startTime = delay + value; }
			else { startTime = value ; }
		}
		get { return startTime; }
	} 
	

	// Update is called once per frame
	void Update () {
	
	}

	public void TweenPos()
	{
		// Distance moved = time * speed.
		float distCovered = (Time.time - startTime) * speed;// moveSpeed;
		
		// Fraction of journey completed = current distance divided by total distance.
		float fracJourney = distCovered / distTotal;
		
		// Set our position as a fraction of the distance between the markers.
		this.transform.localPosition = Vector3.Lerp(fromPos,toPos, fracJourney);
		
	}
}
