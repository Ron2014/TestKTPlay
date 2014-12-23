using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalaxyCurves : ScriptableObject
{
	[System.Serializable]
	public class GalaxyCurve 
	{
		public string name;
		public AnimationCurve curve;
	}

	public List<GalaxyCurve> curves;
}
