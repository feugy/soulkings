using UnityEngine;
using System.Collections.Generic;

public enum GetChildOption
{
	ObjectOnly,
	FullHierarchy
}

public enum GetBoundsOption
{
	ObjectOnly,
	FullHierarchy
}

public static class GameObjectUtils
{
	public static void BroadcastMessageToScene ( string messageName, System.Object messageParameter = null )
	{
		GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType ( typeof ( GameObject ) );
		foreach ( GameObject go in gos )
		{
			if ( go && go.transform.parent == null )
			{
				go.gameObject.BroadcastMessage ( messageName, messageParameter, SendMessageOptions.DontRequireReceiver );
			}
		}
	}
}

public static class GameObjectMethods
{
	public static void MoveChildTo ( this GameObject from, GameObject to )
	{
		List<Transform> childs = new List<Transform> ();
		for ( int i = 0; i < from.transform.childCount; i++ )
			childs.Add ( from.transform.GetChild ( i ) );

		foreach ( Transform child in childs )
			child.parent = to.transform;
	}

	public static T GetOrCreateComponent<T> ( this GameObject gameObject ) where T : Component
	{
		T component = gameObject.GetComponent<T> ();
		if ( component == null )
			component = gameObject.AddComponent<T> ();

		return component;
	}

	public static GameObject GetChildUnderPoint ( this GameObject gameObject, Vector2 point )
	{
		int count = gameObject.transform.childCount;
		for ( int i = 0; i < count; i++ )
		{
			GameObject child = gameObject.transform.GetChild ( i ).gameObject;
			BoxCollider2D boxCollider2D = child.GetComponent<BoxCollider2D> ();
			if ( boxCollider2D != null )
			{
				Rect r = RectUtil.FromCollider ( boxCollider2D );
				// Debug.Log ( "Rect : " + r.ToString() + " - Point : " + point.ToString() );
				if ( r.Contains ( point ) )
					return child;
			}
		}

		return null;
	}

	public static bool IsUnderPoint ( this GameObject gameObject, Vector2 point )
	{
		BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D> ();
		if ( boxCollider2D != null )
		{
			Rect r = RectUtil.FromCollider ( boxCollider2D );
			return r.Contains ( point );
		}

		CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D> ();
		if ( circleCollider2D != null )
		{
			Vector2 dist = ( (Vector2)circleCollider2D.transform.position + circleCollider2D.center ) - point;
			return ( dist.magnitude < circleCollider2D.radius );
		}

		Debug.LogWarning ( "IsUnderPoint : No collider on object " + gameObject.name );
		return false;
	}

	public static bool IsUnderMouse ( this GameObject gameObject )
	{
		return gameObject.IsUnderPoint ( Camera.main.ScreenToWorldPoint ( Input.mousePosition ) );
	}

	public static Bounds GetBounds ( this GameObject gameObject, GetBoundsOption option = GetBoundsOption.ObjectOnly )
	{
		if ( option == GetBoundsOption.ObjectOnly )
		{
			if ( gameObject.renderer != null )
				return gameObject.renderer.bounds;
			else
				return new Bounds ();
		}
		else
		{
			Bounds b = gameObject.GetChildBounds ();
			if ( gameObject.renderer != null )
				b.Encapsulate ( gameObject.renderer.bounds );

			return b;
		}
	}

	public static Bounds GetChildBounds ( this GameObject gameObject )
	{
		Bounds bounds = new Bounds ();
		if ( gameObject.transform.childCount > 0 )
		foreach ( Transform t in gameObject.transform )
		{
			Bounds childBound = t.gameObject.GetBounds ( GetBoundsOption.FullHierarchy );
			if ( childBound.size != Vector3.zero )
				if ( bounds.size == Vector3.zero )
					bounds = childBound;
				else
					bounds.Encapsulate ( childBound );
		}

		if ( bounds.size == Vector3.zero )
			return new Bounds ( gameObject.transform.position, Vector3.zero );

		return bounds;
	}

// 	public static Vector3 GetChildBounds ( this GameObject gameObject )
// 	{
// 		Vector3 max = new Vector3 ( float.MinValue, float.MinValue, float.MinValue );
// 		Vector3 min = new Vector3 ( float.MaxValue, float.MaxValue, float.MaxValue );
// 
// 		int count = gameObject.transform.childCount;
// 		for ( int i = 0; i < count; i++ )
// 		{
// 			GameObject child = gameObject.transform.GetChild ( i ).gameObject;
// 			BoxCollider2D boxCollider2D = child.GetComponent<BoxCollider2D> ();
// 			if ( boxCollider2D != null )
// 			{
// 				Vector3 position = child.transform.localPosition;
// 				float left = position.x - ( boxCollider2D.size.x / 2 ) + boxCollider2D.center.x;
// 				float right = position.x + ( boxCollider2D.size.x / 2 ) + boxCollider2D.center.x;
// 				float bottom = position.y - ( boxCollider2D.size.y / 2 ) + boxCollider2D.center.y;
// 				float top = position.y + ( boxCollider2D.size.y / 2 ) + boxCollider2D.center.y;
// 
// 				min.x = System.Math.Min ( min.x, left );
// 				max.x = System.Math.Max ( max.x, right );
// 				min.y = System.Math.Min ( min.y, bottom );
// 				max.y = System.Math.Max ( max.y, top );
// 			}
// 		}
// 
// 		return max - min;
// 	}

	public static List<GameObject> GetChilds ( this GameObject gameObject, GetChildOption option = GetChildOption.ObjectOnly )
	{
		List<GameObject> childs = new List<GameObject> ();
		int count = gameObject.transform.childCount;
		for ( int i = 0; i < count; i++ )
		{
			GameObject child = gameObject.transform.GetChild ( i ).gameObject;
			childs.Add ( child );
			if ( option == GetChildOption.FullHierarchy )
				childs.AddRange ( child.GetChilds ( option ) );
		}
		return childs;
	}

	public static GameObject FindChildByName ( this GameObject gameObject, string name )
	{
		int count = gameObject.transform.childCount;
		for ( int i = 0; i < count; i++ )
		{
			GameObject child = gameObject.transform.GetChild ( i ).gameObject;
			if ( child.name.Equals ( name ) )
				return child;
		}
		return null;
	}

	public static void SetPosition ( this GameObject gameObject, float x, float y )
	{
		gameObject.transform.position = new Vector3 ( x, y, gameObject.transform.position.z );
	}

	public static GameObject GetRootParent ( this GameObject self )
	{
		Transform parent = self.transform.parent;
		
		if ( parent == null )
			return null;
	
		while ( parent.transform.parent != null )
			parent = parent.transform.parent;

		return parent.gameObject;
	}

	public static void SetAlpha ( this GameObject self, float alpha, bool recursiveOnChildren = false )
	{
		if ( self.renderer != null )
			if ( self.renderer.material != null )
				self.renderer.material.SetAlpha ( alpha );

		if ( recursiveOnChildren )
			for ( int i = 0; i < self.transform.childCount; i++ )
				self.transform.GetChild ( i ).gameObject.SetAlpha ( alpha, recursiveOnChildren );
	}

	public static void alphaTo ( this GameObject self, float duration, float endValue )
	{
		if ( self.renderer != null )
			if ( self.renderer.material != null )
				self.renderer.material.alphaTo ( duration, endValue );

		for ( int i = 0; i < self.transform.childCount; i++ )
			self.transform.GetChild ( i ).gameObject.alphaTo ( duration, endValue );
	}
}


