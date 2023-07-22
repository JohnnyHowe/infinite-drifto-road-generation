using UnityEngine;

namespace RoadGeneration
{
    public interface IRoadSection
    {
        RoadSectionShape GetShape();
        RoadSectionShape GetLocalShape();
        void AlignByStartPoint(RoadSectionShape.EndPoint newStartPoint);
        IRoadSection Clone();
    }
}