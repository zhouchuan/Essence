using UnityEngine;
using System.Collections;
using Common;

public partial class ESLabScript : AbsBehaviour 
{
	// 1 - wink, player just wake up	
	private void InitWinkAnimation()
	{
		this.player.IsControllable = false;
		this.mouseLook.sensitivityX = 0;
		this.mouseLook.sensitivityY = 0;
		GameCommon.SetTween(this.winkSpriteUp.gameObject, TweenType.TweenPosition,true);
		GameCommon.SetTween(this.winkSpriteDown.gameObject, TweenType.TweenPosition,true);
		Invoke ("OnWinkFnished",timeWink);
	}


	// 2 - call when wink animation finished
	private void OnWinkFnished()
	{
		this.plotStatusDic["isFinishWink"] = true;
		PlaySeriesCaption(1,3,0f);
		float triggerTime = audioContainer.AudioLengthList [1] + audioContainer.AudioLengthList [2] 
			+ audioContainer.AudioLengthList [3];
		Invoke ("UnlockMouseX", triggerTime);
	}

	private void UnlockMouseX()
	{
		this.mouseLook.sensitivityX = 3;
	}

	private void UnlockMouseY()
	{
		this.mouseLook.sensitivityY = 3;
	}
	
	// 3 - check if player move mouse
	private void PlotMoveMouse()
	{
		if(!plotStatusDic["isFinishMouse"] )
		{
			//			Mode 1 - LookAt Target 
			//			RaycastHit hit;
			//			if (Physics.Raycast(this.player.transform.localPosition,this.player.transform.forward, out hit, 10.0F))
			//			{
			//				Debug.Log(hit.collider.gameObject.name);
			//				if(hit.collider.gameObject.name == "Monitor")
			//				{
			//					plotStatusDic["isFinishMouse"] = true;
			//					Debug.Log("000000000000000000000000000");
			//				}
			//			}
			// Mode 2 - just move camera
			if(Mathf.Abs(Input.GetAxis("Mouse X")) > 1.5f && this.mouseLook.sensitivityX != 0 && !plotStatusDic["isMoveMouseX"])
			{
				plotStatusDic["isMoveMouseX"] = true;
				Invoke("UnlockMouseY",audioContainer.AudioLengthList [4]);
				PlayPlot();
				return;
			}
			
			if(plotStatusDic["isMoveMouseX"] && this.mouseLook.sensitivityY != 0 && Mathf.Abs(Input.GetAxis("Mouse Y")) > 1f)
			{
				plotStatusDic["isMoveMouseY"] = true;
				plotStatusDic["isFinishMouse"] = true;
				this.player.IsControllable = true;
				PlaySeriesCaption(5,6,0);
			}
		}
	}
	
	// 4 - check player press all the buttons (and hold down for seconds)
	private void PlotMovePhysical()
	{
		if(!plotStatusDic["isFinishMove"] && plotStatusDic["isFinishMouse"])
		{
			MoveStatus inputStatus = GetMoveStatus();

			if(inputList.Count != 0)
			{
				if(inputList.Contains(inputStatus))
				{
					inputList.Remove(inputStatus);
				}
			}
			else
			{
				Invoke("PlayPlot",2);
				plotStatusDic["isFinishMove"] = true;
			}
		}
	}

	
	// Room1 - Move Test
	private void MovementTest()
	{
		if(this.player.CurrentHitObj == null)
			return;

		if(this.player.CurrentHitObj.gameObject.tag == "LightBlock")
		{
			this.player.CurrentHitObj.gameObject.renderer.material.mainTexture = lightBlockTexture;
		}
	}
	
