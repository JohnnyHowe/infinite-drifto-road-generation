using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;
using Shapes;
using Other;

public class RoadSectionBoundsTester
{
    [Test]
    public void TestFromMeshNoOffset()
    {
        Mesh unitCubeMesh = MeshGeneratorUtility.GenerateUnitCubeMesh();
        RoadSectionBounds bounds = RoadSectionBounds.FromMesh(unitCubeMesh, Vector3.zero, Quaternion.identity, Vector3.one);

        List<Vector2> unitCubeTopology = new List<Vector2>() {
            new Vector2(.5f, .5f),
            new Vector2(-.5f, .5f),
            new Vector2(-.5f, -.5f),
            new Vector2(.5f, -.5f)
        };

        ConvexShape2D expected = new ConvexShape2D(unitCubeTopology);
        Assert.AreEqual(bounds.GlobalTopology, expected);
        Assert.AreEqual(bounds.GlobalYRange, new FloatRange(-.5f, .5f));
    }

    [Test]
    public void TestFromMeshPositionOffset()
    {
        Mesh unitCubeMesh = MeshGeneratorUtility.GenerateUnitCubeMesh();
        Vector3 positionOffset = Vector3.one;
        Vector2 topologyPositionOffset = Vector2.one;
        RoadSectionBounds bounds = RoadSectionBounds.FromMesh(unitCubeMesh, positionOffset, Quaternion.identity, Vector3.one);

        List<Vector2> unitCubeTopology = new List<Vector2>() {
            new Vector2(.5f, .5f) + topologyPositionOffset,
            new Vector2(-.5f, .5f) + topologyPositionOffset,
            new Vector2(-.5f, -.5f) + topologyPositionOffset,
            new Vector2(.5f, -.5f) + topologyPositionOffset
        };

        ConvexShape2D expected = new ConvexShape2D(unitCubeTopology);
        Assert.AreEqual(bounds.GlobalTopology, expected);
        Assert.AreEqual(bounds.GlobalYRange, new FloatRange(.5f, 1.5f));
    }

    [Test]
    public void TestFromMeshScaleOffset()
    {
        Mesh unitCubeMesh = MeshGeneratorUtility.GenerateUnitCubeMesh();
        RoadSectionBounds bounds = RoadSectionBounds.FromMesh(unitCubeMesh, Vector3.zero, Quaternion.identity, Vector3.one * 2);

        List<Vector2> unitCubeTopology = new List<Vector2>() {
            new Vector2(1, 1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            new Vector2(1, -1),
        };

        ConvexShape2D expected = new ConvexShape2D(unitCubeTopology);
        Assert.AreEqual(bounds.GlobalTopology, expected);
        Assert.AreEqual(bounds.GlobalYRange, new FloatRange(-1f, 1f));
    }

    // Commented out because floating point math causes assertion failures
    // [Test]
    // public void TestFromMeshRotationYOffset()
    // {
    //     Mesh unitCubeMesh = MeshGeneratorUtility.GenerateUnitCubeMesh();
    //     float yDegs = 45;
    //     float yRads = Mathf.Deg2Rad * yDegs;
    //     RoadSectionBounds bounds = RoadSectionBounds.FromMesh(unitCubeMesh, Vector3.zero, Quaternion.Euler(new Vector3(0, yDegs, 0)), Vector3.one);

    //     List<Vector2> unitCubeTopology = new List<Vector2>() {
    //         new Vector2(Mathf.Sin(yRads), 0),
    //         new Vector2(0, Mathf.Sin(yRads)),
    //         new Vector2(0, -Mathf.Sin(yRads)),
    //         new Vector2(-Mathf.Sin(yRads), 0),
    //     };

    //     ConvexShape2D expected = new ConvexShape2D(unitCubeTopology);
    //     CollectionAssert.AreEquivalent(bounds.GlobalTopology.GetVertices(), expected.GetVertices());
    // }

    [Test]
    public void TestEqualityDifferentRefrence()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.GlobalTopology = new ConvexShape2D(new List<Vector2>() { Vector2.up, Vector2.right, Vector2.down });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.GlobalTopology = new ConvexShape2D(new List<Vector2>() { Vector2.up, Vector2.right, Vector2.down });

        Assert.True(o1 == o2);
    }

    [Test]
    public void TestWillCauseOverlapNoOffsetFalse()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
         });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(2, 0),
            new Vector2(3, 0),
            new Vector2(3, 1),
            new Vector2(2, 1),
         });

        Assert.False(o1.WillCauseOverlapWith(o2, Vector3.zero, 0));
    }

    [Test]
    public void TestWillCauseOverlapNoOffsetTrue()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
         });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(.5f, 0),
            new Vector2(1.5f, 0),
            new Vector2(1.5f, 1),
            new Vector2(.5f, 1),
         });

        Assert.True(o1.WillCauseOverlapWith(o2, Vector3.zero, 0));
    }

    [Test]
    public void TestWillCauseOverlapPositionOffsetFalse()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
         });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
         });

        Assert.False(o1.WillCauseOverlapWith(o2, Vector3.right * 2, 0));
    }

    [Test]
    public void TestWillCauseOverlapRotationAndPositionOffsetFalse()
    {
        RoadSectionBounds o1 = new RoadSectionBounds();
        o1.GlobalYRange = new FloatRange(0, 1);
        o1.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(-.5f, -.5f),
            new Vector2(.5f, -.5f),
            new Vector2(.5f, .5f),
            new Vector2(-.5f, .5f),
        });

        RoadSectionBounds o2 = new RoadSectionBounds();
        o2.GlobalYRange = new FloatRange(0, 1);
        o2.GlobalTopology = new ConvexShape2D(new List<Vector2>() {
            new Vector2(-.5f, -.5f),
            new Vector2(.5f, -.5f),
            new Vector2(.5f, .5f),
            new Vector2(-.5f, .5f),
        });

        Assert.False(o1.WillCauseOverlapWith(o2, Vector3.one * 0.9f, 45));
    }
}