using UnityEngine;
using System.Collections;

public class HPLabel : HudWidget
{
	HPLabel ()
	{
	}

	void Start ()
	{
		Character.OnHPChanged += OnHPChanged;
		OnHPChanged ();
	}

	void Destroy ()
	{
		Character.OnHPChanged -= OnHPChanged;
	}

	void OnHPChanged ()
	{
		GetComponent<TextMesh> ().text = "HP : " + ( (int)Character.HP ).ToString ();
	}
}
