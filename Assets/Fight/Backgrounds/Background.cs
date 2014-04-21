using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
	public GameObject RoofTop;
	public GameObject CentralPark;
	public GameObject Alley;

	void Start ()
	{
		if ( FightParams.Exists )
		{
			GameObject prefab = null;
			switch ( FightParams.Instance.arena )
			{
				case PlayerSingleton.Arena.Alley:
					prefab = Alley;
					break;

				case PlayerSingleton.Arena.CenterPark:
					prefab = CentralPark;
					break;

				case PlayerSingleton.Arena.RoofTop:
					prefab = RoofTop;
					break;
			}
			if ( prefab != null )
			{
				GameObject background = GameObject.Instantiate ( prefab ) as GameObject;
				if ( background != null )
					background.transform.position = new Vector3 ( 0, 0, 100 );
			}
		}
	}
}
