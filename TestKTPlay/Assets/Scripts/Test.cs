using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	GameObject goBtnKTPlay;

	// Use this for initialization
	void Start () {
		GameObject goBtn0 = GameObject.Find("Button0");
		UIEventListener.Get(goBtn0).onClick = OnClickButton0;

		GameObject goBtn1 = GameObject.Find("Button1");
		UIEventListener.Get(goBtn1).onClick = OnClickButton1;

		GameObject goBtn2 = GameObject.Find("Button2");
		UIEventListener.Get(goBtn2).onClick = OnClickButton2;

		goBtnKTPlay = GameObject.Find("ButtonKTPlay");
		UIEventListener.Get(goBtnKTPlay).onClick = OnClickButtonKTPlay;

		DontDestroyOnLoad(gameObject);
	}

	void OnEnable(){
		goBtnKTPlay.SetActive(KTPlay.IsEnabled());
	}

	void OnResume(){
		goBtnKTPlay.SetActive(KTPlay.IsEnabled());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnClickButton0(GameObject go){
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("startActivity0", "this is first activity");
	}

	void OnClickButton1(GameObject go){
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("startActivity1", "this is second activity");
	}

	void OnClickButton2(GameObject go){
		Application.Quit ();
	}

	void OnClickButtonKTPlay(GameObject go){
		KTPlay.Show();
	}

	void GetMessage(string msg){
		HintContainer.Create(msg);
	}

	void Log(string msg){
		Debug.Log(string.Format("===============Log {0}", msg));
	}
}
