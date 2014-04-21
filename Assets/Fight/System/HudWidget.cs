using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HudWidget : MonoBehaviour
{
	private CharacterHud characterHud;
	internal CharacterHud CharacterHud
	{
		get
		{
			if ( characterHud == null )
			{
				GameObject parent = transform.parent.gameObject;
				if ( parent == null )
					Debug.Log ( "Stop" );

				characterHud = parent.GetComponent<CharacterHud> ();

				if ( characterHud == null )
				{
					transform.localEularAnglesTo ( 1, new Vector3 ( 0, 0, 360 ), true ).eases ( GoEaseType.Linear ).loopsInfinitely ();
					Debug.Log ( "HudWidget " + name + " don' have a parent CharacterHud" );
				}
			}
			
			return characterHud;
		}
	}

	private Character character;
	internal Character Character
	{
		get
		{
			if ( character == null )
				character = CharacterHud.Character;

			return character;
		}
	}
}


public class Character
{
	internal string Name { get; set; }

	internal delegate void RageChanged ();
	internal event RageChanged OnRageChanged;

	internal delegate void ManaChanged ();
	internal event ManaChanged OnManaChanged;

	internal delegate void MaxManaChanged ();
	internal event MaxManaChanged OnMaxManaChanged;

	internal delegate void HPChanged ();
	internal event HPChanged OnHPChanged;

	internal delegate void MaxHPChanged ();
	internal event MaxHPChanged OnMaxHPChanged;

	internal delegate void ReduceHit ( Hit hit );
	internal event ReduceHit OnReduceHit;

	internal delegate void HitReceived ( Hit hit );
	internal event HitReceived OnHitReceived;

	internal delegate void HitProduced ( Hit hit );
	internal event HitProduced OnHitProduced;

	internal delegate void HealReceived ( Heal heal );
	internal event HealReceived OnHealReceived;

	internal delegate void HealProduced ( Heal heal );
	internal event HealProduced OnHealProduced;

	internal delegate void StunReceived ( Stun stun );
	internal event StunReceived OnStunReceived;

	internal delegate void StunProduced ( Stun stun );
	internal event StunProduced OnStunProduced;

	internal delegate void EnterStun ();
	internal event EnterStun OnEnterStun;

	internal delegate void LeaveStun ();
	internal event LeaveStun OnLeaveStun;

	internal delegate void DotProduced ( DamageOverTime dot );
	internal event DotProduced OnDotProduced;

	internal delegate void DotReceived ( DamageOverTime dot );
	internal event DotReceived OnDotReceived;

	internal delegate void HotProduced ( HealOverTime hot );
	internal event HotProduced OnHotProduced;

	internal delegate void HotReceived ( HealOverTime hot );
	internal event HotReceived OnHotReceived;

	internal float DamageFactor;
	internal float ArmorFactor;

	private float maxHp;
	internal float MaxHP
	{
		get { return maxHp; }
		set
		{
			maxHp = System.Math.Max ( 0, value );
			if ( OnMaxHPChanged != null )
				OnMaxHPChanged ();
		}
	}

	private float hp;
	internal float HP
	{
		get { return hp; }
		set
		{
			hp = System.Math.Max ( 0, value );
			if ( OnHPChanged != null )
				OnHPChanged ();
		}
	}

	internal float MaxRage
	{ get { return 100; } }

	private float rage;
	internal float Rage
	{
		get { return rage; }
		set
		{
			rage = System.Math.Max ( 0, System.Math.Min ( MaxRage, value ) );
			if ( OnRageChanged != null )
				OnRageChanged ();
		}
	}

	private float maxMana;
	internal float MaxMana
	{
		get { return maxMana; }
		set
		{
			maxMana = System.Math.Max ( 0, value );
			if ( OnMaxManaChanged != null )
				OnMaxManaChanged ();
		}
	}

	private float mana;
	internal float Mana
	{
		get { return mana; }
		set
		{
			mana = System.Math.Max ( 0, System.Math.Min ( 100, value ) );
			if ( OnManaChanged != null )
				OnManaChanged ();
		}
	}

	internal bool IsAlive { get { return HP > 0; } }

	internal float StunTime { get; set; }

	internal bool IsStunned { get { return StunTime > 0; } }

	internal bool IsPlaying { get { return IsAlive && ! IsStunned; } }

	internal float CriticalChance { get; set; }

	internal bool IsDoingACritical
	{
		get
		{
			return ( Random.Range ( 0.0f, 1.0f ) < CriticalChance );
		}
	}

	List<DamageOverTime> Dots = new List<DamageOverTime> ();
	List<HealOverTime> Hots = new List<HealOverTime> ();

	internal void ReceiveHit ( Hit hit )
	{
		if ( HP > 0 )
		{
			if ( OnReduceHit != null )
				OnReduceHit ( hit );

			if ( hit.Points > 0 )
			{
				HP = System.Math.Max ( 0, HP - hit.DamagePoints );

				if ( OnHitReceived != null )
					OnHitReceived ( hit );
			}
		}
	}

