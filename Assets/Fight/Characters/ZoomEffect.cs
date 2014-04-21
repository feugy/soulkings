using UnityEngine;
using System.Collections;

public class ZoomEffect : MonoBehaviour
{
	public float duration;
	public float scaleFrom = 1;
	public float scaleTo = 1;

	void Start ()
	{
		transform.localScale = new Vector3 ( scaleFrom, scaleFrom, scaleFrom );
		transform.scaleTo ( duration, scaleTo ).loopsInfinitely ( GoLoopType.PingPong );
	}
}
