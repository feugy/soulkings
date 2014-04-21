using UnityEngine;
using System.Collections;


public class NonPlayableCharacterFrame : HudWidget
{
	internal void Start ()
	{
		Character.OnHitProduced += OnHitProduced;
	}

	internal void OnDelete ()
	{
		Character.OnHitProduced -= OnHitProduced;
	}

	void OnMouseDown ()
	{
		TargetedActionButton activeButton = GameScreen.Instance.ActiveActionButton;
		if ( activeButton != null )
			activeButton.OnHitTarget ( CharacterHud );
	}

	internal void OnHitProduced ( Hit hit )
	{
		float factor =  System.Math.Min ( 1, System.Math.Max ( 0.25f, hit.DamagePoints / 20 ) );

		transform.positionTo ( 0.25f, new Vector3 ( -25 * factor, 0, 0 ), true ).loops ( 2, GoLoopType.PingPong );
	}
}
