using UnityEngine;

namespace RoadGeneration
{
    public interface IRoadSection
    {
        RoadSectionShape GetShape();
        void AlignByStartPoint(TransformData newStartPoint);
        IRoadSection Clone();
    }
}