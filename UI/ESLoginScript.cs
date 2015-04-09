using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public partial class ESLoginScript : AbsBehaviour
{
	public UICaption		loginCaption;
	public MenuScript		menuPanel;
	public LogoScript		logoPanel;
	public SelectScript		selectPanel;
	public CreditScript		creditPanel;

	public List<float>		soloTrigger;
	
	private static int		speakerIndex = 0;
	private static int		soloIndex = 1;
	private static int		uibmIndex = 2;
	
	private int				captionIndex = 0;

	private float			logoTime = 4.0f;
	private float			checkTime = 3.0f;
	private float			menuTime;

	protected override void OnLoad()
	{
		this.PlotCaptionMap = ConfigMap.Instance().SoloCaptionMap;
		this.selectPanel.ShowSelectPanel(false);
		PlotModule.Instance().AudioContainer = audioContainer;
		InitLoginUI();
	}

	public void InitLoginUI()
	{
		//this.menuPanel.Invoke("DisableButtons",0);
		//load plot caption if game first play
		if(PlayerModule.Instance().IsChooseLevel)
		{
			OnChooseLevelListener();
			return;
		}

		if(PlayerModule.Instance().IsFirstPlay)
		{
			this.logoPanel.ShowLogoPanel(true);
			this.logoPanel.ShowStudio(false);
			this.logoPanel.ShowLeftSpeaker(false);
			this.logoPanel.ShowRightSpeaker(false);
			this.logoPanel.ShowCaptions(false);
			this.menuPanel.ShowMenuPanel(false);
			this.menuPanel.DisableButtons();
			this.menuPanel.ShowSecondPlayUI(false);
			
			Invoke("LeftSpeakerCheck", logoTime);
			Invoke("RightSpeakerCheck", checkTime + logoTime);
			Invoke("PlayLoginPlot", checkTime * 2 + logoTime);
			menuTime = checkTime * 2 + logoTime + soloTrigger[PlotCaptionMap.Count - 1]+5.0f;
		}
		else
		{
			this.logoPanel.ShowStudio(true);
			this.logoPanel.ShowLogoPanel(false);
			menuTime = 0;//logoTime;
		}
		
		Invoke ("LoadMenu", menuTime);
	}

	protected override void OnUpdate ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(PlayerModule.Instance().IsFirstPlay && logoPanel.gameObject.activeSelf)
			{
				CancelInvoke();
				this.audioSource.Stop();
				this.logoPanel.ShowLogoPanel(false);
				this.menuPanel.ShowMenuPanel(true);
				this.menuPanel.ShowButtons();
				PlotModule.Instance().PlayAudioByIndex(audioSource,uibmIndex);
			}

			if(selectPanel.gameObject.activeSelf)
			{
				menuPanel.ShowMenuPanel(true);
				selectPanel.ShowSelectPanel(false);
			}
			creditPanel.HideCredit();
		}
	}

	private void LeftSpeakerCheck()
	{
		PlotModule.Instance().PlayAudioByIndex(audioSource,speakerIndex);
		this.logoPanel.ShowLeftSpeaker(true);
	}

	private void RightSpeakerCheck()
	{
		this.logoPanel.ShowRightSpeaker(true);
	}

	private void PlayLoginPlot()
	{
		this.loginCaption.SetCaptionText("");
		this.logoPanel.ShowCaptions(true);
		PlotModule.Instance().PlayAudioByIndex(audioSource,soloIndex);

		for(int i = 0; i < this.PlotCaptionMap.Count; i++)
		{
			Invoke ("PlaySolo",soloTrigger[i]);
		}

		Invoke("ShowStudioLogo",1);
	}

	private void ShowStudioLogo()
	{
		this.logoPanel.ShowStudio(true);
	}

	private void PlaySolo()
	{
		this.loginCaption.SetCaptionText(this.PlotCaptionMap[captionIndex++]);
		//PlotModule.Instance().SetCaptionByIndex(audioSource, loginCaption.captions, ConfigMap.Instance().SoloCaptionMap);
	}

	private void LoadMenu()
	{
		this.logoPanel.ShowCaptions(false);
		this.menuPanel.ShowMenuPanel(true);
		if(!PlayerModule.Instance().IsFirstPlay)
		{
			this.menuPanel.ShowSecondPlayUI(true);
		}
		PlotModule.Instance().PlayAudioByIndex(audioSource,uibmIndex);
		this.menuPanel.Invoke("ShowButtons",3.0f);
	}
	

}

