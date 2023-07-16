using UnityEngine;

namespace RoadGeneration
{
    public interface IRoadSection
    {
        RoadSectionShape GetLocalShape();
    }
}