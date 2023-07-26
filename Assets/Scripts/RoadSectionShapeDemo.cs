using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadGeneration;

public class RoadSectionShapeDemo : MonoBehaviour
{
    [SerializeField] private RoadSection _section;
    [SerializeField] private Transform _translatedHandle;

    private RoadSection clone;

    void Update()
    {
        if (clone != null) Destroy(clone.gameObject);
        clone = (RoadSection)_section.Clone();
        clone.AlignByStartPoint(TransformData.FromTransform(_translatedHandle));
        clone.Draw();
    }
}
