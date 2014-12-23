
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using KTPlaySDKJson;



public class KTAccountManager : MonoBehaviour
{


	public delegate void Callback(string s);

	



	public static void SetLoginStatusChange(MonoBehaviour obj,KTAccountManager.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTAccountManagerAndroid.SetLoginStatusChangeCallback(obj, callbackMethod);
#elif UNITY_IOS

		 KTAccountManageriOS.SetLoginStatusChangeCallback(obj,callbackMethod);
#else
#endif
	}

	public static void UserProfile(string userId,MonoBehaviour obj,KTAccountManager.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTAccountManagerAndroid.UserProfile(userId, obj, callbackMethod);
#elif UNITY_IOS
		KTAccountManageriOS.UserProfile(userId,obj,callbackMethod);
#else
#endif
	}



	public static void ShowLoginView(bool closeable, MonoBehaviour obj,KTAccountManager.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTAccountManagerAndroid.ShowLoginView(closeable, obj,callbackMethod);
#elif UNITY_IOS
		KTAccountManageriOS.ShowLoginView(closeable, obj,callbackMethod);
#else
#endif
	}
	
	public static void loginWithGameUser(string gameUserId, MonoBehaviour obj,KTAccountManager.Callback callbackMethod){
#if UNITY_ANDROID
		KTAccountManagerAndroid.loginWithGameUser(gameUserId, obj,callbackMethod);
#elif UNITY_IOS
		KTAccountManageriOS.loginWithGameUser(gameUserId, obj,callbackMethod);
#else
#endif	
	}
	
	public static void setNickname(string nickName, MonoBehaviour obj,KTAccountManager.Callback callbackMethod){
#if UNITY_ANDROID
		KTAccountManagerAndroid.setNickname(nickName, obj,callbackMethod);
#elif UNITY_IOS
		KTAccountManageriOS.setNickname(nickName, obj,callbackMethod);
#else
#endif
		
	}
	
	public static void logout(){	
#if UNITY_ANDROID
		KTAccountManagerAndroid.logout();
#elif UNITY_IOS
		KTAccountManageriOS.logout();
#else
#endif	
	}

	

	public static bool IsLoggedIn()
	{
#if UNITY_ANDROID
		return KTAccountManagerAndroid.IsLoggedIn();
#elif UNITY_IOS
		return KTAccountManageriOS.IsLoggedIn();
#else
#endif
		return false;
	}
	

	public static KTUser CurrentAccount()
	{
#if UNITY_ANDROID
		return KTAccountManagerAndroid.CurrentAccount();
#elif UNITY_IOS
		return KTAccountManageriOS.CurrentAccount();
#else
#endif
		return null;
	}




	public class KTAccountManagerCallbackParams
	{
		private const string KEY_WHAT = "what";
		private const string KEY_PARAMS = "params";
		private const string KEY_REQINFO = "requestInfo";

		
		public enum KTAccountManagerEvent
		{
			KTPlayAccountEventStatusChange = 100,
			KTPlayAccountEventUserProfile,
			KTPlayAccountEventLoginViewLogin,
			KTPlayAccountEventLoginWhithGameUser,
			KTPlayAccountEventSetNickName,
			OnKTError = 1000
		};
		

		public KTAccountManagerEvent accountManagerEventResult;

		
		public KTError playError = null;
		public KTUser statusUser = null;
		public KTUser oneUser = null;
		public bool isLogin = false;
		
		public string userId = null;
		public string nickName = null;
		public string gameUserId = null;


		public void ParseFromString(string s)
		{
			Hashtable data = (Hashtable)KTJSON.jsonDecode(s);			
			
			if(data[KEY_WHAT] != null)
			{
				this.accountManagerEventResult = (KTAccountManagerEvent)((double)data[KEY_WHAT]);
				
				switch(this.accountManagerEventResult){
					
				case KTAccountManagerEvent.KTPlayAccountEventStatusChange:
				{
					
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					
					if(param["userId"] == null)
					{
						statusUser = null;
					}else{
						statusUser = new KTUser(param);
					}
					
					if((bool)param["isLogin"] == true){
						isLogin = true;
					}
					else{
						isLogin = false;
					}
					
				}
					break;
					
				case KTAccountManagerEvent.KTPlayAccountEventUserProfile:
				{
					
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if(param["userId"] == null)
					{
						oneUser = null;
					}else{
						oneUser = new KTUser(param);
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						userId = (string)requestInfo[@"userId"];
					}
				}
					
					break;
				case KTAccountManagerEvent.KTPlayAccountEventLoginViewLogin:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if(param["userId"] == null)
					{
						oneUser = null;
					}else{
						oneUser = new KTUser(param);
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						userId = (string)requestInfo[@"userId"];
					}
					
				}
					break;
					
				case KTAccountManagerEvent.KTPlayAccountEventLoginWhithGameUser:{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if(param["userId"] == null)
					{
						oneUser = null;
					}else{
						oneUser = new KTUser(param);
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						gameUserId = (string)requestInfo[@"gameUserId"];
					}
				}
					break;
					
				case KTAccountManagerEvent.KTPlayAccountEventSetNickName:{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					if(param["userId"] == null)
					{
						oneUser = null;
					}else{
						oneUser = new KTUser(param);
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						nickName = (string)requestInfo[@"nickName"];
					}
				}
					break;
					
				case KTAccountManagerEvent.OnKTError:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					playError = new KTError ();
					if(param["description"] != null){
						playError.description = (string)param["description"];
					}
					if(param["code"] != null){
						playError.code = (double)param["code"];
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						userId = (string)requestInfo[@"userId"];
					}else{
						userId = null;
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						gameUserId = (string)requestInfo[@"gameUserId"];
					}else{
						gameUserId = null;
					}
					
					if(data[KEY_REQINFO] != null){
						Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
						nickName = (string)requestInfo[@"nickName"];
					}else{
						nickName = null;
					}
				}
					break;
				}
			}
		}
		
		public KTAccountManagerCallbackParams(string s)
		{
			this.ParseFromString(s);
		}
		
	};

}



