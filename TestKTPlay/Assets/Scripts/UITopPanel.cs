using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITopPanel : MonoBehaviour {
	public static int MAGIC_DEPTH = 9999999;
	public UIPanel panel;
	bool mStarted = false;

	void FindPanel(){
		panel = GetComponentInChildren<UIPanel>();
	}

	void Start (){
		SetToTop();
		mStarted = true;
	}

	void OnEnable(){
		if(mStarted) SetToTop();
	}

	public void SetToTop()
	{
		if(panel==null)
			FindPanel();

		if( panel != null && panel.depth >= MAGIC_DEPTH )
			return;
		
		GameObject root = NGUITools.GetRoot(gameObject);
		
		int maxDepth = 0;
		foreach(UIPanel p in root.transform.GetComponentsInChildren<UIPanel>()){
			if(p.depth>maxDepth && !NGUITools.IsChild(transform, p.transform))
				if( p.depth < MAGIC_DEPTH )
					maxDepth = p.depth;
		}

		List<UIPanel> list = new List<UIPanel>(GetComponentsInChildren<UIPanel>());
		list.Sort(delegate(UIPanel x, UIPanel y) {
			int w0 = 0, w1 = 0;
			int rate = 1;

			if(x.depth<y.depth)
				w1 += rate;
			else if(x.depth>y.depth)
				w0 += rate;

			return w0 - w1;
		});

		for(int i=0; i<list.Count; i++){
			UIPanel node = list[i];
			node.depth = maxDepth + 1 + i;
		}
	}
}
