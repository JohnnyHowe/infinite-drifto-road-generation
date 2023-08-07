using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Other;
using RoadGeneration;

public class DecisionCheckerDemo : MonoBehaviour
{
    [SerializeField] private int depth = 5;
    [SerializeField] private Transform currentPiecesContainer;
    [SerializeField] private List<RoadSection> choices;

    private RoadGeneratorChoiceEngine _engine;
    private List<RoadSection> demoSections;

    private void Awake()
    {
        _engine = new RoadGeneratorChoiceEngine();
        demoSections = new List<RoadSection>();
    }

    private void Update()
    {
        ClearDemoSections();

        _engine.Reset(GetCurrentSections(), GetSectionChoices(), depth);
        _engine.StepUntilChoiceIsFound();

        PlaceDemoSections();
    }

    private void ClearDemoSections()
    {
        foreach (RoadSection section in demoSections)
        {
            Destroy(section.gameObject);
        }
        demoSections = new List<RoadSection>();
    }

    private void PlaceDemoSections()
    {
        List<RoadSection> currentSections = GetCurrentSections();

        demoSections = new List<RoadSection>();
        TransformData start = currentSections[currentSections.Count - 1].GetShape().End;
        foreach (int choiceIndex in _engine._combinationGenerator.GetState())
        {
            RoadSection toPlace = choices[choiceIndex];
            RoadSection copy = toPlace.Clone();
            copy.AlignByStartPoint(start);
            start = copy.GetShape().End;
            demoSections.Add(copy);
        }
    }

    private List<RoadSection> GetSectionChoices()
    {
        return choices;
    }

    private List<RoadSection> GetCurrentSections()
    {
        List<RoadSection> section = new List<RoadSection>();
        foreach (Transform piece in currentPiecesContainer)
        {
            section.Add(piece.GetComponent<RoadSection>());
        }
        return section;
    }
}
