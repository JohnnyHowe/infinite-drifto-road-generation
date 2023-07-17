using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Other.Tests
{
    public class ConvexBoundaryBlackBoxTester
    {
        private const float VECTOR_DUPLICATE_MIN_DISTANCE = 0.001f;

        [Test]
        public void DoesOverlapWithAndFromMeshUnitCubeFalse()
        {
            Mesh unitCube1 = MeshGeneratorUtility.GenerateUnitCubeMesh();
            Mesh unitCube2 = MeshGeneratorUtility.GenerateUnitCubeMesh(Vector3.right * 2);

            ConvexBoundary convexBoundary1 = ConvexBoundary.FromMesh(unitCube1);
            ConvexBoundary convexBoundary2 = ConvexBoundary.FromMesh(unitCube2);

            Assert.False(convexBoundary1.DoesOverlapWith(convexBoundary2));
        }

        [Test]
        public void DoesOverlapWithAndFromMeshUnitCubeTrue()
        {
            Mesh unitCube1 = MeshGeneratorUtility.GenerateUnitCubeMesh();
            Mesh unitCube2 = MeshGeneratorUtility.GenerateUnitCubeMesh();

            ConvexBoundary convexBoundary1 = ConvexBoundary.FromMesh(unitCube1);
            ConvexBoundary convexBoundary2 = ConvexBoundary.FromMesh(unitCube2);

            Assert.True(convexBoundary1.DoesOverlapWith(convexBoundary2));
        }

        [Test]
        public void DoesOverlapWithUsingFromMeshWithPositionOffsetUnitCubeFalse()
        {
            Mesh unitCube1 = MeshGeneratorUtility.GenerateUnitCubeMesh();
            Mesh unitCube2 = MeshGeneratorUtility.GenerateUnitCubeMesh();

            ConvexBoundary convexBoundary1 = ConvexBoundary.FromMesh(unitCube1);
            ConvexBoundary convexBoundary2 = ConvexBoundary.FromMesh(unitCube2, Vector3.right * 2, Quaternion.identity, Vector3.one);

            Assert.False(convexBoundary1.DoesOverlapWith(convexBoundary2));
        }

        [Test]
        public void TransformPointNoChange()
        {
            Assert.AreEqual(
                Vector3.zero,
                ConvexBoundary._TransformPoint(Vector3.zero, Vector3.zero, Quaternion.identity, Vector3.one)
            );
        }

        [Test]
        public void TransformPointAllOffsetsXY()
        {
            Assert.True(_AreCloseEnough(
                new Vector3(3, -2, 0),
                ConvexBoundary._TransformPoint(new Vector3(1, 2, 0), new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, -90), Vector3.one * 2)
            ));
        }

        private static bool _AreCloseEnough(Vector3 v1, Vector3 v2)
        {
            return Vector3.Distance(v1, v2) < VECTOR_DUPLICATE_MIN_DISTANCE;
        }
    }
}