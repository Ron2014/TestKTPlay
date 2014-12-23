using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIRoot))]
public class UIRootAdjustor : MonoBehaviour {
    float standardWidth = 640f;
    float standardHeight = 960;

	// Use this for initialization
	void Awake() {
        float deviceWidth = Screen.width;
        float deviceHeight = Screen.height;

        float standardAspect = standardWidth / standardHeight;
        float deviceAspect = deviceWidth / deviceHeight;


		if( deviceAspect < standardAspect )
		{
			float rate = standardAspect / deviceAspect;
			gameObject.GetComponent<UIRoot>().manualHeight = (int)(standardHeight * rate);
		}

		Debug.Log( string.Format("UIRoot ManulHeight={0}", gameObject.GetComponent<UIRoot>().manualHeight), gameObject );
	}

}
