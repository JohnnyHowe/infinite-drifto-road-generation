using System.Collections.Generic;
using System.Linq;
using RoadGeneration;
using UnityEngine;
using Other;
using System;

namespace RoadGeneration.Tests
{
    public class MockRoadGenerator
    {
        public static List<IRoadSection> AlignMockSections(params IRoadSection[] toAlign)
        {
            return AlignMockSections(new List<IRoadSection>(toAlign));
        }

        public static List<IRoadSection> AlignMockSections(List<IRoadSection> toAlign)
        {
            List<IRoadSection> aligned = new List<IRoadSection>();
            // TransformData nextStartPoint = new TransformData(Vector3.zero, Quaternion.Euler(0, 0, 1));
            throw new NotImplementedException();
            // foreach (IRoadSection section in toAlign)
            // {
            //     section.AlignByStartPoint(nextStartPoint);
            //     nextStartPoint = section.GetShape().End;
            //     aligned.Add(section);
            // }
            return aligned;
        }

        public static IRoadSection CreateBasicStraight()
        {
            throw new NotImplementedException();
            // MockRoadSection section = new MockRoadSection();
            // section.LocalShape = new RoadSectionShape();
            // // section.LocalShape.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh());
            // throw new NotImplementedException();
            // section.LocalShape.End = new TransformData(new Vector3(0, 0, -.5f), Quaternion.Euler(0, 0, 1));
            // section.LocalShape.Start = new TransformData(new Vector3(0, 0, .5f), Quaternion.Euler(0, 0, 1));
            // return section;
        }
    }
}