	void OnMovementTestFinished()
	{
		PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,8);
	}

	void TriggerFalseLabStart()
	{
		this.labObjects.ShowLabStart (false);
	}

	void TriggerTrueLabTest1()
	{
		this.labObjects.ShowLabTest1 (true);
	}

	// Call when on platform - animation call
	void TriggerFlexibilityTestTips()
	{
		PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,9);
	}

	// Call when start flexibility test - animation call
	void TriggerFlexibilityTest()
	{
		if(!this.plotStatusDic["isFinishTest1"])
		{
			this.restartPos = this.player.GetPlayerPos ();
			this.plotStatusDic["isFinishTest1"] = true;
			this.timeLabel.gameObject.SetActive(true);
			PlotModule.Instance().PlayAudioByName(audioWater, "Lab-water");
		}
	}


	// Room2 - Jump & Climb Test
	private void FlexibilityTest()
	{
		if(!this.plotStatusDic["isFinishTest2"] && this.plotStatusDic["isFinishTest1"])
		{
			int leftSeconds = -1;

			if(!this.plotStatusDic["isTest2TimeUp"])
			{
				leftSeconds = cdTimer.CountDownTimer(test2Time);
				timeLabel.text = "Time left: "+leftSeconds;

				waterObj.transform.localPosition += new Vector3(0f,0.3f,0f) * Time.deltaTime;
				if(leftSeconds == countDonwnSecond)
				{
					// play count down every second 
					PlotModule.Instance().PlayAudioByName(audioCountDown, "CountDown-"+leftSeconds.ToString());
					countDonwnSecond --;
				}
			}

			if(leftSeconds == 0 || this.player.IsDeath )//&& ..touch the water
			{
				this.plotStatusDic["isTest2TimeUp"] = true;
				this.timeLabel.gameObject.SetActive(false);
				foreach(Light alertLight in alertLightList)
				{
					alertLight.gameObject.SetActive(true);
					alertLight.color = new Color (0.81f, 0, 0);
				}
				TriggerDeath();
			}

			if(leftSeconds == -1 || this.player.IsDeath)
			{
				//AlertLight
				foreach(Light alertLight in alertLightList)
				{
					alertLight.intensity = 2 * (Mathf.Sin (Time.time * 2)+2);
				}
			}
		}
	}

	void OnFlexibilityTestFinished()
	{
		if(!this.plotStatusDic ["isFinishTest2"])
		{
			this.plotStatusDic ["isFinishTest2"] = true;
			this.timeLabel.gameObject.SetActive(false);
			this.labObjects.ShowLabTest2(true);
			PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,10);
		}
	}
	
	//Room3 - Artificial Intelligence test
	private void AITest()
	{

	}

	void TriggerAITestTip()
	{
		PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,11);
		this.labObjects.ShowLabTest1 (false);
		this.audioWater.Pause ();
	}

	void TriggerAITestFinished()
	{
		if(!this.plotStatusDic["isFinishiTestAI"])
		{
			this.plotStatusDic["isFinishiTestAI"] = true;
			PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,12);
			this.labObjects.ShowLabTest3 (true);
		}
	}

	//Room4 - Order Obey Test
	void TriggerOderObeyTest()
	{
		PlotModule.Instance().SetCaptionByIndex(audioSource,captionLabel,PlotCaptionMap,13);
		this.labObjects.ShowLabTest2 (false);
	}

	void TriggerRightButton()
	{
		// Choose right button - play animation
		choiceAni.SetBool("isRotate", true);
	}

	void TriggerWrongButton()
	{
		// Choose wrong button - fall down
		triggerDropObj.SetActive(true);
		choiceAni.SetBool("isRotate", true);
	}

	void TriggerFninalTest()
	{
		PlaySeriesCaption(14,15,0);
	}

	void TriggerTrueLabTest4()
	{
		this.labObjects.ShowLabTest4 (true);
	}

	void TriggerTrueLabEnd2()
	{
		this.labObjects.ShowLabEnd2 (true);
	}

	void TriggerFalseLabTest3()
	{
		this.labObjects.ShowLabTest3 (false);
	}

	void TriggerFalseLabTest4()
	{
		this.labObjects.ShowLabTest4 (false);
	}

	void TriggerTrueEnd1()
	{
		this.labObjects.ShowLabEnd1 (true);
	}

	// Trigger drop - test03
	void OnTriggerDrop()
	{
		PlotModule.Instance().PlayAudioByName(audioCountDown, "Lab-FailAlert");
		Invoke ("TriggerDestroy",1f);
	}

	void TriggerDestroy()
	{
		Destroy(rotationPlatObj);
	}


	// Trigger go to sofa room up
	void TriggerFinalRoom1()
	{
		//trigger light
		watchPos = watchPos1;
		watchRot = watchRot1;
		spotLightUp.gameObject.SetActive(true);
		PlayerModule.Instance ().IsTestPassed = 1;
	}

	// Trigger go to sofa room down
	void TriggerFinalRoom2()
	{
		//trigger light
		watchPos = watchPos2;
		watchRot = watchRot2;
		spotLightDown.gameObject.SetActive(true);
		PlayerModule.Instance ().IsTestPassed = 2;
	}


	private void PlotEvaluate()
	{
		if(this.player.CurrentSwitchObj != null)
		{
			if(this.player.CurrentSwitchObj.tag == "Sofa")
			{
				//trigger animation
				//play plot
				this.player.IsControllable = false;
				this.plotStatusDic["isGetSofa"] = true;
				this.player.gameObject.GetComponent<MouseLook>().enabled = false ;
			}
		}

		if(plotStatusDic["isGetSofa"]&& !plotStatusDic["isEvaluating"])
		{
			this.player.CurrentSwitchObj = null;

			Vector3 currentPos = this.player.GetPlayerPos();
			float wantedX = Mathf.Lerp(currentPos.x, watchPos.x, Time.deltaTime);
			float wantedY = Mathf.Lerp(currentPos.y, watchPos.y, Time.deltaTime);
			float wantedZ = Mathf.Lerp(currentPos.z, watchPos.z, Time.deltaTime);
			this.player.transform.position = new Vector3(wantedX, wantedY, wantedZ);

			Quaternion lookAtRot = Quaternion.LookRotation(watchRot);
			this.player.transform.rotation = Quaternion.Slerp(this.player.transform.rotation,lookAtRot,Time.deltaTime);
	
			if((this.player.transform.rotation.eulerAngles - watchDir).magnitude < 1)
			{
				this.player.gameObject.GetComponent<MouseLook>().enabled = true ;
				this.mouseLook.axes = MouseLook.RotationAxes.MouseX;
				plotStatusDic["isEvaluating"] = true;
				if(watchPos.y > 0)
				{
					PlotModule.Instance ().PlayAudioByName (audioSource, "Lab-endReject");
					screenReject.renderer.material.mainTexture = movieReject;
					movieReject.Play();
				}
				else
				{
					PlotModule.Instance ().PlayAudioByName (audioSource, "Lab-endPass");
					screenPass.renderer.material.mainTexture = moviePass;
					moviePass.Play();
				}
				return;
			}
		}

		//pass test
		if(plotStatusDic["isEvaluating"])
		{
			if(PlayerModule.Instance().IsTestPassed == 1 && !this.player.IsPloting)
			{
				this.player.IsControllable = false;
				this.mouseLook.enabled = false;
				PlotModule.Instance().PlayAudioByName(audioCountDown, "Lab-FailAlert");
				destroyPanel.SetActive(true);
			}

			if(PlayerModule.Instance().IsTestPassed == 2 && !this.player.IsPloting)
			{
				Application.LoadLevel(MainBehaviour.infinityScene);
			}
		}
	}


	private void CDTimerDoor()
	{
		if (this.player.CurrentSwitchObj == null) 
		{
			return;
		}
		if (this.player.CurrentSwitchObj.tag == "DoorTrigger")
		{
			int leftSeconds = cdTimer.CountDownTimer(GameCommon.DoorCDTime);
			this.timeLabel.gameObject.SetActive(true);
			this.timeLabel.text = "Time left: "+leftSeconds;

			if(leftSeconds == -1)
			{
				this.timeLabel.gameObject.SetActive(false);
				this.player.CurrentSwitchObj = null;
			}
		}

	}

	void TriggerDestroyPlayer()
	{
		destroyPanel.SetActive (true);
		this.player.IsControllable = false;
	}

	void TriggerDeath()
	{
		this.restartPanel.SetActive (true);
		this.player.IsControllable = false;
		this.mouseLook.enabled = false;
		PlotModule.Instance().PlayAudioByName(audioCountDown, "Lab-FailAlert");

		if(this.player.GetPlayerPos().y < waterLevel)
		{
			PlotModule.Instance().PlayAudioByName(audioWaterDrop, "Lab-dropWater1");
		}
	}

	void RestartGame()
	{
		this.restartPanel.SetActive (false);
		this.restartPos.y += 2;

		this.player.ResetPlayerPos (restartPos);
		this.player.IsControllable = true;
		this.player.IsDeath = false;
		this.mouseLook.enabled = true;

		if(!this.plotStatusDic["isFinishTest2"])
		{
			this.plotStatusDic["isTest2TimeUp"] = false;
			this.cdTimer.ResetTimer ();
			this.waterObj.transform.localPosition = waterPos;
			this.timeLabel.gameObject.SetActive(true);

			foreach(Light alertLight in alertLightList)
			{
				alertLight.color = new Color (0.698f, 0.886f, 1f);
				alertLight.intensity = 2;
				alertLight.gameObject.SetActive(false);
			}
		}
	}
	
	void KeybroadListener()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			destroyPanel.SetActive (false);
			restartPanel.SetActive (false);
		}
	}

	void QuiteGame()
	{
		Application.LoadLevel(MainBehaviour.loginScence);
	}
}