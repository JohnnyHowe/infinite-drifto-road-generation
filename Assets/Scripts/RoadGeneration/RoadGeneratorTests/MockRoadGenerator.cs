using System.Collections.Generic;
using System.Linq;
using RoadGeneration;
using UnityEngine;
using Other;

namespace RoadGeneration.Tests
{
    public class MockRoadGenerator
    {
        public static List<IRoadSection> AlignMockSections(List<IRoadSection> toAlign)
        {
            List<IRoadSection> aligned = new List<IRoadSection>();
            RoadSectionShape.EndPoint nextStartPoint = new RoadSectionShape.EndPoint(Vector3.zero, Quaternion.Euler(0, 0, 1));
            foreach (IRoadSection section in toAlign)
            {
                section.AlignByStartPoint(nextStartPoint);
                nextStartPoint = section.GetShape().End;
                aligned.Add(section);
            }
            return aligned;
        }

        public static IRoadSection CreateBasicStraightMock()
        {
            MockRoadSection section = new MockRoadSection();
            section.LocalShape = new RoadSectionShape();
            section.LocalShape.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh());
            section.LocalShape.End = new RoadSectionShape.EndPoint(new Vector3(0, 0, -.5f), Quaternion.Euler(0, 0, 1));
            section.LocalShape.Start = new RoadSectionShape.EndPoint(new Vector3(0, 0, .5f), Quaternion.Euler(0, 0, 1));
            return section;
        }
    }
}