using UnityEngine;

namespace RoadGeneration
{
    public interface IRoadSection
    {
        RoadSectionBounds GetBounds();
        Vector3 GetGlobalEndPosition();
        Vector3 GetLocalStartPosition();
        Quaternion GetGlobalEndRotation();
        Quaternion GetLocalStartRotation();
    }
}