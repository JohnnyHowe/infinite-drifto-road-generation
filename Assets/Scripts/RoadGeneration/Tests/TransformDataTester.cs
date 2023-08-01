using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RoadGeneration;
using UnityEngine;
using UnityEngine.TestTools;

public class TransformDataTester
{
    private const float CLOSE_ENOUGH_THRESHOLD = 0.0001f;

    [Test]
    public void TransformPointNoChange()
    {
        TransformData transformData = new TransformData(Vector3.zero, Quaternion.identity, Vector3.one);
        Assert.AreEqual(Vector3.one, transformData.TransformPoint(Vector3.one));
    }

    [Test]
    public void InverseTransformPointVector()
    {
        TransformData transformData = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 45), Vector3.one * 2);
        Vector3 worldPoint = new Vector3(3f, 2f, 5f);
        Vector3 actual = transformData.InverseTransformPoint(worldPoint);
        AssertCloseEnough(new Vector3(0.71f, -.71f, 1), actual, 0.005f);
    }

    [Test]
    public void TransformPointVector()
    {
        TransformData transformData = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 45), Vector3.one * 2);
        Vector3 localPoint = new Vector3(0.71f, -.71f, 1);
        Vector3 actual = transformData.TransformPoint(localPoint);
        AssertCloseEnough(new Vector3(3f, 2f, 5f), actual, 0.01f);
    }

    [Test]
    public void TransformPointTransformDataPositionOffsetOnly()
    {
        TransformData transformData = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 0), Vector3.one);
        TransformData localPoint = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 45), new Vector3(1, 2, 3));
        TransformData actual = transformData.TransformPoint(localPoint);
        TransformData expected = new TransformData(new Vector3(2, 4, 6), Quaternion.Euler(0, 0, 45), new Vector3(1, 2, 3));
        AssertCloseEnough(expected, actual, 0.01f);
    }

    [Test]
    public void TransformPointTransformDataZRotationOffsetOnly()
    {
        TransformData transformData = new TransformData(Vector3.zero, Quaternion.Euler(0, 0, -90), Vector3.one);
        TransformData localPoint = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 0), Vector3.one);
        TransformData actual = transformData.TransformPoint(localPoint);
        TransformData expected = new TransformData(new Vector3(2, -1, 3), Quaternion.Euler(0, 0, -90), Vector3.one);
        AssertCloseEnough(expected, actual, 0.01f);
    }

    [Test]
    public void TransformPointTransformDataScaleOffsetOnly()
    {
        TransformData transformData = new TransformData(Vector3.zero, Quaternion.Euler(0, 0, 0), new Vector3(1, 2, 3));
        TransformData localPoint = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 0), Vector3.one);
        TransformData actual = transformData.TransformPoint(localPoint);
        TransformData expected = new TransformData(new Vector3(1, 4, 9), Quaternion.Euler(0, 0, 0), new Vector3(1, 2, 3));
        AssertCloseEnough(expected, actual, 0.01f);
    }

    [Test]
    public void TransformPointTransformDataPositionAndScaleOffset()
    {
        TransformData transformData = new TransformData(new Vector3(2, 1, 0), Quaternion.Euler(0, 0, 0), new Vector3(1, 2, 3));
        TransformData localPoint = new TransformData(new Vector3(1, 2, 3), Quaternion.Euler(0, 0, 0), Vector3.one);
        TransformData actual = transformData.TransformPoint(localPoint);
        TransformData expected = new TransformData(new Vector3(1, 4, 9), Quaternion.Euler(0, 0, 0), new Vector3(1, 2, 3));
        AssertCloseEnough(expected, actual, 0.01f);
    }

    private static void AssertCloseEnough(TransformData expected, TransformData actual, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        AssertCloseEnough(expected.Position, actual.Position, $"TransformData.Position\nExpected: {expected.Position}\n But was: {actual.Position}", threshold);
        AssertCloseEnough(expected.Rotation, actual.Rotation, $"TransformData.Rotation\nExpected: {expected.Rotation.eulerAngles}\n But was: {actual.Rotation.eulerAngles}", threshold);
        AssertCloseEnough(expected.Scale, actual.Scale, $"TransformData.Scale\nExpected: {expected.Scale}\n But was: {actual.Scale}", threshold);
    }

    private static bool CloseEnough(TransformData d1, TransformData d2, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        return CloseEnough(d1.Position, d2.Position, threshold) && CloseEnough(d1.Scale, d2.Scale, threshold) && CloseEnough(d1.Rotation, d2.Rotation, threshold);
    }

    private static void AssertCloseEnough(Vector3 expected, Vector3 actual, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        AssertCloseEnough(expected, actual, $"Expected: {expected}\n But was: {actual}", threshold);
    }

    private static void AssertCloseEnough(Vector3 expected, Vector3 actual, string errorMessage, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        Assert.True(CloseEnough(expected, actual, threshold), errorMessage);
    }

    private static void AssertCloseEnough(Quaternion expected, Quaternion actual, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        AssertCloseEnough(expected, actual, $"Expected: {expected}\n But was: {actual}", threshold);
    }

    private static void AssertCloseEnough(Quaternion expected, Quaternion actual, string errorMessage, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        Assert.True(CloseEnough(expected, actual, threshold), errorMessage);
    }

    private static bool CloseEnough(Vector3 v1, Vector3 v2, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        return CloseEnough((v1 - v2).magnitude, 0, threshold);
    }

    private static bool CloseEnough(Quaternion v1, Quaternion v2, float threshold = CLOSE_ENOUGH_THRESHOLD)
    {
        return CloseEnough(v1.eulerAngles, v2.eulerAngles, threshold);
    }

    private static bool CloseEnough(float n1, float n2, float threshold)
    {
        return (n1 - n2) < threshold;
    }
}
