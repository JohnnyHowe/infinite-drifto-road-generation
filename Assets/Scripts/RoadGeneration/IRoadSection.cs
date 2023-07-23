using UnityEngine;

namespace RoadGeneration
{
    public interface IRoadSection
    {
        RoadSectionShape GetShape();
        RoadSectionShape GetLocalShape();   // TODO remove
        RoadSectionShape GetShapeRelativeToStart();
        void AlignByStartPoint(RoadSectionShape.EndPoint newStartPoint);
        IRoadSection Clone();
    }
}