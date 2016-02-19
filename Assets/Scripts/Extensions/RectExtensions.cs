using UnityEngine;
using System.Collections;

public static class RectExtensions
{
    /// <summary>
    /// This method clamps a Vector3 X and Y coordinates inside the receiver Rect. Z position is not modified.
    /// </summary>
    /// <param name="rect">The receiver Rect.</param>
    /// <param name="position">The position to clamp inside the Rect.</param>
    /// <returns>The clamped Vector3 position.</returns>
    public static Vector3 ClampPosition(this Rect rect, Vector3 position)
    {
        Vector3 fixedPosition = position;
        fixedPosition.x = Mathf.Clamp(fixedPosition.x, rect.xMin, rect.xMax);
        fixedPosition.y = Mathf.Clamp(fixedPosition.y, rect.yMin, rect.yMax);
        return fixedPosition;
    }

    /// <summary>
    /// This method clamps a Vector2 X and Y coordinates inside the receiver Rect.
    /// </summary>
    /// <param name="rect">The receiver Rect.</param>
    /// <param name="position">The position to clamp inside the Rect.</param>
    /// <returns>The clamped Vector2 position.</returns>
    public static Vector2 ClampPosition(this Rect rect, Vector2 position)
    {
        Vector2 fixedPosition = position;
        fixedPosition.x = Mathf.Clamp(fixedPosition.x, rect.xMin, rect.xMax);
        fixedPosition.y = Mathf.Clamp(fixedPosition.y, rect.yMin, rect.yMax);
        return fixedPosition;
    }
}


