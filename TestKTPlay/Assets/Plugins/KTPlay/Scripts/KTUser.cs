using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



public class KTUser
{

	public string userId;

	public string headerUrl;
	

	public string nickname;

	public double gender;

	public string city;

	public string score;

	public double rank;
	

	public string snsUserId;
	
	

	public string loginType;
	
	public string gameUserId;
	
	public KTUser()
		{
		}

	
	public void setUserId(string userId) {
		this.userId = userId;
	}
	public void setHeaderUrl(string headerUrl) {
		this.headerUrl = headerUrl;
	}
	public void setNickname(string nickname) {
		this.nickname = nickname;
	}
	public void setGender(int gender) {
		this.gender = (double)gender;
	}
	public void setCity(string city) {
		this.city = city;
	}
	public void setScore(string score) {
		this.score = score;
	}
	public void setRank(long rank) {
		this.rank = (double)rank;
	}
	public void setSnsUserId(string snsUserId) {
		this.snsUserId = snsUserId;
	}
	public void setLoginType(string loginType) {
		this.loginType = loginType;
	}
	
	public void setGameUserId(string gameUserId)
	{
		this.gameUserId = gameUserId;
	}

	public  KTUser(Hashtable param)
	{
		if((string)param["userId"]!= null)
			this.userId = (string)param["userId"];

		if((string)param["headerUrl"]!=  null)
			this.headerUrl = (string)param["headerUrl"];

		if((string)param["nickname"]!= null)
			this.nickname = (string)param["nickname"];

		if((string)param["city"]!=  null)
			this.city = (string)param["city"];

		if((string)param["score"]!= null)
			this.score = (string)param["score"];

		if((string)param["snsUserId"]!=  null)
			this.snsUserId = (string)param["snsUserId"];

		if((string)param["loginType"]!=  null)
			this.loginType = (string)param["loginType"];

		if(param["gender"]!= null){
			this.gender = (double)param["gender"] ;
		}

		if(param["rank"]!= null){
			this.rank = (double)param["rank"] ;
		}
		
		if(param["gameUserId"]!=null){
			this.gameUserId = (string)param["gameUserId"];
		}
	}
}