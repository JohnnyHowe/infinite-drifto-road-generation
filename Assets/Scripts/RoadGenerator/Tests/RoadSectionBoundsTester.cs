using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class RoadSectionBoundsTester
{
    [Test]
    public void TestFromMeshNoOffset()
    {
        // Mesh unitCubeMesh = MeshGeneratorUtility.GenerateUnitCubeMesh();
        // RoadSectionBounds bounds = RoadSectionBounds.FromMesh(unitCubeMesh, Vector3.zero, Quaternion.identity, Vector3.zero);
        // TODO
    }

    [Test]
    public void TestEqualityDifferentRefrence()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.Topology = new ConvexShape2D(new List<Vector2>() { Vector2.up, Vector2.right, Vector2.down });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.Topology = new ConvexShape2D(new List<Vector2>() { Vector2.up, Vector2.right, Vector2.down });

        Assert.True(o1 == o2);
    }
}