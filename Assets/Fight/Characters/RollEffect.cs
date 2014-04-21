using UnityEngine;
using System.Collections;

public class RollEffect : MonoBehaviour
{
	public float duration;
	public float startAngle;
	public float angle;

	void Start ()
	{
		// transform.eulerAngles = new Vector3 ( 0, 0, transform.eulerAngles.z + startAngle );
		transform.localEularAnglesTo ( duration, new Vector3 ( 0, 0, angle ), true ).loopsInfinitely ( GoLoopType.PingPong );
	}
}
