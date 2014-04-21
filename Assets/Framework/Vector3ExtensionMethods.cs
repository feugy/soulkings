using UnityEngine;

static class Vector3ExtensionMethods
{
	public static Vector3 NormalizeValues ( this Vector3 self )
	{
		if ( ( self.x > self.y ) && ( self.x > self.z ) )
		{
			self.y /= self.x;
			self.z /= self.x;
			self.x = 1;
		}
		else if ( ( self.y > self.x ) && ( self.y > self.z ) )
		{
			self.x /= self.y;
			self.z /= self.y;
			self.y = 1;
		}
		else if ( ( self.z > self.x ) && ( self.z > self.y ) )
		{
			self.x /= self.z;
			self.y /= self.z;
			self.z = 1;
		}
		return self;
	}

	public static Vector3 EulerRotate ( this Vector3 self, float xAngle, float yAngle, float zAngle )
	{
		return Quaternion.Euler ( xAngle, yAngle, zAngle ) * self;
	}

	public static Vector3 AxisRotate ( this Vector3 self, float angle, Vector3 axis )
	{
		return Quaternion.AngleAxis ( angle, axis ) * self;
	}
}
