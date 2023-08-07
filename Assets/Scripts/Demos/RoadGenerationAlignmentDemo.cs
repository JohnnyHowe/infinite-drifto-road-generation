using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Other;
using RoadGeneration;

public class RoadGenerationAlignmentDemo : MonoBehaviour
{
    [SerializeField] private RoadSection[] prototypes;
    [SerializeField] private int[] choiceIndices;

    private RoadSection[] instantiatedSections;

    private void Start()
    {
        InstantiateAll();
    }

    private void InstantiateAll()
    {
        instantiatedSections = new RoadSection[choiceIndices.Length];
        for (int i = 0; i < choiceIndices.Length; i++)
        {
            GameObject newSection = Instantiate(prototypes[choiceIndices[i]].gameObject);
            RoadSection roadSection = newSection.GetComponent<RoadSection>();

            if (i > 0) {
                roadSection.AlignByStartPoint(instantiatedSections[i - 1].GetShape().End);
            }

            instantiatedSections[i] = roadSection;
            roadSection.gameObject.SetActive(true);
        }
    }
}
