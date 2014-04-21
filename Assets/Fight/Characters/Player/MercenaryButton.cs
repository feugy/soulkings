using UnityEngine;
using System.Collections;

class MercenaryButton : ImmediateActionButton
{
	public override void OnAction ()
	{
		switch ( FightParams.Instance.selected )
		{
			case PlayerSingleton.Mercenary.PamGrier:
				{
					HealOverTime hot = new HealOverTime ( Character, Character, 50, 5 );
					hot.Start ();

					if ( GameResources.Instance.SoundBank.mercenary_girl != null )
						AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.mercenary_girl, Vector3.zero );
				}
				break;

			case PlayerSingleton.Mercenary.Harry:
				{
					Heal heal = new Heal ( Character, Character, 25 );
					heal.Apply ();

					if ( GameResources.Instance.SoundBank.mercenary_cop != null )
						AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.mercenary_cop, Vector3.zero );
				}
				break;

			case PlayerSingleton.Mercenary.Callahan:
				{
					Heal heal = new Heal ( Character, Character, 25 );
					heal.Apply ();

					if ( GameResources.Instance.SoundBank.mercenary_priest != null )
						AudioSource.PlayClipAtPoint ( GameResources.Instance.SoundBank.mercenary_priest, Vector3.zero );
				}
				break;
		}

		ResetCoolDown ( 10 );
	}
}
