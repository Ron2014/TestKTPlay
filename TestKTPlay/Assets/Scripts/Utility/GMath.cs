using UnityEngine;
using System.Collections;

static public class GMath 
{
	public static void BubbleSort(float[] list)
	{
		int count = list.Length;
		
		for(int i = 0; i < count - 1; ++i)
		{
			for(int j = 0; j < count - i - 1; ++j)
			{
				if(list[j] > list[j+1])
				{
					float tmp = list[j];
					list[j] = list[j+1];
					list[j+1] = tmp;
				}
			}
		}
	}

	public static bool FloatEqual(float val1, float val2, float tolerance = 0.0001f)
	{
		return Mathf.Abs(val1 - val2) < tolerance;
	}

	public static bool FloatNotEqual(float val1, float val2, float tolerance = 0.0001f)
	{
		return !FloatEqual(val1, val2, tolerance);
	}
	
	public static float NormalizedDegree(float degree)		// normalize angle to 0 ~ 360
	{
		while(degree < 0)
			degree += 3600;
		
		return degree % 360.0f;
	}

	public static float RndValue(float val, float valRnd)
	{
		return Random.Range(val - valRnd, val + valRnd);
	}
	
	public static Vector2 RndValue(Vector2 val, Vector2 valRnd)
	{
		return new Vector2(Random.Range(val.x - valRnd.x, val.x + valRnd.x), 
		                   Random.Range(val.y - valRnd.y, val.y + valRnd.y));
	}

	public static float RndSign()
	{
		float value = Random.Range(-1.0f, 1.0f);
		return value > 0 ? 1.0f : -1.0f;
	}

	public static bool PosInRect(Vector2 pos, Vector4 rect)
	{
		if(pos.x > rect.x && pos.x < rect.z && 
		   pos.y > rect.y && pos.y < rect.w)
			return true;

		return false;
	}

	public static Vector3 Lerp(Vector3 from, Vector3 to, float factor)		// diffrent from Unity's Vector3.Lerp, factor can be greater than 1.0f
	{
		return from + factor * (to - from);
	}

	// see: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205509(v=vs.85).aspx
	public static Vector3 HermiteInterpolation(Vector3 v1, Vector3 t1, Vector3 v2, Vector3 t2, float weightFactor)
	{
		Vector3 resultPos = v1;
		
		float s = weightFactor;
		float s_pow2 = s*s;
		float s_pow3 = s*s*s;
		
		float h1 = 2.0f * s_pow3 - 3.0f * s_pow2 + 1.0f;
		float h2 = -2.0f * s_pow3 + 3.0f * s_pow2;
		float h3 = s_pow3 - 2.0f * s_pow2 + s;
		float h4 = s_pow3 - s_pow2;
		
		resultPos = h1 * v1 + h2 * v2 + h3 * t1 + h4 * t2;
		
		return resultPos;
	}

	public static Vector3 HermiteInterpolationTangent(Vector3 v1, Vector3 t1, Vector3 v2, Vector3 t2, float weightFactor)
	{
		Vector3 resultTangent = t1;
		
		float s = weightFactor;
		float s_pow2 = s*s;

		float h1 = 6.0f * s_pow2 - 6.0f * s;
		float h2 = -h1;
		float h3 = 3.0f * s_pow2 - 4.0f * s + 1.0f;
		float h4 = 3.0f * s_pow2 - 2.0f * s;

		resultTangent = h1 * v1 + h2 * v2 + h3 * t1 + h4 * t2;

		return resultTangent;
	}
}
