using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class DFSCombinationGeneratorTester
{
    [Test]
    public void TestGetCurrentDepthIndexEmpty()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { -1, -1, -1, -1, -1 });
        Assert.AreEqual(generator.GetCurrentDepthIndex(), -1);
    }

    [Test]
    public void TestGetCurrentDepthIndex()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, -1, -1, -1 });
        Assert.AreEqual(generator.GetCurrentDepthIndex(), 1);
    }

    [Test]
    public void TestGetCurrentDepthIndexAlmostMax()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, 0, 0, -1 });
        Assert.AreEqual(generator.GetCurrentDepthIndex(), 3);
    }

    [Test]
    public void TestGetCurrentDepthIndexMax()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, 0, 0, 0 });
        Assert.AreEqual(generator.GetCurrentDepthIndex(), 4);
    }

    [Test]
    public void TestInitialState()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        CollectionAssert.AreEqual(new int[] { -1, -1, -1, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestGetCurrentEndPartialFull()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 1, 2, -1, -1 });
        Assert.AreEqual(2, generator.GetCurrentEnd());
    }

    [Test]
    public void TestGetCurrentEndEmpty()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { -1, -1, -1, -1, -1 });
        Assert.AreEqual(-1, generator.GetCurrentEnd());
    }

    [Test]
    public void TestGetCurrentEndFull()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 1, 2, 3, 4 });
        Assert.AreEqual(4, generator.GetCurrentEnd());
    }

    [Test]
    public void TestHasFoundSolutionTrue()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, 0, 0, 0 });
        Assert.True(generator.HasFoundSolution());
    }

    [Test]
    public void TestHasFoundSolutionFalse()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, 0, -1, -1 });
        Assert.False(generator.HasFoundSolution());
    }

    [Test]
    public void TestValidOne()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.StepValid();
        CollectionAssert.AreEqual(new int[] { 0, -1, -1, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestValidThree()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.StepValid();
        generator.StepValid();
        generator.StepValid();
        CollectionAssert.AreEqual(new int[] { 0, 0, 0, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestCompleteAllValid()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(3, 3);
        generator.StepValid();
        generator.StepValid();
        generator.StepValid();
        Assert.True(generator.HasFoundSolution());
    }

    [Test]
    public void TestInvalidOne()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(3, 3);
        generator.StepInvalid();
        CollectionAssert.AreEqual(new int[] { 0, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestInvalidThreeNoBackTrack()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.StepInvalid();
        generator.StepInvalid();
        generator.StepInvalid();
        CollectionAssert.AreEqual(new int[] { 2, -1, -1, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestInvalidSingleBackTrack()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 0, 4, -1, -1 });
        generator.StepInvalid();
        CollectionAssert.AreEqual(new int[] { 0, 1, -1, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestInvalidDoubleBackTrack()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 0, 4, 4, -1, -1 });
        generator.StepInvalid();
        CollectionAssert.AreEqual(new int[] { 1, -1, -1, -1, -1 }, generator.GetState());
    }

    [Test]
    public void TestInvalidDoubleBackTrackToStart()
    {
        DFSCombinationGenerator generator = new DFSCombinationGenerator(5, 5);
        generator.SetState(new int[] { 4, 4, 4, -1, -1 });
        generator.StepInvalid();
        // What now?
    }
}