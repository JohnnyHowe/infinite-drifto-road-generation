using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using JonathonOH.RoadGeneration;
using Other;

public class OverlapDetectorDemoWithShape : MonoBehaviour
{
    [SerializeField] private GameObject _enableOnOverlap;
    [SerializeField] private MeshFilter _mesh1;
    [SerializeField] private Transform _mesh1Handle;
    [SerializeField] private bool mesh1InfiniteHeight = true;
    [SerializeField] private MeshFilter _mesh2;
    [SerializeField] private Transform _mesh2Handle;
    [SerializeField] private bool mesh2InfiniteHeight = true;

    private void Update() {
        RoadSectionShape shape1 = new RoadSectionShape();
        shape1.SetBoundaryFromMesh(_mesh1.mesh, TransformData.FromTransform(_mesh1.transform), TransformData.FromTransform(_mesh1Handle), mesh1InfiniteHeight);
        RoadSectionShape shape2 = new RoadSectionShape();
        shape2.SetBoundaryFromMesh(_mesh2.mesh, TransformData.FromTransform(_mesh2.transform), TransformData.FromTransform(_mesh2Handle), mesh2InfiniteHeight);

        shape1.DebugDraw();
        shape2.DebugDraw();

        _enableOnOverlap.SetActive(shape1.DoesOverlapWith(shape2));
    }
}
