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
        List<Vector2> _unitSquareAtOrigin = new List<Vector2> {
            new Vector2(-.5f, -.5f),
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(.5f, -.5f),
        };
        ConvexShape2D shape = new ConvexShape2D(_unitSquareAtOrigin);

        List<Vector2> axes = shape.GetAxes();
        List<Vector2> expected = new List<Vector2>() { Vector2.right, Vector2.up };

        CollectionAssert.AreEquivalent(expected, axes);
    }
}
