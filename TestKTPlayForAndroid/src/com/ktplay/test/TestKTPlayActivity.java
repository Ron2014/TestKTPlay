package com.ktplay.test;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.ktplay.open.KTPlay;
import com.unity3d.player.*;

public class TestKTPlayActivity extends UnityPlayerActivity {

	Context mContext = null;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);		
		mContext = this;

		Log.i("Unity", "TestKTPlayActivity.onCreate");
		KTPlay.startWithAppKey(mContext, "HoZZt", "99cd1b31e2c9fe8da44173faba643f202551f4aa");
	}
	
	public void startActivity0(String name){
		Log.i("Unity", "TestKTPlayActivity.startActivity0");
		
		Intent intent = new Intent(mContext, TestActivity0.class);
		intent.putExtra("name", name);
		this.startActivity(intent);
	}
	
	public void startActivity1(String name){
		Log.i("Unity", "TestKTPlayActivity.startActivity1");
		
		Intent intent = new Intent(mContext, TestActivity1.class);
		intent.putExtra("name", name);
		this.startActivity(intent);
	}
	
	@Override
	public void onPause(){
		super.onPause();

		Log.i("Unity", "TestKTPlayActivity.onPause");
		KTPlay.onPause(mContext);
	}
	
	@Override
	public void onResume(){
		super.onResume();

		Log.i("Unity", "TestKTPlayActivity.onResume");
		KTPlay.onResume(mContext);
	}
}
