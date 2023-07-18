using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Other;

namespace RoadGeneration.Tests
{
    public class RoadSectionShapeTester
    {
        [Test]
        public void TestDoesOverlapWithFalseUsingSetBoundaryFromMeshNoOffset()
        {
            RoadSectionShape shape1 = new RoadSectionShape();
            shape1.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh());

            RoadSectionShape shape2 = new RoadSectionShape();
            shape2.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh(Vector3.right * 2));

            Assert.False(shape1.DoesOverlapWith(shape2));
        }

        [Test]
        public void TestGetCopyWithStartAlignedNoMovement()
        {
            RoadSectionShape shape = new RoadSectionShape();
            shape.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh());
            shape.Start = new RoadSectionShape.EndPoint(Vector3.back * .5f, Quaternion.Euler(0, 0, 1));
            shape.End = new RoadSectionShape.EndPoint(Vector3.forward * .5f, Quaternion.Euler(0, 0, 1));

            RoadSectionShape.EndPoint targetStart = new RoadSectionShape.EndPoint(Vector3.back * .5f, Quaternion.Euler(0, 0, 1));

            RoadSectionShape translatedShape = shape.GetCopyWithStartAlignedTo(targetStart);
            Assert.True(shape.Equals(translatedShape));
        }

        [Test]
        public void TestGetCopyWithStartAlignedMovedRight()
        {
            RoadSectionShape shape = new RoadSectionShape();
            shape.SetBoundaryFromMesh(MeshGeneratorUtility.GenerateUnitCubeMesh());
            shape.Start = new RoadSectionShape.EndPoint(Vector3.back * .5f, Quaternion.Euler(0, 0, 1));
            shape.End = new RoadSectionShape.EndPoint(Vector3.forward * .5f, Quaternion.Euler(0, 0, 1));

            RoadSectionShape.EndPoint targetStart = new RoadSectionShape.EndPoint(new Vector3(1, 0, 0), Quaternion.Euler(0, 0, 1));

            RoadSectionShape translatedShape = shape.GetCopyWithStartAlignedTo(targetStart);
            Assert.False(shape.Equals(translatedShape));
        }
    }
}