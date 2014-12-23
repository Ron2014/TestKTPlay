using UnityEngine;
using System.Collections;

public class HintNode : MonoBehaviour {
	public float stay = 3.0f;
	public float duration = 0.5f;

	bool isHiding;
	UILabel labelText;
	UISprite spriteBg;
	
	void Awake () {
		isHiding = false;
		GetComponent<UIWidget>().alpha = 0;

		spriteBg = transform.FindChild("Message/Background").GetComponent<UISprite>();
		labelText = transform.FindChild("Message").GetComponent<UILabel>();
	}

	public void UpdateView(string text){
		labelText.text = text;

		// change size
		spriteBg.Update();
	}
	
	public void Hide(){
		if(isHiding) return;

		isHiding = true;
		TweenAlpha.Begin(gameObject, duration, 0.0f).onFinished.Add(new EventDelegate(OnFinishHide));
	}

	void OnEnable(){
//		GLog.Log ("OnEnable");
		Invoke("Hide", stay);
	}

	public void OnFinishHide(){
		HintContainer container = NGUITools.FindInParents<HintContainer>(gameObject);
		Vector2 size = GetSize ();
		NGUITools.Destroy(gameObject);

		if(container!=null)
			container.ResetPos(size);
	}

	public Vector2 GetSize(){
		Vector2 size = Vector2.zero;
		size.x += spriteBg.width;
		size.y += spriteBg.height;
		return size;
	}
}
