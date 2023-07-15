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

        /// <summary>
        /// Get a COPY of this aligned such that it is aligned with the start of this at
        /// startPosition and with a rotation about the y axis of yAxisRotation (zero on 
        /// other axes)
        /// </summary>
        IRoadSection GetAlignedTo(Vector3 startPosition, float yAxisRotationStart);
    }
}