using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LinearMovement))]
[CanEditMultipleObjects]
public class LinearMovementInspector : Editor
{

	private void OnSceneGUI()
	{
		LinearMovement line = target as LinearMovement;
		Transform handleTransform = line.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;
		Vector3 p0 = handleTransform.position;
		Transform parent_trans = line.transform.parent;
        Vector3 p1 = parent_trans == null ? handleTransform.localPosition + line.m_point_b : parent_trans.TransformPoint(handleTransform.localPosition + line.m_point_b);

		Handles.color = Color.white;
		Handles.DrawLine(p0, p1);
		EditorGUI.BeginChangeCheck();
		p1 = Handles.DoPositionHandle(p1, handleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(line, "Move Point");
			EditorUtility.SetDirty(line);
			line.m_point_b = (parent_trans == null ? p1 : parent_trans.InverseTransformPoint(p1)) - handleTransform.localPosition;
		}
	}
}