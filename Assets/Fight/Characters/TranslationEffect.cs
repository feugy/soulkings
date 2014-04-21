using UnityEngine;
using System.Collections;

public class TranslationEffect : MonoBehaviour
{
	public float duration;
	public Vector3 translation;

	void Start ()
	{
		transform.localPositionTo ( duration, translation, true ).loopsInfinitely ( GoLoopType.PingPong );
	}
}
