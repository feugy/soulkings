using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DPSMeter : MonoBehaviour
{
	private float elapsedTime;
	private float damageProduced;
	private float Dps
	{
		get { return damageProduced / elapsedTime; }
	}

	void Start ()
	{
		List<GameObject> children = gameObject.transform.parent.gameObject.GetChilds ( GetChildOption.FullHierarchy );
		foreach ( GameObject child in children )
		{
			CharacterHud hud = child.GetComponent<CharacterHud> ();
			if ( hud != null )
			{
				hud.Character.OnHitProduced += OnHitProduced;
				hud.Character.OnDotProduced += OnDotProduced;
			}
		}
	}

	void FixedUpdate ()
	{
		elapsedTime += Time.fixedDeltaTime;

		UpdateText ();
	}

	void OnDotProduced ( DamageOverTime dot )
	{
		damageProduced += dot.DamagePoints;
	}

	void OnHitProduced ( Hit hit )
	{
		damageProduced += hit.DamagePoints;
	}

	private void UpdateText ()
	{
		GetComponent<TextMesh> ().text = "DPS : " + Dps.ToString("0.00");
	}
}
