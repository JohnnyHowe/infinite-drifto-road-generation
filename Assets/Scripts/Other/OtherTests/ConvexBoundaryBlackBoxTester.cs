using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Other.Tests
{
    public class ConvexBoundaryBlackBoxTester
    {
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
    }
}