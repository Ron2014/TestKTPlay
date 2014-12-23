
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using KTPlaySDKJson;




public class KTPlayLeaderboard : MonoBehaviour
{
	public delegate void Callback(string s);


	public static void FriendsLeaderboard(string leaderboardId, int startIndex,int count,MonoBehaviour obj, KTPlayLeaderboard.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTLeaderboardAndroid.FriendsLeaderBoard(leaderboardId, startIndex, count, obj, callbackMethod);
#elif UNITY_IOS
		KTLeaderboardiOS.FriendsLeaderBoard(leaderboardId,startIndex,count,obj,callbackMethod);
#else
#endif
	}
	
	public static void GlobalLeaderboard(string leaderboardId,int startIndex,int count,MonoBehaviour obj,KTPlayLeaderboard.Callback callbackMethod){
#if UNITY_ANDROID
		KTLeaderboardAndroid.GlobalLeaderboard(leaderboardId,startIndex,count,obj,callbackMethod);

#elif UNITY_IOS
		KTLeaderboardiOS.GlobalLeaderboard(leaderboardId,startIndex,count,obj,callbackMethod);
#else
#endif

	}

	public static void ReportScore(int score,string leaderboardId,MonoBehaviour obj,KTPlayLeaderboard.Callback callbackMethod)
	{
#if UNITY_ANDROID
		KTLeaderboardAndroid.ReportScore(score, leaderboardId, obj, callbackMethod);
#elif UNITY_IOS
		KTLeaderboardiOS.ReportScore(score,leaderboardId,obj,callbackMethod);
#else
#endif
	}



	public class KTLeaderboardPaginator
	{

		public double total;

		//nextCursor is invailid in globalLeaderboard
		public string nextCursor;

		//previousCursor is invailid in globalLeaderboard
		public string previousCursor;

		public ArrayList items;

		public string leaderboardName;


		public string leaderboardIcon;

		public string leaderboardId;


		//myRank is valid in globalLeaderboard
		public double myRank;

		public string myScore;

		public string periodicalSummaryId;

		public KTLeaderboardPaginator(Hashtable param)
		{
			if(param["total"]!=  null){
				this.total = (double)param["total"];
			}

			if((string)param["nextCursor"]!=  null){
				this.nextCursor = (string)param["nextCursor"];
			}

			if((string)param["previousCursor"]!=  null){
				this.previousCursor = (string)param["previousCursor"];
			}


			if(param["myRank"]!=  null){
				this.myRank = (double)param["myRank"];
			}

			if(param["myScore"]!=  null){
				this.myScore = (string)param["myScore"];
			}


			if(param["items"] != null){

				IList list = (IList)param["items"];
				this.items = new ArrayList();
				foreach(Hashtable user in list)
				{
					KTUser one = new KTUser(user);
					this.items.Add(one);
				}
			}

			if((string)param["leaderboardName"]!=  null){
				this.leaderboardName = (string)param["leaderboardName"];
			}

			if((string)param["leaderboardIcon"]!=  null){
				this.leaderboardIcon = (string)param["leaderboardIcon"];
			}

			if((string)param["leaderboardId"]!=  null){
				this.leaderboardId = (string)param["leaderboardId"];
			}

			if((string)param["periodicalSummaryId"]!=  null){
				this.periodicalSummaryId = (string)param["periodicalSummaryId"];
			}
		}
	}



	public class KTLeaderboardCallbackParams
	{
		private const string KEY_WHAT = "what";
		private const string KEY_PARAMS = "params";
		private const string KEY_REQINFO = "requestInfo";


		public enum KTLeaderboardEvent
		{
			KTPlayLeaderboardEventFriendsLeaderboard = 300,
			KTPlayLeaderboardEventGlobalLeaderboard,
			KTPlayLeaderboardEventReportScore,
			OnKTError = 1000
		};


		public KTLeaderboardEvent leaderboardEventResult;


		public KTError playError = null;


		public KTLeaderboardPaginator friendLeaderboardPaginator = null;


		public KTLeaderboardPaginator globalLeaderboardPaginator = null;

		public string leaderboardId = null;

		public double score;


		public void ParseFromString(string s)
		{
			Hashtable data = (Hashtable)KTJSON.jsonDecode(s);

			if(data[KEY_WHAT] != null)
			{
				this.leaderboardEventResult = (KTLeaderboardEvent)((double)data[KEY_WHAT]);

				switch(this.leaderboardEventResult){

					case KTLeaderboardEvent.KTPlayLeaderboardEventFriendsLeaderboard:
					{
						Hashtable param = (Hashtable)data[KEY_PARAMS];
						friendLeaderboardPaginator =  new KTLeaderboardPaginator(param);

						if(data[KEY_REQINFO] != null){
							Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
							leaderboardId = (string)requestInfo[@"leaderboardId"];
						}

					}
						break;
					case KTLeaderboardEvent.KTPlayLeaderboardEventGlobalLeaderboard:
					{
						Hashtable param = (Hashtable)data[KEY_PARAMS];
						globalLeaderboardPaginator =  new KTLeaderboardPaginator(param);

						if(data[KEY_REQINFO] != null){
							Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
							leaderboardId = (string)requestInfo[@"leaderboardId"];
						}

					}
						break;
					case KTLeaderboardEvent.KTPlayLeaderboardEventReportScore:
					{
						if(data[KEY_REQINFO] != null){
							Hashtable requestInfo = (Hashtable)data[KEY_REQINFO];
							leaderboardId = (string)requestInfo[@"leaderboardId"];
							score = (double)requestInfo[@"score"];
						}

					}
						break;

					case KTLeaderboardEvent.OnKTError:
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
							leaderboardId = (string)requestInfo[@"leaderboardId"];
							if(requestInfo[@"score"] != null){
								score = (double)requestInfo[@"score"];
							}
						}
					}
						break;
				}
			}
		}


		public KTLeaderboardCallbackParams(string s)
		{
			this.ParseFromString(s);
		}
	}

}
