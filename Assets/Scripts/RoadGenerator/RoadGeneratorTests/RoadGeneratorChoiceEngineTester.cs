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

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<MockRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        Assert.False(engine.FoundValidChoice());
    }

    [Test]
    public void TestFoundValidChoiceAllValidOneCurrentPiece()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<MockRoadSection>() {
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

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<MockRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasic90RightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(5);
        Assert.False(engine.FoundValidChoice());
    }

    [Test]
    public void TestFoundValidChoiceImpossibleTwoCurrentStraightPieces()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<MockRoadSection>() {
            MockRoadGenerator.GetBasicStraightAtOrigin(),
            MockRoadGenerator.GetBasicStraightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasic90RightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(50);
        Assert.False(engine.FoundValidChoice());
    }

[Test]
    public void TestFoundValidChoiceImpossibleThreeCurrentRightPieces()
    {
        RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();

        List<IRoadSection> currentRoadSections = MockRoadGenerator.GetAlignedRoad(new List<MockRoadSection>() {
            MockRoadGenerator.GetBasic90RightAtOrigin(),
            MockRoadGenerator.GetBasic90RightAtOrigin(),
            MockRoadGenerator.GetBasic90RightAtOrigin(),
        });

        List<IRoadSection> possibleChoices = new List<IRoadSection>() {
            MockRoadGenerator.GetBasic90RightAtOrigin(),
        };

        engine.UpdateInputsAndResetSearch(currentRoadSections, possibleChoices, 5);
        engine.Run(5);
        Assert.False(engine.FoundValidChoice());
    }
}