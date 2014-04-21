using UnityEngine;
using System.Collections;

public class GodricSword : BaseBehaviourIcon
{
	void Start ()
	{
		MinTimeBeforeAttack = 3;
		MaxTimeBeforeAttack = 5;
		TimeBeforeNextAction = Random.Range ( MinTimeBeforeAttack, MaxTimeBeforeAttack );
		TimeActionIsShown = 1;
	}

	public override void OnAction ()
	{
		ResetCoolDown ( 2 );

		Character targetPlayer = GameScreen.Instance.RandomPlayerCharacter;
		if ( targetPlayer != null )
		{
			Hit hit = new Hit ( Character, targetPlayer, 1 );
			hit.Apply ();
		}
	}
}
