
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using KTPlaySDKJson;




public class KTFriendship : MonoBehaviour
{
	

	public delegate void Callback(string s);



	public static void SendFriendRequests(string userIds,MonoBehaviour obj,KTFriendship.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTFriendshipAndroid.SendFriendRequest(userIds, obj, callbackMethod);
#elif UNITY_IOS
		 KTFriendshipiOS.SendFriendRequest(userIds,obj,callbackMethod);
#else
#endif
	}


	public static void FriendList(MonoBehaviour obj,KTFriendship.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTFriendshipAndroid.FriendList(obj, callbackMethod);
#elif UNITY_IOS
		 KTFriendshipiOS.FriendList(obj,callbackMethod);
#else
#endif
	}

	public static void ShowFriendRequestsView()
	{
#if UNITY_ANDROID
		KTFriendshipAndroid.ShowFriendRequestsView();
#elif UNITY_IOS
		 KTFriendshipiOS.ShowFriendRequestsView();
#else
#endif	
	}



	public class KTFriendShipCallbackParams
	{
		private const string KEY_WHAT = "what";
		private const string KEY_PARAMS = "params";
		private const string KEY_REQINFO = "requestInfo";

		private const string KEY_REWARD_NAME = "name";
		private const string KEY_REWARD_TYPE = "typeId";
		private const string KEY_REWARD_VALUE = "value";
		

		public enum KTFriendshipEvent
		{
			KTPlayFriendShipEventFriendList = 200,
			KTPlayFriendShipEventFriendRequest,
			OnKTError = 1000
		};
		

		public KTFriendshipEvent friendshipEventResult;
				
		public KTError playError = null;
		public ArrayList friendList = null;
		public double friendRequestCount = 0;
		

		public void ParseFromString(string s)
		{
			Hashtable data = (Hashtable)KTJSON.jsonDecode(s);
			
			if(data[KEY_WHAT] != null)
			{
				this.friendshipEventResult = (KTFriendshipEvent)((double)data[KEY_WHAT]);
				
				switch(this.friendshipEventResult){
					
				case KTFriendshipEvent.KTPlayFriendShipEventFriendList:
				{
					IList list = (IList)data[KEY_PARAMS];
					friendList = new ArrayList();
					foreach (Hashtable item in list)
					{
						KTUser one =  new KTUser(item);
						friendList.Add(one);
					}
					
				}
					break;	
				case KTFriendshipEvent.KTPlayFriendShipEventFriendRequest:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					friendRequestCount = (double)param["count"];
					
				}
					break;
					
				case KTFriendshipEvent.OnKTError:
				{
					Hashtable param = (Hashtable)data[KEY_PARAMS];
					playError = new KTError ();
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
		
		public KTFriendShipCallbackParams(string s)
		{
			this.ParseFromString(s);
		}
	}
	
}



