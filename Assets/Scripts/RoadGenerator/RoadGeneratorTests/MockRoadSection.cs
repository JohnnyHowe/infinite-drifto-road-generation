using RoadGeneration;
using UnityEngine;

public class MockRoadSection : IRoadSection
{
    public RoadSectionBounds LocalBounds;
    public Vector3 LocalStartPosition;
    public Vector3 LocalEndPosition;
    public float LocalStartYRotation;
    public float LocalEndYRotation;
    public Vector3 GlobalPosition;
    public float GlobalRotation;

    public RoadSectionBounds GetGlobalBounds()
    {
        return LocalBounds.GetCopy(GlobalPosition, Quaternion.Euler(0, GlobalRotation, 0), Vector3.one);
    }

    public Vector3 GetGlobalEndPosition()
    {
        Vector3 position = GlobalPosition + LocalEndPosition;
        return TransformPoint(LocalEndPosition, GlobalPosition, Quaternion.Euler(0, GlobalRotation, 0), Vector3.one);
    }

    public Quaternion GetGlobalEndRotation()
    {
        return Quaternion.Euler(0, GlobalRotation + LocalEndYRotation, 0);
    }

    public Vector3 GetLocalStartPosition()
    {
        return LocalStartPosition;
    }

    public Quaternion GetLocalStartRotation()
    {
        return Quaternion.Euler(0, LocalStartYRotation, 0);
    }

    public IRoadSection GetAlignedTo(Vector3 startPosition, float yAxisRotationStart)
    {
        MockRoadSection newSection = new MockRoadSection();

        float rotation = yAxisRotationStart - GetLocalStartRotation().eulerAngles.y;
        Vector3 position = startPosition - GetLocalStartPosition();

        newSection.GlobalPosition = position;
        newSection.GlobalRotation = rotation;
        newSection.LocalBounds = LocalBounds;
        newSection.LocalStartPosition = LocalStartPosition;
        newSection.LocalEndPosition = LocalEndPosition;
        newSection.LocalStartYRotation = LocalStartYRotation;
        newSection.LocalEndYRotation = LocalEndYRotation;

        return newSection;
    }

    public static Vector3 TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Vector3 scaledPoint = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);
        Vector3 rotatedPoint = rotation * scaledPoint;
        Vector3 transformedPoint = rotatedPoint + position;
        return transformedPoint;
    }

    public RoadSectionShape GetLocalShape()
    {
        throw new System.NotImplementedException();
    }
}