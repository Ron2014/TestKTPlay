using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GCurve : MonoBehaviour 
{	
	public enum CurveType
	{
		DEFAULT_LINEAR,
		CONSTANT_ONE,
		SPEED_UP,
		SPEED_DOWN,
		POPUP_PANEL,
		SCALE_DOWN_PANEL,
		SCALE_DOWN_ALPHA,
		HERO_LABEL_SCALE,
		PULL_DOWN_PANEL,
		FLYING_COIN,
		FLYING_COIN_2,
		FAST_FADE_IN,
		S_CURVE_RISE,
		BREATH,
		SPEED_DOWN_FALL,
		SPEED_UP_FALL,
		SCREENSHAKE,
		INVALID,
	}

	public static bool isInitialized = false;
	public GalaxyCurves curveAsset;

	static Dictionary<string, AnimationCurve> curves = new Dictionary<string, AnimationCurve>();
	static AnimationCurve linear;

	void loadData()
	{
		curves.Clear();

		List<GalaxyCurves.GalaxyCurve> curveList = new List<GalaxyCurves.GalaxyCurve>();
		curveList = curveAsset.curves;

		foreach(GalaxyCurves.GalaxyCurve galaxyCurve in curveList)
		{
			curves.Add(galaxyCurve.name, galaxyCurve.curve);
		}
	}

	public static AnimationCurve GetCurve(string name)
	{
		AnimationCurve curve = linear;

		if(curves.ContainsKey(name))
			curve = curves[name];

		return curve;
	}

	void Awake()
	{
		isInitialized = true;

		loadData();
		linear = defaultLinearCurve();
	}

	static AnimationCurve defaultLinearCurve()
	{
		Keyframe[] keyFrames;
		
		keyFrames = new Keyframe[2];
		keyFrames[0] = new Keyframe(0, 0);
		keyFrames[1] = new Keyframe(1, 1);
		
		return new AnimationCurve(keyFrames);
	}
	public static AnimationCurve GetCurve(CurveType curveType)
	{
		if(!isInitialized)
			return defaultLinearCurve();
		
		AnimationCurve curve = linear;
		
		switch(curveType)
		{
		case CurveType.DEFAULT_LINEAR:
			curve = GetCurve("Linear");
			break;
			
		case CurveType.CONSTANT_ONE:
			curve = GetCurve("Constant One");
			break;
			
		case CurveType.SPEED_UP:
			curve = GetCurve("Speed Up");
			break;
			
		case CurveType.SPEED_DOWN:
			curve = GetCurve("Speed Down");
			break;
			
		case CurveType.POPUP_PANEL:
			curve = GetCurve("Popup");
			break;
			
		case CurveType.SCALE_DOWN_PANEL:
			curve = GetCurve("Scale Down");
			break;
			
		case CurveType.SCALE_DOWN_ALPHA:
			curve = GetCurve("SCurve Fall");
			break;
			
		case CurveType.HERO_LABEL_SCALE:
			curve = GetCurve("Hero Label Scale");
			break;
			
		case CurveType.PULL_DOWN_PANEL:
			curve = GetCurve("Panel Pull Down");
			break;
			
		case CurveType.FLYING_COIN:
			curve = GetCurve("Fly Coin");
			break;
			
		case CurveType.FLYING_COIN_2:
			curve = GetCurve("Fly Coin 2");
			break;
			
		case CurveType.S_CURVE_RISE:
			curve = GetCurve("SCurve");
			break;
			
		case CurveType.BREATH:
			curve = GetCurve("Breath");
			break;
			
		case CurveType.SPEED_DOWN_FALL:
			curve = GetCurve("Speed Down Fall");
			break;
		
		case CurveType.SCREENSHAKE:
			curve = GetCurve("ScreenShake");
			break;

		case CurveType.SPEED_UP_FALL:
			curve = GetCurve("Speed Up Fall");
			break;
			
		default:
			Debug.LogError("shouldn't reach here.");
			break;
		}
		
		return curve;
	}
}
