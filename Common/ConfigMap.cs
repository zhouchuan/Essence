using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class ConfigMap {

	public Dictionary<string,string> captionFiles; //<roomName, fileName>
	private static ConfigMap instance = null;
	public static ConfigMap Instance()
	{
		if(instance == null)
		{
			instance = new ConfigMap();
		}
		return instance;
	}

	public List<string> SoloCaptionMap { set; get; }
	public List<string> GuideCaptionMap { set; get; }
	public List<string> ForestCaptionMap { set; get; }
	public List<string> CaveCaptionMap { set; get; }
	public List<string> TownCaptionMap { set; get; }
	public List<string> AlleyCaptionMap { set; get; }
	public List<string> LabCaptionMap { set; get; }
	public List<string> InfinityCaptionMap { set; get; }
	public List<string> SpecialCaptionMap { set; get; }

	private ConfigMap()
	{
		this.SoloCaptionMap = GameCommon.ReadCaptionFromFile("SoloPlot");
		this.GuideCaptionMap = GameCommon.ReadCaptionFromFile("GuidePlot");
		this.ForestCaptionMap = GameCommon.ReadCaptionFromFile("ForestPlot");
		this.CaveCaptionMap = GameCommon.ReadCaptionFromFile("CavePlot");
		this.TownCaptionMap = GameCommon.ReadCaptionFromFile("TownPlot");
		this.AlleyCaptionMap = GameCommon.ReadCaptionFromFile("AlleyPlot");
		this.LabCaptionMap = GameCommon.ReadCaptionFromFile("LabPlot");
		this.InfinityCaptionMap = GameCommon.ReadCaptionFromFile("InfinityPlot");
		this.SpecialCaptionMap = GameCommon.ReadCaptionFromFile ("Special");
	}

}
