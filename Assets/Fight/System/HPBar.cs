using UnityEngine;
using System.Collections;

public class HPBar : HudWidget
{
	private float initialSize;

	void Start ()
	{
		Character.OnHPChanged += OnHPChanged;
		
		Quad quad = GetComponent<Quad> ();
		initialSize = quad.Width;

		OnHPChanged ();
	}

	void OnHPChanged ()
	{
		Quad quad = GetComponent<Quad> ();
		quad.Width = (int)( initialSize * Character.HP / Character.MaxHP );
		quad.Recreate ();
	}
}
