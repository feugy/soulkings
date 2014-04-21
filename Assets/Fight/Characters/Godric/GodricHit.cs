using UnityEngine;
using System.Collections;

public class GodricHit : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 1;
		MaxTimeBeforeAttack = 2;
		TimeActionIsShown = 2;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 2.0f );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 5 );
			hit.Apply ();
		}
	}
}