	internal void ProduceHit ( Hit hit )
	{
		if ( OnHitProduced != null )
			OnHitProduced ( hit );
	}

	internal void ReceiveHeal ( Heal heal )
	{
		if ( HP > 0 )
		{
			if ( heal.Points > 0 )
			{
				HP = HP + heal.Points;

				if ( OnHealReceived != null )
					OnHealReceived ( heal );
			}
		}
	}

	internal void ProduceHeal ( Heal heal )
	{
		if ( OnHealProduced != null )
			OnHealProduced ( heal );
	}

	internal void ReceiveStun ( Stun stun )
	{
		if ( StunTime <= 0 )
			if ( OnEnterStun != null )
				OnEnterStun ();

		StunTime += stun.Time;
		
		if ( OnStunReceived != null )
			OnStunReceived ( stun );
	}

	internal void ProduceStun ( Stun stun )
	{
		if ( OnStunProduced != null )
			OnStunProduced ( stun );
	}

	internal void Update ( float elapsedTime )
	{
		if ( StunTime > 0 )
		{
			StunTime -= elapsedTime;
			if ( StunTime <= 0 )
				if ( OnLeaveStun != null )
					OnLeaveStun ();
		}

		int count = Dots.Count;
		for ( int i = count - 1; i >= 0; i-- )
		{
			DamageOverTime dot = Dots[i];
			dot.Update ( elapsedTime );
			if ( dot.RemainingTime <= 0 )
				Dots.RemoveAt ( i );
		}
	}

	internal void ProduceDot ( DamageOverTime dot )
	{
		if ( OnDotProduced != null )
			OnDotProduced ( dot );
	}

	internal void ReceiveDot ( DamageOverTime dot )
	{
		Dots.Add ( dot );
		if ( OnDotReceived != null )
			OnDotReceived ( dot );
	}

	internal void ProduceHot ( HealOverTime hot )
	{
		if ( OnHotProduced != null )
			OnHotProduced ( hot );
	}

	internal void ReceiveHot ( HealOverTime hot )
	{
		Hots.Add ( hot );
		if ( OnHotReceived != null )
			OnHotReceived ( hot );
	}
}


internal class Hit
{
	internal Character Target;
	internal Character Source;

	internal bool IsCritical { get; set; }

	internal float Points;
	internal float DamagePoints
	{
		get
		{
			float points = ( Points / Target.ArmorFactor ) * Source.DamageFactor;

			return IsCritical ? Points * 1.5f : Points;
		}
	}

	internal Hit ( Character source, Character target, float points )
	{
		this.Source = source;
		this.Target = target;
		this.Points = points;
		IsCritical = source.IsDoingACritical;
	}

	internal void Apply()
	{
		Source.ProduceHit ( this );
		Target.ReceiveHit ( this );
	}
}

internal class Heal
{
	internal Character Source;
	internal Character Target;

	internal float Points;

	internal Heal ( Character source, Character target, float points )
	{
		this.Source = source;
		this.Target = target;
		this.Points = points;
	}

	internal void Apply()
	{
		Source.ProduceHeal ( this );
		Target.ReceiveHeal ( this );
	}
}

internal class Stun
{
	internal Character Source;
	internal Character Target;

	internal float Time;

	internal Stun ( Character source, Character target, float time )
	{
		this.Source = source;
		this.Target = target;
		this.Time = time;
	}

	internal void Apply ()
	{
		Source.ProduceStun ( this );
		Target.ReceiveStun ( this );
	}

}



internal class DamageOverTime
{
	internal Character Target;
	internal Character Source;

	internal float DamagePoints;
	private float Duration;
	internal float RemainingTime;

	internal DamageOverTime ( Character source, Character target, float points, float duration )
	{
		this.Source = source;
		this.Target = target;
		this.DamagePoints = points;
		this.Duration = duration;
		this.RemainingTime = duration;
	}

	internal void Start()
	{
		Source.ProduceDot ( this );
		Target.ReceiveDot ( this );
	}

	internal void Update ( float elapsedTime )
	{
		if ( Target.IsAlive )
		{
			float damageTime = System.Math.Min ( RemainingTime, elapsedTime );
			RemainingTime -= damageTime;
			Target.HP -= ( damageTime * DamagePoints ) / this.Duration;
		}
		else
			RemainingTime = 0;
	}
}


internal class HealOverTime
{
	internal Character Target;
	internal Character Source;

	internal float HealPoints;
	private float Duration;
	internal float RemainingTime;

	internal HealOverTime ( Character source, Character target, float points, float duration )
	{
		this.Source = source;
		this.Target = target;
		this.HealPoints = points;
		this.Duration = duration;
		this.RemainingTime = duration;
	}

	internal void Start ()
	{
		Source.ProduceHot ( this );
		Target.ReceiveHot ( this );
	}

	internal void Update ( float elapsedTime )
	{
		if ( Target.IsAlive )
		{
			float healTime = System.Math.Min ( RemainingTime, elapsedTime );
			RemainingTime -= healTime;
			Target.HP += ( healTime * HealPoints ) / this.Duration;
		}
		else
			RemainingTime = 0;
	}
}