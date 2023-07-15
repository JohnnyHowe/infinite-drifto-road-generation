using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RoadGeneration;

public class RoadGeneratorChoiceEngineTester
{
    [Test]
    public void TestFoundValidChoiceFalseOnNoRunOneCurrentPiece()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

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
    public void TestOneIterationValidStraightOneCurrentPiece()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

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
    public void TestFoundValidChoiceAllValidOneCurrentPiece()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

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
    public void TestFoundValidChoiceImpossibleOneCurrentPiece()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<RoadSectionMock>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
            MockRoadGenerator.GetBasic90LeftAtOrigin(),
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