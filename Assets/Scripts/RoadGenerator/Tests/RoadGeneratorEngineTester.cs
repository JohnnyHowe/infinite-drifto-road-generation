using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class RoadGeneratorEngineTester
{
    [Test]
    public void TestFoundValidChoiceFalseOnNoRun()
    {
        RoadGeneratorEngine engine = new RoadGeneratorEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<RoadSectionMock>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        Assert.False(engine.FoundValidChoice());
    }

    [Test]
    public void TestOneIterationValidStraight()
    {
        RoadGeneratorEngine engine = new RoadGeneratorEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<RoadSectionMock>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(1);
        Assert.False(engine.FoundValidChoice());
    }

    [Test]
    public void TestFoundValidChoiceAllValid()
    {
        RoadGeneratorEngine engine = new RoadGeneratorEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<RoadSectionMock>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(5);
        Assert.True(engine.FoundValidChoice());
    }

    [Test]
    public void TestFoundValidChoiceImpossible()
    {
        RoadGeneratorEngine engine = new RoadGeneratorEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<RoadSectionMock>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasic90RightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(5);
        Assert.False(engine.FoundValidChoice());
    }
}