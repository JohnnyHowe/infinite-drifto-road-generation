using RoadGeneration;
using UnityEngine;

public class MockRoadSection : IRoadSection
{
    public RoadSectionBounds Bounds;
    public Vector3 LocalStartPosition;
    public Vector3 LocalEndPosition;
    public float LocalStartYRotation;
    public float LocalEndYRotation;
    public Vector3 GlobalPosition;
    public float GlobalRotation;

    public RoadSectionBounds GetBounds()
    {
        return Bounds;
    }

    public Vector3 GetGlobalEndPosition()
    {
        return GlobalPosition + LocalEndPosition;
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



        return newSection;
    }
}