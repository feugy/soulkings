using UnityEngine;
using System.Collections;

public abstract class CharacterFace : HudWidget
{
	internal void Start ()
	{
		Character.OnHitReceived += OnHitReceived;
		Character.OnHitProduced += OnHitProduced;
		Character.OnHealReceived += OnHealReceived;
		Character.OnEnterStun += OnEnterStun;
		Character.OnLeaveStun += OnLeaveStun;
	}

	internal void OnDelete ()
	{
		Character.OnHitReceived -= OnHitReceived;
		Character.OnHitProduced -= OnHitProduced;
		Character.OnHealReceived -= OnHealReceived;
	}
	
	void OnHitReceived ( Hit hit )
	{
		GameObject effect = GameObject.Instantiate ( GameResources.Instance.Prefabs.HitEffect ) as GameObject;
		effect.transform.position = transform.position + new Vector3 ( 0, 0, -5 );
		effect.GetComponent<HitEffect> ().Play ( hit );
	}

	void OnHealReceived ( Heal heal )
	{
		GameObject effect = GameObject.Instantiate ( GameResources.Instance.Prefabs.HealEffect ) as GameObject;
		effect.transform.position = transform.position + new Vector3 ( 0, 0, -5 );
		effect.GetComponent<HealEffect> ().Play ( heal );
	}

	void OnEnterStun ()
	{
		gameObject.renderer.material.color = Color.gray;
	}

	void OnLeaveStun ()
	{
		gameObject.renderer.material.color = Color.white;
	}

	abstract internal void OnHitProduced ( Hit hit );
}