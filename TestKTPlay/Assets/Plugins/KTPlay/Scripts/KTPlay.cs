using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using KTPlaySDKJson;


public class KTPlay : MonoBehaviour
{
	private static bool isScreenShooting = false;






	public class KTPlayRewardsItem
	{

		public string name;   
		
	
		public string typeId;  
		
	
		public double value;   
	}

	public delegate void Callback(string s);

	public void Awake()
	{
#if UNITY_ANDROID
		KTPlayAndroid.Init();
#endif
	}


	public static void SetViewDidAppearCallback(MonoBehaviour obj, KTPlay.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetViewDidAppearCallback(obj, callbackMethod);
#elif UNITY_IOS
		KTPlayiOS.SetViewDidAppearCallback(obj,callbackMethod);
#else
#endif
	}


	public static void SetViewDidDisappearCallback(MonoBehaviour obj, KTPlay.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetViewDidDisappearCallback(obj, callbackMethod);
#elif UNITY_IOS
		KTPlayiOS.SetViewDidDisappearCallback(obj,callbackMethod);
#else
#endif
	}


	public static void SetDidDispatchRewardsCallback(MonoBehaviour obj, KTPlay.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetDidDispatchRewardsCallback(obj, callbackMethod);
#elif UNITY_IOS
		KTPlayiOS.SetDidDispatchRewardsCallback(obj,callbackMethod);
#else
#endif
	}


	public static void SetActivityStatusChangedCallback(MonoBehaviour obj, KTPlay.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetActivityStatusChangedCallback(obj, callbackMethod);
#elif UNITY_IOS
		KTPlayiOS.SetActivityStatusChangedCallback(obj,callbackMethod);
#else
#endif
	}
	
	public static void SetAvailabilityChangedCallback(MonoBehaviour obj, KTPlay.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetAvailabilityChangedCallback(obj, callbackMethod);
#elif UNITY_IOS
		KTPlayiOS.SetAvailabilityChangedCallback(obj,callbackMethod);
#else
#endif
	}


	public static void Show()
	{
#if UNITY_ANDROID
		KTPlayAndroid.Show();
#elif UNITY_IOS
		KTPlayiOS.Show();
#else
#endif
	}


	public static void Dismiss()
	{
#if UNITY_ANDROID
		KTPlayAndroid.Dismiss();
#elif UNITY_IOS
		KTPlayiOS.Dismiss();
#else
#endif
	}


	public static bool IsEnabled()
	{
#if UNITY_ANDROID
		return KTPlayAndroid.IsEnabled();
#elif UNITY_IOS
		return KTPlayiOS.IsEnabled();
#else
		return false;
#endif
	}


	public static bool IsShowing()
	{
#if UNITY_ANDROID
		return KTPlayAndroid.IsShowing();
#elif UNITY_IOS
		return KTPlayiOS.IsShowing();
#else
		return false;
#endif
	}


	public static void SetScreenshotRotation(float degress)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetScreenshotRotation(degress);
#elif UNITY_IOS
		isScreenShooting = true;
		KTPlayiOS.SetScreenshotRotation(degress);
#else
#endif
	}



	public static void ShareImageToKT(string imagePath,string description)
	{
#if UNITY_ANDROID
		KTPlayAndroid.ShareImageToKT(imagePath, description);
#elif UNITY_IOS
		KTPlayiOS.ShareImageToKT(imagePath, description);
#else
#endif
	}


	public static void SetNotificationEnabled(bool enabled)
	{
#if UNITY_ANDROID
		KTPlayAndroid.SetNotificationEnabled(enabled);
#elif UNITY_IOS
		 KTPlayiOS.SetNotificationEnabled(enabled);
#else
#endif
	}


	public class KTPlayCallbackParams
	{
		private const string KEY_WHAT = "what";
		private const string KEY_PARAMS = "params";
		private const string KEY_REWARD_NAME = "name";
		private const string KEY_REWARD_TYPE = "typeId";
		private const string KEY_REWARD_VALUE = "value";

		public bool hasNewActivity = false;
		public enum KTPlayEvent
		{
			OnAppear = 0,
			OnDisappear,
			OnDispatchRewards,
			OnActivityStatusChanged,
			OnAvailabilityChanged,
			OnKTError = 1000
		};


		public KTPlayEvent KTPlayEventResult;

		public ArrayList rewards;
		public KTError playError = null;
		
		public bool isEnabled;



		public void ParseFromString(string s)
		{
			Hashtable data = (Hashtable)KTJSON.jsonDecode(s);
			this.rewards = null;

			if(data[KEY_WHAT] != null)
			{
				this.KTPlayEventResult = (KTPlayEvent)((double)data[KEY_WHAT]);
				Debug.Log("param.KTPlayEvent=" + this.KTPlayEventResult);

				switch(this.KTPlayEventResult){

				case KTPlayEvent.OnDispatchRewards:
				{
					IList list = (IList)data[KEY_PARAMS];
					rewards = new ArrayList();
					foreach (IDictionary item in list)
					{
						KTPlayRewardsItem rewardItem = new KTPlayRewardsItem();
						if(item[KEY_REWARD_NAME] != null)
							rewardItem.name = (string)item[KEY_REWARD_NAME];
						if(item[KEY_REWARD_TYPE] != null)
							rewardItem.typeId =  (string)item[KEY_REWARD_TYPE];
						if(item[KEY_REWARD_VALUE] != null)
							rewardItem.value = (double)item[KEY_REWARD_VALUE];
						rewards.Add(rewardItem);
					}
				}
					break;
				case KTPlayEvent.OnAppear:
				{
					#if UNITY_IOS
					isScreenShooting = true;
					#endif
				}
					break;
				case KTPlayEvent.OnDisappear:

					break;
				case KTPlayEvent.OnActivityStatusChanged:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if((bool)param["hasNewActivity"] == true){
						hasNewActivity = true;
					}
					else{
						hasNewActivity = false;
					}
				}
					break;
				case KTPlayEvent.OnAvailabilityChanged:{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if((bool)param["isEnabled"] == true){
						isEnabled = true;
					}
					else{
						isEnabled = false;
					}
				}
					break;
				case KTPlayEvent.OnKTError:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					playError = new KTError();
					if(param["description"] != null){
						playError.description = (string)param["description"];
					}
					if(param["code"] != null){
						playError.code = (double)param["code"];
					}
				}
					break;
				}
			}
		}

		public KTPlayCallbackParams(string s)
		{
			this.ParseFromString(s);
		}
	};

	IEnumerator CapturePNG()
	{
		yield return new WaitForEndOfFrame();
		Debug.Log("post");
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] bytes = tex.EncodeToPNG();
		Destroy(tex);

		
		string path = Application.temporaryCachePath + "/kt_captureScreenshot";
		System.IO.File.WriteAllBytes(path,bytes);

	}

	void Update()
	{
#if UNITY_IOS


		if(isScreenShooting)
		{
			StartCoroutine (CapturePNG());

		
			isScreenShooting = false;
		}

#endif

#if UNITY_ANDROID
		KTPlayAndroid.Update();
#endif
	}
}