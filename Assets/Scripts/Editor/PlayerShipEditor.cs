using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerShip))]
public class PlayerShipEditor : Editor {

    [DrawGizmo(GizmoType.Selected)]
    static void DrawInterestingAreas(PlayerShip playerShip, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        playerShip.gameArea.DrawGizmo();
    }

}
