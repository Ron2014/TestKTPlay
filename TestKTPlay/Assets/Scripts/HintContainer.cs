using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HintContainer : MonoBehaviour {
	public int maxCount = 5;
	public float moveDuration = 0.2f;

	List<HintNode> mWaitingList = new List<HintNode>();
//	bool _mIsChanging  = false;
//	bool mIsChanging{
//		get { return _mIsChanging; }
//		set {
//			GLog.Log(string.Format("<color=red>set mIsChanging {0}</color>", value));
//			_mIsChanging = value;
//		}
//	}
	bool mIsChanging = false;

	GameObject prefab;
	Transform mTable;
	
	public static void Create(string text){
		Instance.AddTextNode(text);
		Instance.GetComponent<UITopPanel>().SetToTop();
	}

	public static HintContainer Instance{
		get {
			HintContainer ret = GameObject.FindObjectOfType<HintContainer>();			
			if(ret==null) ret = HintContainer.Create ();
			return ret;
		}
	}
	
	static HintContainer Create (){
		UIRoot root = GameObject.FindObjectOfType<UIRoot>();
		GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/MsgBox/Hint_Container");
		GameObject go = NGUITools.AddChild(root.gameObject, prefab);
		go.transform.localScale = Vector3.one;

		return go.GetComponent<HintContainer>();
	}

	void Awake(){		
		prefab = Resources.Load<GameObject>("Prefabs/UI/MsgBox/Hint_Node");
		mTable = transform.FindChild("Table");
		
		TweenPosition tp = mTable.GetComponent<TweenPosition>();
		tp.duration = moveDuration;
		tp.onFinished.Add(new EventDelegate(OnFinishTween));
	}

	[ContextMenu("NotifyChanged")]
	void NotifyChanged(){
		if(mWaitingList.Count<=0) return;
		if(mIsChanging) return;

		mIsChanging = true;
		
		HintNode newNode = mWaitingList[0];
		int childCount = mTable.childCount;

		if(childCount>0){			
			int hideCount = childCount - maxCount;
			for(int i=0; i<=hideCount; i++){
				Transform topChild = mTable.GetChild(i);
				topChild.GetComponent<HintNode>().Hide();
			}

			Vector2 newSize = newNode.GetSize();			
			Vector2 lastSize = mTable.GetChild(childCount-1).GetComponent<HintNode>().GetSize ();
			float distance = (newSize.y + lastSize.y) * 0.5f;

			if(distance>0.1f){				
				Vector3 tablePos = mTable.localPosition;
				tablePos.y += distance;
								
//				GLog.Log(string.Format("<color=yellow>distance={0} {1} {2}</color>", distance,mTable.localPosition, tablePos));

				TweenPosition tp = mTable.GetComponent<TweenPosition>();
				tp.from = mTable.localPosition;
				tp.to = tablePos;
				tp.PlayForward();

			}else
				OnFinishTween();
		}else
			OnFinishTween();
	}

	void OnFinishTween(){
//		GLog.Log(string.Format("<color=yellow>HintContainer.OnFinishTween</color>"));

		HintNode newNode = mWaitingList[0];
		mWaitingList.Remove(newNode);

		newNode.transform.parent = mTable.transform;
		newNode.transform.localScale = Vector3.one;
		newNode.gameObject.SetActive(true);

		GetComponent<UITopPanel>().SetToTop();

		mIsChanging = false;
		Invoke("NotifyChanged", 0f);
	}
	
	public void AddTextNode(string text){
		GameObject go = NGUITools.AddChild(null, prefab);

		HintNode hintNode = go.GetComponent<HintNode>();
		hintNode.UpdateView(text);		
		hintNode.gameObject.SetActive(false);
		mWaitingList.Add(hintNode);

//		GLog.Log(string.Format("<color=yellow>HintContainer.AddTextNode {0}</color>", text));

		NotifyChanged();
	}

	public void ResetPos(Vector2 size){
		if(mTable.childCount==0)
			mTable.localPosition = Vector3.zero;
	}
}
