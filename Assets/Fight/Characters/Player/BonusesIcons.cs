using UnityEngine;
using System.Collections;

public class BonusesIcons : MonoBehaviour
{
	public GameObject Puppet;
	public GameObject Crux;
	public GameObject Radio;
	public GameObject DiscoBall;

	void Start ()
	{
		SetActivated ( Puppet, InventorySingleton.Instance.puppet, 0.25f );
		SetActivated ( Crux, InventorySingleton.Instance.crux, 0.5f );
		SetActivated ( Radio, InventorySingleton.Instance.radio, 0.75f );
		SetActivated ( DiscoBall, InventorySingleton.Instance.discoBall, 1.0f );
	}

	void SetActivated ( GameObject bonus, bool isActivated, float delay )
	{
		if ( isActivated )
			bonus.transform.scaleTo ( 0.5f, 0.60f ).delays ( delay ).loopsInfinitely ( GoLoopType.PingPong );
		else
			bonus.renderer.material.color = Color.gray;
	}
}
