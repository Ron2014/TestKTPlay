package com.ktplay.test;

import com.unity3d.player.UnityPlayer;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class TestActivity1 extends Activity {

	@Override
	public void onCreate(Bundle savedInstanceState){
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);

		TextView text = (TextView)this.findViewById(R.id.textView0);
		text.setText(this.getIntent().getStringExtra("name"));
		
		final EditText edit = (EditText)this.findViewById(R.id.editText1);
		
		Button btn = (Button)this.findViewById(R.id.button0);
		btn.setOnClickListener(new OnClickListener(){
			
			@Override
			public void onClick(View v){
				UnityPlayer.UnitySendMessage("Platform", "GetMessage", String.format("TestActivity1 send %s", edit.getText().toString()));
				TestActivity1.this.finish();
			}
			
		});
	}

}
