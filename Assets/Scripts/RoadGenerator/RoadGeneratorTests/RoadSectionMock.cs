using System;
using RoadGeneration;
using UnityEngine;

public class RoadSectionMock : IRoadSection
{
    public RoadSectionBounds Bounds;
    public Vector3 OriginalGlobalEndPosition;
    public Vector3 LocalStartPosition;
    public Quaternion OriginalGlobalEndRotation;
    public Quaternion LocalStartRotation;
    public Vector3 CurrentGlobalEndPosition;
    public Quaternion CurrentGlobalEndRotation;
    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    public RoadSectionBounds GetBounds()
    {
        return Bounds;
    }

    public Vector3 GetGlobalEndPosition()
    {
        return CurrentGlobalEndPosition;
    }

    public Quaternion GetGlobalEndRotation()
    {
        return CurrentGlobalEndRotation;
    }

    public Vector3 GetLocalStartPosition()
    {
        return LocalStartPosition;
    }

    public Quaternion GetLocalStartRotation()
    {
        return LocalStartRotation;
    }

    internal void SetPosition(Vector3 position)
    {
        _positionOffset = position;
        CurrentGlobalEndPosition = TransformPoint(OriginalGlobalEndPosition, _positionOffset, _rotationOffset, Vector3.one);
    }

    internal void SetRotation(Quaternion rotation)
    {
        _rotationOffset = rotation;
        CurrentGlobalEndRotation = OriginalGlobalEndRotation * _rotationOffset;
    }

    public static Vector3 TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Vector3 rotatedPoint = rotation * point;
        Vector3 transformedPoint = rotatedPoint + position;
        return transformedPoint;
    }
}