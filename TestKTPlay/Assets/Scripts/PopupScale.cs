using UnityEngine;
using System.Collections;

public class PopupScale : MonoBehaviour 
{
	public ScaleAnim.OnAnimFinished onPopup;
	public ScaleAnim.OnAnimFinished onPopdown;

	public bool scaleDownWhenInactive = true;

	float minScale = 0;
	float maxScale = 1.1f;
	float animTime = 0.3f;
	
	public bool playOnStart = true;
	bool playOnEnable = true;

	public float AnimTime{
		get { return animTime; }
		set { animTime = value; }
	}

	void Start()
	{
		setScaleAnimArgsToPopup();
	}

	void setScaleAnimArgsToPopup()
	{
		ScaleAnim scaleAnim = gameObject.AddMissingComponent<ScaleAnim>();

		scaleAnim.minScale = minScale;
		scaleAnim.maxScale = maxScale;
		scaleAnim.destScale = 1.0f;
		scaleAnim.time = animTime;

		scaleAnim.sclaeCurveType = GCurve.CurveType.POPUP_PANEL;
		scaleAnim.alphaCurveType = GCurve.CurveType.CONSTANT_ONE;

		scaleAnim.playOnStart = playOnStart;
		scaleAnim.playOnEnable = playOnEnable;
		scaleAnim.onAnimFinished = onPopup;
		
		NGUIHelper.SetAlpha(gameObject, 0);
		NGUIHelper.SetDirty(gameObject);
	}
	
	void setScaleAnimArgsToScaleDown()
	{
		ScaleAnim scaleAnim = gameObject.AddMissingComponent<ScaleAnim>();
		
		scaleAnim.minScale = minScale;
		scaleAnim.maxScale = maxScale;
		scaleAnim.destScale = 0.01f;
		scaleAnim.time = animTime;
		
		scaleAnim.sclaeCurveType = GCurve.CurveType.SCALE_DOWN_PANEL;
		scaleAnim.alphaCurveType = GCurve.CurveType.SCALE_DOWN_ALPHA;
		scaleAnim.playOnStart = playOnStart;
		scaleAnim.playOnEnable = playOnEnable;
	}

	public static void ActivePanel(GameObject go, bool isActive)		// alpha it to 0 when active
	{
		if(go.GetComponent<PopupScale>() != null && isActive == true)
		{
			NGUIHelper.SetAlpha(go, 0);
		}
		go.SetActive(isActive);
	}

	public static void SetActive(GameObject go, bool isActive)
	{
		PopupScale popupScale = go.GetComponentInChildren<PopupScale>();
		if(popupScale == null)
		{
			go.SetActive(isActive);
			return;
		}

		if(isActive == true)
		{		
			NGUIHelper.SetAlpha(popupScale.gameObject, 0);
			go.SetActive(true);
		}
		else
		{
			if(popupScale.scaleDownWhenInactive)
			{
				popupScale.StartCoroutine(popupScale.scaleDownAndInactive(go));
			}
			else
			{
				go.SetActive(false);
			}
		}
	}

	public static void ScaleDownAndDestory(GameObject goScale, GameObject goDestory)
	{
//		GLog.Log("ScaleDownAndDestory");

		PopupScale popupScale = goScale.GetComponent<PopupScale>();
		if(popupScale == null || !popupScale.enabled)
		{
			Destroy(goDestory);
			return;
		}

		popupScale.StartCoroutine(popupScale.scaleDownAndDestory(goDestory));
	}

	IEnumerator scaleDownAndDestory(GameObject goDestory = null)
	{
		PlayScaleDownAnim();
		yield return new WaitForSeconds(animTime);

		if(onPopdown!=null) onPopdown();

		if(goDestory != null)
			Destroy(goDestory);
		else
			Destroy(gameObject);

		setScaleAnimArgsToPopup();		// recover scaleAnim args to popup when scaledown finished
	}

	IEnumerator scaleDownAndInactive(GameObject go = null)
	{
		PlayScaleDownAnim();
		yield return new WaitForSeconds(animTime);
		
		if(onPopdown!=null) onPopdown();

		if(go != null)
			go.SetActive(false);
		else
			gameObject.SetActive(false);

		setScaleAnimArgsToPopup();		// recover scaleAnim args to popup when scaledown finished
	}
	
	public void PlayScaleDownAnim(ScaleAnim.OnAnimFinished finishFunc = null)
	{
		ScaleAnim scaleAnim = gameObject.AddMissingComponent<ScaleAnim>();
		scaleAnim.onAnimFinished = finishFunc;

		setScaleAnimArgsToScaleDown();
		scaleAnim.Play();
	}
}
