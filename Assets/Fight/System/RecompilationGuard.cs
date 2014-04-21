using UnityEngine;
using System.Collections;

public class RecompilationGuard : MonoBehaviour
{
	private static bool startMethodCalled;

	RecompilationGuard ()
	{
	}

	void Start ()
	{
		startMethodCalled = true;
	}

	void Update ()
	{
		if ( ! startMethodCalled )
		{
			startMethodCalled = true;

			Debug.Log ( "Compilation detected, restarting..." );
			
			GameObjectUtils.BroadcastMessageToScene ( "Start" );

			GameObjectUtils.BroadcastMessageToScene ( "OnCompiled" );
		}
	}
}
