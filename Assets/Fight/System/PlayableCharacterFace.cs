using UnityEngine;
using System.Collections;

public class PlayableCharacterFace : CharacterFace
{
	new void Start ()
	{
		base.Start ();
	}

	new void OnDelete ()
	{
		base.OnDelete ();
	}

	void OnMouseDown ()
	{
		TargetedActionButton activeButton = GameScreen.Instance.ActiveActionButton;
		if ( activeButton != null )
			activeButton.OnHitTarget ( CharacterHud );
	}

	internal override void OnHitProduced ( Hit hit )
	{
		transform.positionTo ( 0.1f, new Vector3 ( 25, 0, 0 ), true ).loops ( 2, GoLoopType.PingPong );
	}
}
