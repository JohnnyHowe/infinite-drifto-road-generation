using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadGeneration;

public class RoadSectionShapeDemo : MonoBehaviour
{
    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private Transform _translatedHandle;

    void Update()
    {
        RoadSectionShape shape = new RoadSectionShape();
        shape.SetBoundaryFromMesh(_mesh.mesh, TransformData.FromTransform(_mesh.transform), TransformData.FromTransform(_start));
        shape.Start = TransformData.FromTransform(_start);
        shape.End = TransformData.FromTransform(_end);
        // shape.DebugDraw();  

        RoadSectionShape translated = shape.GetTranslatedCopy(TransformData.FromTransform(_translatedHandle));
        translated.DebugDraw();  
    }
}
