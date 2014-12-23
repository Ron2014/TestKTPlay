using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class NGUIHelper : MonoBehaviour 
{
	public static Vector2 GetWorldCenter(GameObject go)
	{
		Vector2 center = Vector2.zero;
		
		Vector2 bottomLeft = Vector2.zero;
		Vector2 topRight = Vector2.zero;
		
		UIPanel uiPanel = go.GetComponent<UIPanel>();
		UIWidget uiWidget = go.GetComponent<UIWidget>();
		
		if(uiPanel != null)
		{
			bottomLeft.x = uiPanel.worldCorners[0].x;
			bottomLeft.y = uiPanel.worldCorners[0].y;
			
			topRight.x = uiPanel.worldCorners[2].x;
			topRight.y = uiPanel.worldCorners[2].y;
			
			center = 0.5f * (bottomLeft + topRight);
		}
		else if(uiWidget != null)
		{
			bottomLeft.x = uiWidget.worldCorners[0].x;
			bottomLeft.y = uiWidget.worldCorners[0].y;
			
			topRight.x = uiWidget.worldCorners[2].x;
			topRight.y = uiWidget.worldCorners[2].y;
			
			center = 0.5f * (bottomLeft + topRight);
		}
		
		return center;
	}
	
	public static Vector2 GetWorldCenter(UIWidget widget)		// result is in range -1 ~ 1
	{
		Vector2 bottomLeft = Vector2.zero;
		Vector2 topRight = Vector2.zero;
		
		bottomLeft.x = widget.worldCorners[0].x;
		bottomLeft.y = widget.worldCorners[0].y;
		
		topRight.x = widget.worldCorners[2].x;
		topRight.y = widget.worldCorners[2].y;
		
		return 0.5f * (bottomLeft + topRight);
	}
	
	public static Vector2 GetScreenSpaceCenter(UIWidget widget)		// in pixel
	{
		Vector2 center = GetWorldCenter(widget);
		center.x *= (float)Screen.height / Screen.width;
		
		center.x = (center.x + 1.0f) / 2.0f;
		center.y = (center.y + 1.0f) / 2.0f;
		
		center.x *= Screen.width;
		center.y *= Screen.height;
		
		return center;
	}
	
	public static Vector2 GetScreenSpaceCenterZeroOne(GameObject go)
	{
		Vector2 center = new Vector2(go.transform.position.x, go.transform.position.y);
		return NGUIPosToScreenPos(center);
	}

	public static Vector4 GetWidgetRect(GameObject go)
	{
		Vector2 bottomLeft = Vector2.zero;
		Vector2 topRight = Vector2.zero;
		
		UIPanel uiPanel = go.GetComponent<UIPanel>();
		UIWidget uiWidget = go.GetComponent<UIWidget>();
		
		if(uiPanel != null)
		{
			bottomLeft.x = uiPanel.worldCorners[0].x;
			bottomLeft.y = uiPanel.worldCorners[0].y;
			
			topRight.x = uiPanel.worldCorners[2].x;
			topRight.y = uiPanel.worldCorners[2].y;
		}
		else if(uiWidget != null)
		{
			bottomLeft.x = uiWidget.worldCorners[0].x;
			bottomLeft.y = uiWidget.worldCorners[0].y;
			
			topRight.x = uiWidget.worldCorners[2].x;
			topRight.y = uiWidget.worldCorners[2].y;
		}

		bottomLeft = NGUIPosToScreenPos(bottomLeft);
		topRight = NGUIPosToScreenPos(topRight);
		
		return new Vector4(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);
	}
	
	public static Vector2 NGUIPosToScreenPos(Vector2 nguiPos)
	{
		Vector2 screenPos = nguiPos;
		screenPos.x *= (float)Screen.height / Screen.width;
		
		screenPos.x = (screenPos.x + 1.0f) / 2.0f;
		screenPos.y = (screenPos.y + 1.0f) / 2.0f;
		
		return screenPos;
	}
	
	public static Vector2 ScreenPosToNGUIPos(Vector2 screenPos)
	{
		Vector2 pos = screenPos;
		
		pos.x = pos.x * 2.0f - 1.0f;
		pos.y = pos.y * 2.0f - 1.0f;
		
		pos.x *= (float)Screen.width / Screen.height;
		
		return pos;
	}

	
	public static void SetTransformPos(GameObject go, Camera camera, Vector3 posWolrd)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(posWolrd);
		screenPos.x /= (float)Screen.width;
		screenPos.y /= (float)Screen.height;
		
		Vector2 nguiPos = NGUIHelper.ScreenPosToNGUIPos(new Vector2(screenPos.x, screenPos.y));
		
		go.transform.position = new Vector3(nguiPos.x, nguiPos.y, 0);
	}

	public static void SetTransformPos(GameObject go, Vector2 screenPos)
	{
		go.transform.position = ScreenPosToNGUIPos(screenPos);
	}
	
	public static void SetAlpha(GameObject go, float alpha)		
	{
		if(go.GetComponent<UIPanel>() != null)
		{
			go.GetComponent<UIPanel>().alpha = alpha;
			return;
		}
		
		if(go.GetComponent<UIWidget>() != null)
		{
			go.GetComponent<UIWidget>().alpha = alpha;
			return;
		}
	}

	public static float GetAlpha(GameObject go)
	{
		if(go.GetComponent<UIPanel>() != null)
			return go.GetComponent<UIPanel>().alpha;
		
		if(go.GetComponent<UIWidget>() != null)
			return go.GetComponent<UIWidget>().alpha;

		Debug.LogWarning("NGUIHelper.GetAlpha() return default 0.");
		return 0;
	}

	public static void SetDepth(GameObject go, int depth)
	{
		if(go.GetComponent<UIPanel>() != null)
		{
			go.GetComponent<UIPanel>().depth = depth;
			return;
		}
		
		if(go.GetComponent<UIWidget>() != null)
		{
			go.GetComponent<UIWidget>().depth = depth;
			return;
		}
	}

	public static float GetDepth(GameObject go)
	{
		if(go.GetComponent<UIPanel>() != null)
			return go.GetComponent<UIPanel>().depth;
		
		if(go.GetComponent<UIWidget>() != null)
			return go.GetComponent<UIWidget>().depth;
		
		Debug.LogWarning("NGUIHelper.GetDepth() return default 0.");
		return 0;
	}

	public static void SetDirty(GameObject go)
	{
		if(go.GetComponent<UIPanel>() != null)
		{
			go.GetComponent<UIPanel>().SetDirty();
			return;
		}
		
		if(go.GetComponent<UIWidget>() != null)
		{
			go.GetComponent<UIWidget>().SetDirty();
			return;
		}

	}

	public static void SetColor(GameObject go, Color color)
	{
		if(go.GetComponent<UIWidget>() != null)
		{
			go.GetComponent<UIWidget>().color = color;
			return;
		}
	}

	public static bool MousePosOnWidget(GameObject go)
	{
		Vector2 mousePos = new Vector2((float)Input.mousePosition.x / Screen.width, (float)Input.mousePosition.y / Screen.height);

		return PosOnWidget(go, mousePos);
	}

	public static bool PosOnWidget(GameObject go, Vector2 pos)		// screenPos 0 ~ 1
	{
		Vector4 widgetRect = GetWidgetRect(go);
		
		return GMath.PosInRect(pos, widgetRect);
	}

	public static UIRoot GetTopRoot()
	{
		UIRoot result = null;
		foreach(UIRoot root in GameObject.FindObjectsOfType<UIRoot>())
		{
			if(result==null)
				result = root;

			else{
				int w0=0, w1=0, rate=1;
				UIPanel panel0 = result.GetComponent<UIPanel>();
				UIPanel panel1 = root.GetComponent<UIPanel>();

				if(panel0==null)
					w1 += rate;

				if(panel1==null)
					w0 += rate;

				rate *= 2;

				if(panel0!=null && panel1!=null){
					if(panel0.depth<panel1.depth)
						w1 += rate;
					else if(panel0.depth>panel1.depth)
						w0 += rate;
				}
				
				rate *= 2;

				Camera camera0 = panel0.GetComponentInChildren<Camera>();
				Camera camera1 = panel1.GetComponentInChildren<Camera>();
				
				if(camera0==null)
					w1 += rate;
				
				if(camera1==null)
					w0 += rate;
				
				rate *= 2;
				
				if(camera0!=null && camera1!=null){
					if(camera0.depth<camera1.depth)
						w1 += rate;
					else if(camera0.depth>camera1.depth)
						w0 += rate;
				}

				if(w1>w0)
					result = root;
			}
		}
		return result;
	}

	public static List<GameObject> GetGameObjectsByTag(string tag, List<GameObject> list = null){
		List<GameObject> result = new List<GameObject>();
		if(list==null){
			foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag)){
				result.Add(go);
			}

		}else{			
			foreach(GameObject go in list){
				if(go.tag==tag) result.Add(go);
			}
		}
		return result;
	}

	public static void EnableButtonClick(GameObject go, bool enabled = true)
	{
		EnableCollider(go, enabled);
	}

	public static void EnableCollider(GameObject go, bool enabled = true)
	{
		BoxCollider2D collider2D = go.GetComponentInChildren<BoxCollider2D>();
		if(collider2D != null)
			collider2D.enabled = enabled;
	}

	public static void TranslateLocal(GameObject go, Vector3 offset)
	{
		go.transform.localPosition += offset;
	}

	public static Vector2 GetPivotOffsetFromCenter(GameObject go)	// pivot pos from center in pixel
	{
		Vector2 offset = Vector2.zero;

		UIWidget widget = go.GetComponent<UIWidget>();
		if(widget != null)
		{
			offset.x = (widget.pivotOffset.x - 0.5f) * widget.localSize.x;
			offset.y = (widget.pivotOffset.y - 0.5f) * widget.localSize.y;
		}

		return offset;
	}

	public static int GetMaxDepth(GameObject go)
	{
		int maxDepth = 0;

		foreach(UIWidget widget in go.transform.GetComponentsInChildren<UIWidget>())
		{
			if(widget.depth > maxDepth)
				maxDepth = widget.depth;
		}

		return maxDepth;
	}

	public static EventDelegate EventDelegate(EventDelegate.Callback callBack, GameObject paramGO = null, bool oneshot = true)
	{
		EventDelegate ed = new EventDelegate(callBack);
		ed.oneShot = oneshot;

		if(paramGO != null)
			ed.parameters[0].obj = paramGO;

		return ed;
	}

	/// <summary>
	/// change member-method to non-params method
	/// </summary>
	/// <returns>The closure.</returns>
	/// <param name="target">Target.</param>
	/// <param name="methodName">Method name.(MUST BE PUBLIC!!!)</param>
	/// <param name="args">Arguments.</param>
	public static EventDelegate FuncClosure (object target, string methodName, params object[] args){
		Type protocol = target.GetType();
		MethodInfo method = protocol.GetMethod(methodName);

		EventDelegate result = new EventDelegate(delegate() {
			if(target==null) return;
			method.Invoke(target, args);
		});

		return result;
	}
	
	/// <summary>
	/// change static-method to non-params method
	/// </summary>
	/// <returns>The closure.</returns>
	/// <param name="target">Target.</param>
	/// <param name="methodName">Method name.(MUST BE PUBLIC!!!)</param>
	/// <param name="args">Arguments.</param>
	public static EventDelegate FuncClosure (Type protocol, string methodName, params object[] args){
		MethodInfo method = protocol.GetMethod(methodName);
		
		EventDelegate result = new EventDelegate(delegate() {
			method.Invoke(null, args);
		});
		
		return result;
	}
}
