using UnityEngine;
using System.Collections;

public class ScaleAnim : MonoBehaviour 
{
	public float minScale = 0;
	public float maxScale = 1.0f;
	public float destScale = 1.0f;

	private Vector3 mInitialScale = Vector3.one;

	public float time = 1.0f;
	public GCurve.CurveType sclaeCurveType = GCurve.CurveType.DEFAULT_LINEAR;
	public GCurve.CurveType alphaCurveType = GCurve.CurveType.CONSTANT_ONE;
	
	public bool playOnStart = false;
	public bool playOnEnable = false;
	
	float mTimer = 0;
	bool mIsPlaying = false;
	public bool IsPlaying{
		get { return mIsPlaying; }
	}

	AnimationCurve mCurveScale;
	AnimationCurve mCurveAlpha;
	
	public delegate void OnAnimFinished();
	public OnAnimFinished onAnimFinished;

	void Start()
	{
		mInitialScale = transform.localScale;

		if(playOnStart)
			Play();
	}

	void OnEnable()
	{
		reset();

		if(playOnEnable)
			Play();
	}

	void reset()
	{
		mTimer = 0;
		mIsPlaying = false;
	}
	
	void Update()
	{
		if(mTimer > 0)
		{
			if(mIsPlaying == false)
			{
				mIsPlaying = true;
				setAlphaToOne();
			}


			float factor = (time - mTimer) / time;
			float curveValue = mCurveScale.Evaluate(factor);
			
			setAlpha();

			float scale = minScale + curveValue * (maxScale - minScale);

			transform.localScale = scale * mInitialScale;
			NGUIHelper.SetDirty(gameObject);
			
			mTimer -= Time.deltaTime;
		}
		else
		{
			if(mIsPlaying)
			{
				// finished
				mIsPlaying = false;
				transform.localScale = destScale * mInitialScale;	// incase fps is too low, set scale to dest when finish anyway.

				if(onAnimFinished != null)
					onAnimFinished();
			}
		}
	}

	[ContextMenu("Execute")]
	public void Play()
	{
		reset();
		
		mCurveScale = GCurve.GetCurve(sclaeCurveType);
		mCurveAlpha = GCurve.GetCurve(alphaCurveType);
		mTimer = time;
	}

	void setAlphaToOne()
	{
		NGUIHelper.SetAlpha(gameObject, 1.0f);
	}

	void setAlpha()
	{
		float factor = (time - mTimer) / time;
		float curveValue = mCurveAlpha.Evaluate(factor);

		NGUIHelper.SetAlpha(gameObject, curveValue);
	}	
}
