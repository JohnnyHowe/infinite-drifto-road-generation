using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class ConvexShape2DTester
{
    private List<Vector2> _unitSquareAtOrigin = new List<Vector2> {
            new Vector2(-.5f, -.5f),
            new Vector2(-.5f, .5f),
            new Vector2(.5f, .5f),
            new Vector2(-.5f, .5f),
        };

    private void _AssertSameContentUnordered(List<Vector2> expected, List<Vector2> actual, string failMessage)
    {
        HashSet<Vector2> expectedSet = new HashSet<Vector2>(expected);
        HashSet<Vector2> actualSet = new HashSet<Vector2>(actual);
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TestGetAxesUnitSquareAtOrigin()
    {
        ConvexShape2D shape = new ConvexShape2D(_unitSquareAtOrigin);
        List<Vector2> axes = shape.GetAxes();
        List<Vector2> expected = new List<Vector2>() { Vector2.right, Vector2.up };
        _AssertSameContentUnordered(expected, axes, "Incorrect axes!");
    }
}
