using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Other;
using JonathonOH.RoadGeneration;

public class RoadSectionShapeDemo : MonoBehaviour
{
    [SerializeField][TextArea] private string _description;
    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Transform _startHandle;
    [SerializeField] private Transform _translatedHandle;

    void Update()
    {
        RoadSectionShape shape = new RoadSectionShape();
        shape.SetBoundaryFromMesh(_mesh.mesh, TransformData.FromTransform(_mesh.transform), TransformData.FromTransform(_startHandle), false);
        shape.DebugDraw();

        RoadSectionShape shape2 = shape.GetTranslatedCopy(TransformData.FromTransform(_translatedHandle));
        shape2.DebugDraw();
    }
}
