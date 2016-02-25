using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyShip))]
public class EnemyShipEditor : Editor {

    [DrawGizmo(GizmoType.Selected)]
	static void DrawInterestingAreas(EnemyShip enemyShip, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        enemyShip.activeArea.DrawGizmo();
        Gizmos.color = Color.magenta;
        enemyShip.shootingArea.DrawGizmo();
    }

}
