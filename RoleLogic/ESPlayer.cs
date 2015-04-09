using UnityEngine;
using System.Collections;
using Common;

public class ESPlayer : MonoBehaviour
{
	
	public CharacterController playerControllor;
	
	public float 		speedWalk = 0.3f;
	public float 		speedJump = 0.3f;
	public float 		speedRunUp = 0.2f;
	public float 		gravity	= 1.5f;
	
	public MoveStatus	moveStatus;
	public AudioSource	audio_walk ;
	public AudioSource	audio_bump ;
	public AudioSource	audio_heart ;
	
	private float		rotateAngle	= 90.0f;
	private float		checkRadius = 3.0f;
	
	private Vector3 	playerDirection;
	private Vector3 	playerRotation;
	private bool		isHit = false;
	private bool		isOnSlope = false;
	private bool		isOnElevator = false;
	private bool		isCarry = false;
	private GameObject 	elevatorObj;
	
	public bool			isVisible = false;
	public bool			IsPloting { set; get; }
	public bool			IsDeath { set; get; } 
	public bool			IsTriggerBlockWall { set; get; }
	public bool			IsControllable { set; get; }
	public GameObject	CurrentHitObj { set; get; }
	public GameObject	CurrentSwitchObj { set; get; }
	public bool			IsRestrictPath { set; get; }
	
	// Use this for initialization
	void Awake () 
	{
		IsDeath = false;
		IsRestrictPath = false;
		IsPloting = false ;
		IsTriggerBlockWall = false;
		IsControllable = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(IsControllable && !IsPloting)
		{
			OnKeyControll();
			OnPlayerMove();
			PlayerSoundEffect();
		}

		if(IsPloting)
		{
			audio_walk.Pause();
		}
	}

	private void OnKeyControll()
	{
		if(isOnSlope && Input.GetButton("Jump"))
		{
			OnJump();
		}

		if(Input.GetKeyUp(KeyCode.E))
		{
			Interation();
		}

		// free path
		if(!IsRestrictPath && !isVisible)
		{
			if(GetRightDown())
			{
				transform.Rotate(Vector3.up, rotateAngle);
			}
			else if(GetLeftDown())
			{
				transform.Rotate(Vector3.down, rotateAngle);
			}
		}
	}
	
	private void OnPlayerMove()
	{
		if(playerControllor.isGrounded)
		{
			playerDirection = new Vector3(0,0,Input.GetAxis("Vertical")) * speedWalk * Time.deltaTime;
			
			if(isVisible)
			{
				playerDirection +=  new Vector3(Input.GetAxis("Horizontal"),0,0) * speedWalk * Time.deltaTime;
			}
			else
			{
				OnRotateHead();
			}
			
			//change vetror from local to world.
			playerDirection = transform.TransformDirection(playerDirection);
			playerDirection.y = 0;
			 
			if(Input.GetButton("Jump") && isVisible)
			{
				OnJump();
			}
		}
		else
		{
			isOnElevator = false;
		}

		// gravity
		playerDirection.y -= gravity * Time.deltaTime;
		playerControllor.Move(playerDirection);
	}

	
	private void OnJump()
	{
		isOnElevator = false;
		
		if(Input.GetAxis("Vertical")>0.2)
		{
			playerDirection.y = speedJump+speedRunUp;
		}
		else
		{
			playerDirection.y = speedJump;
		}
	}
	
	private void OnRotateHead()
	{
		// rotate the head to locate player position
		if(Input.GetKeyDown(KeyCode.R))
		{
			transform.Rotate(Vector3.up,45.0f);
		}
		if(Input.GetKeyUp(KeyCode.R))
		{
			transform.Rotate(Vector3.down,45.0f);
		}
	}
	
	private void PlayerSoundEffect()
	{
		/****Player Audio****/
		if(Input.GetAxis("Vertical") != 0)
		{
			if(!audio_walk.isPlaying)
			{
				audio_walk.Play();
			}
		}
		else
		{
			audio_walk.Pause();
		}
		//		if(!audio_heart.isPlaying)
		//		{
		//			audio_heart.Play();
		//		}
	}
	
	private void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		CurrentHitObj = hit.gameObject;
		if(hit.gameObject.tag == "Block")
		{
			if(!audio_bump.isPlaying && !isHit)
			{
				audio_bump.Play();
				isHit = true;
			}
			return;
		}
		else
		{
			isHit = false;
		}
		
		if(hit.gameObject.tag == "WallBlock")
		{
			this.IsTriggerBlockWall = true;
		}
		
	}
	
	void OnTriggerEnter(Collider hit)
	{
		switch(hit.gameObject.tag)
		{
		case "MovePlatfrom":
			transform.parent = hit.gameObject.transform;
			elevatorObj = hit.gameObject;
			break;
		case "DeathTrigger":
			IsDeath = true;
			//SendMessage("TriggerDeath");
			break;
		case "SlopeTrigger":
			isOnSlope = true;
			break;
		}
	}
	
	
	void OnTriggerExit(Collider hit)
	{
		switch(hit.gameObject.tag)
		{
		case "MovePlatfrom":
			transform.parent = null;
			elevatorObj = null;
			break;
		case "SlopeTrigger":
			isOnSlope = false;
			break;
		}
	}

	// press E to interact with objects
	public void Interation()
	{
		Collider[] ObjInRange = Physics.OverlapSphere(transform.position, checkRadius);
		
		foreach(Collider obj in ObjInRange)
		{
			if(obj.tag == "Untagged" )
			{
				continue;
			}
			
			switch(obj.tag)
			{
			case "MoveableBox":
				if(!isCarry)
				{
					//pick up box
					isCarry = true;
					obj.GetComponent<MoveableBoxScript>().IsCarrying = isCarry;
					obj.rigidbody.isKinematic = true;
				}
				else
				{
					isCarry = false;
					obj.GetComponent<MoveableBoxScript>().IsCarrying = isCarry;
					obj.rigidbody.isKinematic = false;
				}
				break;
				
			case "DoorTrigger":
				obj.transform.parent.SendMessage("SetOpxenStatus");
				obj.transform.parent.SendMessage("InvokeCloseDoor");
				this.CurrentSwitchObj = obj.gameObject;
				break;

			case "RotationTrigger":
				obj.transform.SendMessage("OnTriggerPlot");
				break;
				
			case "ElevatorTrigger":
				obj.transform.parent.SendMessage("ButtonOnListener");
				break;
				
			case "Sofa":
				this.CurrentSwitchObj = obj.gameObject;
				break;
				
			case "HappyBirthday":
				obj.SendMessage("ShowInputBox");
				break;
			}
		}
	}
	
	
	public void ResetPlayerPos(Vector3 pos)
	{
		transform.position = pos;
	}
	
	public Vector3 GetPlayerPos()
	{
		return transform.position;
	}
	
	public bool GetLeftDown()
	{
		return (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ;
	}
	
	public bool GetRightDown()
	{
		return (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ;
	}
	
}