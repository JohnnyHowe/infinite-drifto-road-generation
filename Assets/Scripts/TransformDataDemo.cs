using System.Collections;
using System.Collections.Generic;
using RoadGeneration;
using UnityEngine;

public class TransformDataDemo : MonoBehaviour
{
    [SerializeField] private Transform _reference;
    [SerializeField] private Transform _point;
    [SerializeField] private Transform[] _transformDisplayGlobal;

    void Update()
    {
        TransformData reference = TransformData.FromTransform(_reference);
        TransformData worldPointOriginal = TransformData.FromTransform(_point);
        TransformData localPoint = reference.InverseTransformPoint(worldPointOriginal);
        TransformData worldPointTransformed = reference.TransformPoint(localPoint);

        foreach (Transform display in _transformDisplayGlobal)
        {
            display.position = worldPointTransformed.Position;
            display.rotation = worldPointTransformed.Rotation;
            SetGlobalScale(display, worldPointTransformed.Scale);
        }
    }

    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
