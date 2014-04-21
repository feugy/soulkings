using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static bool Exists
	{
		get
		{
			if ( instance != null )
				return true;
			else
				return ( GameObject.FindObjectOfType<T> () as T ) != null;
		}
	}

	private static T instance;
	public static T Instance
	{
		get
		{
			if ( instance == null )
			{
				instance = GameObject.FindObjectOfType<T> () as T;
				if ( instance == null )
				{
					SingletonManager singletonManager = GameObject.FindObjectOfType<SingletonManager> ();
					GameObject singletonManagerObject = ( singletonManager != null ) ? singletonManager.gameObject : null;
					if ( singletonManager == null )
					{
						singletonManagerObject = new GameObject ();
						singletonManagerObject.name = "Singletons";
						GameObject.DontDestroyOnLoad ( singletonManagerObject );
						singletonManager = singletonManagerObject.AddComponent<SingletonManager> ();
					}
					instance = singletonManagerObject.AddComponent<T> ();
				}
			}
			return instance;
		}
	}
}