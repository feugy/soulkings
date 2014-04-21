using UnityEngine;
using System.Collections;

public abstract class BaseBehaviourIcon : BehaviourIcon
{
	Vector3 savedPosition;

	public override void OnIsAbout ()
	{
		OnStateChanged ();
		savedPosition = gameObject.transform.position;
		gameObject.transform.positionTo ( 0.25f, new Vector3 ( 30, 0, 0 ), true ).loopsInfinitely ( GoLoopType.PingPong );
		// gameObject.transform.eularAnglesTo ( 0.5f, new Vector3 ( 0, 0, 360 ) );
	}

	public override void OnNoMoreAboutTo ()
	{
		OnStateChanged ();
		gameObject.transform.killTweening ();
		gameObject.transform.position = savedPosition;
		// gameObject.transform.eulerAngles = Vector3.zero;
	}

	public override void OnUp ()
	{
		OnStateChanged ();
	}

	public override void OnNonUp ()
	{
		OnStateChanged ();
	}

	public override void OnStateChanged ()
	{
		if ( !IsUp )
			gameObject.renderer.material.color = Color.gray;
		else
//			if ( ! IsAboutTo )
				gameObject.renderer.material.color = Color.white;
// 			else
// 				gameObject.renderer.material.color = Color.green;
	}
}
