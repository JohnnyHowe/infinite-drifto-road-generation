using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class ConvexShape2DTester
{
    [Test]
    public void TestGetAxesUnitSquareAtOrigin()
    {
        List<Vector2> unitSquareAtOrigin = new List<Vector2> {
            new Vector2(-.5f, -.5f),
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(.5f, -.5f),
        };
        ConvexShape2D shape = new ConvexShape2D(unitSquareAtOrigin);

        List<Vector2> axes = shape.GetAxes();
        List<Vector2> expected = new List<Vector2>() { Vector2.right, Vector2.up };

        CollectionAssert.AreEquivalent(expected, axes);
    }

    [Test]
    public void TestEqualityDifferentRefrence()
    {
        List<Vector2> unitSquareAtOrigin = new List<Vector2> {
            new Vector2(-.5f, -.5f),
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(.5f, -.5f),
        };
        ConvexShape2D shape1 = new ConvexShape2D(unitSquareAtOrigin);
        ConvexShape2D shape2 = new ConvexShape2D(unitSquareAtOrigin);

        Assert.True(shape1 == shape2);
    }

    [Test]
    public void TestEqualityDifferentRefrenceDifferentVertexOrder()
    {
        List<Vector2> unitSquareAtOrigin1 = new List<Vector2> {
            new Vector2(-.5f, -.5f),
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(.5f, -.5f),
        };
        ConvexShape2D shape1 = new ConvexShape2D(unitSquareAtOrigin1);
        List<Vector2> unitSquareAtOrigin2 = new List<Vector2> {
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(-.5f, -.5f),
            new Vector2(.5f, -.5f),
        };
        ConvexShape2D shape2 = new ConvexShape2D(unitSquareAtOrigin2);

        Assert.True(shape1 == shape2);
    }
}
