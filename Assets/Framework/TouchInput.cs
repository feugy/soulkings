using UnityEngine;
using System.Collections;

class TouchInput
{
	internal static bool isJustDown;
	internal static bool isJustDragged;
	internal static bool isJustUp;

	internal static bool isDown;
	internal static bool isUp;
	internal static bool isMoved;
	internal static bool isDragging;

	internal static Vector3 previousPosition;
	internal static Vector3 position;
	internal static Vector3 dragVector;

	internal static void Update ()
	{
		previousPosition = position;
		position = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		isDown = Input.GetMouseButton ( 0 );
		isUp = !isDown;
		isMoved = ( previousPosition != position );
		isJustDown = Input.GetMouseButtonDown ( 0 );
		isJustUp = Input.GetMouseButtonUp ( 0 );

		isJustDragged = false;
		if ( isDown )
		{
			isJustDragged = isMoved;
			isDragging |= isMoved;
			dragVector.x += position.x - previousPosition.x;
			dragVector.y += position.y - previousPosition.y;
		}
		else
		{
			isDragging = false;
			dragVector = Vector2.zero;
		}
	}
}