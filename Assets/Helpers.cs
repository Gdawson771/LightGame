using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    public static Vector2 AngleToForce(float angle, float speed)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * speed;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * speed;
        return new Vector2(xcomponent, ycomponent);
    }

    public static Vector2 GetSpritePivot(Sprite sprite)
    {
        Bounds bounds = sprite.bounds;
        var pivotX = - bounds.center.x / bounds.extents.x / 2 + 0.5f;
        var pivotY = - bounds.center.y / bounds.extents.y / 2 + 0.5f;

        return new Vector2(pivotX, pivotY);
    }

    /**
        AngleModulus - Will correctly add or subtract any two Angles to be between 190 & -180 degress
        @param angle - the angle to be aletered
        @param dAngle - the amount of degrees to change by
    **/
    public static float AngleModulus(float angle, float dAngle) {
        if(angle + dAngle <= 180 && angle + dAngle >= -180) return angle + dAngle;
        return Mathf.Sign(dAngle) == 1 ? -(360f - (angle + dAngle)) : 360f + (dAngle + angle);
    }
}
