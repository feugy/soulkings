using UnityEngine;
using System.Collections.Generic;

static class GoTweenMethods
{
	internal static GoTween delays ( this GoTween tween, float delay )
	{
		tween.delay = delay;
		return tween;
	}

	internal static GoTween loops ( this GoTween tween, int count )
	{
		tween.loopCount = count;
		return tween;
	}

	internal static GoTween loops ( this GoTween tween, int count, GoLoopType loopType )
	{
		tween.loopCount = count;
		tween.loopType = loopType;
		return tween;
	}

	internal static GoTween loopsInfinitely ( this GoTween tween )
	{
		tween.loopCount = -1;
		return tween;
	}

	internal static GoTween loopsInfinitely ( this GoTween tween, GoLoopType loopType )
	{
		tween.loopCount = -1;
		tween.loopType = loopType;
		return tween;
	}

	internal static GoTween eases ( this GoTween tween, GoEaseType ease )
	{
		tween.easeType = ease;
		return tween;
	}

	internal static void killTweening ( this Transform transform )
	{
		List<GoTween> tweens = Go.tweensWithTarget ( transform );
		foreach ( GoTween tween in tweens )
		{
			tween.pause ();
			tween.destroy ();
		}
	}

	internal static void killTweening ( this Material material )
	{
		List<GoTween> tweens = Go.tweensWithTarget ( material );
		foreach ( GoTween tween in tweens )
		{
			tween.pause ();
			tween.destroy ();
		}
	}

	internal static GoTweenChain animateJellyInfinite ( this Transform transform, float time, float value, GoEaseType ease = GoEaseType.QuadInOut )
	{
		GoTweenChain chain = new GoTweenChain ();

		Vector3 scaleAtStart = transform.localScale;
		Vector3 target = new Vector3 ( scaleAtStart.x - value, scaleAtStart.y + value, scaleAtStart.z );
		transform.localScale = target;

		target = new Vector3 ( scaleAtStart.x + value, scaleAtStart.y - value, scaleAtStart.z );
		chain.append ( transform.scaleTo ( time, target ).eases ( ease ) );

		target = new Vector3 ( scaleAtStart.x - value, scaleAtStart.y + value, scaleAtStart.z );
		chain.append ( transform.scaleTo ( time, target ).eases ( ease ) );

		chain.loopCount = -1;
		chain.play ();

		return chain;
	}

	internal static GoTweenChain animateJelly ( this Transform transform, float time, float value, int repeatCount = 1, GoEaseType ease = GoEaseType.QuadInOut )
	{
		Vector3 scaleAtStart = transform.localScale;

		GoTweenChain chain = new GoTweenChain ();
		for ( int i = repeatCount; i > 0; i-- )
		{
			float factor = ( (float)i ) / repeatCount;

			Vector3 target = new Vector3 ( scaleAtStart.x + ( value * factor ), scaleAtStart.y - ( value * factor ), scaleAtStart.z );
			chain.append ( transform.scaleTo ( time * factor, target ).eases ( ease ) );

			target = new Vector3 ( scaleAtStart.x - ( value * factor ), scaleAtStart.y + ( value * factor ), scaleAtStart.z );
			chain.append ( transform.scaleTo ( time * factor, target ).eases ( ease ) );
		}

		chain.append ( transform.scaleTo ( time / repeatCount, scaleAtStart ).eases ( ease ) );
		chain.play ();

		return chain;
	}

	internal static GoTween animateBounce ( this Transform transform, float time, float dx, float dy )
	{
		GoTween tween = transform.positionTo ( time, new Vector3 ( dx, dy ), true );
		tween.loopType = GoLoopType.PingPong;
		tween.loops ( 2 );
		return tween;
	}

	public static GoTween alphaTo ( this Material self, float duration, float endValue, string colorName = "_Color" )
	{
		Color endColor = self.color;
		endColor.a = endValue;
		return Go.to ( self, duration, new GoTweenConfig ().materialColor ( endColor, colorName ) );
	}

	public static void SetAlpha ( this Material self, float alpha )
	{
		Color c = self.color;
		c.a = alpha;
		// self.color = c;
		self.SetColor ( "_Color", c );
	}

	public static GoTween rotationTo ( this Transform self, float duration, Vector3 endValue, bool isRelative = false )
	{
		return Go.to ( self, duration, new GoTweenConfig ().rotation ( endValue, isRelative ) );
	}
}