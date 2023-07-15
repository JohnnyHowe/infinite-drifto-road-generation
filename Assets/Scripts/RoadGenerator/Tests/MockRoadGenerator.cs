using System.Collections.Generic;
using System.Linq;
using RoadGeneration;
using UnityEngine;

public class MockRoadGenerator
{
    public static List<IRoadSection> GetAlignedRoad(List<RoadSectionMock> piecesToAlign)
    {
        List<RoadSectionMock> alignedRoad = new List<RoadSectionMock>();
        foreach (RoadSectionMock pieceToAlign in piecesToAlign)
        {
            if (alignedRoad.Count == 0)
            {
                alignedRoad.Add(pieceToAlign);
                continue;
            }

            RoadSectionMock lastPiece = alignedRoad[alignedRoad.Count - 1];

            // Rotate
            Vector3 objectRotation = (lastPiece.GetGlobalEndRotation().eulerAngles - pieceToAlign.GetLocalStartRotation().eulerAngles);
            pieceToAlign.SetRotation(Quaternion.Euler(objectRotation));
            // Translate
            Vector3 objectTranslation = lastPiece.GetGlobalEndPosition() - pieceToAlign.GetLocalStartPosition();
            pieceToAlign.SetPosition(objectTranslation);

            alignedRoad.Add(pieceToAlign);
        }
        return alignedRoad.Cast<IRoadSection>().ToList();
    }

    public static RoadSectionMock GetBasicStraightAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        RoadSectionMock piece = new RoadSectionMock();

        // Generate bounds
        List<Vector2> topology = new List<Vector2>() {
            new Vector2(-width / 2, 0),
            new Vector2(width / 2, 0),
            new Vector2(-width / 2, length),
            new Vector2(width / 2, length),
        };
        ConvexShape2D shape = new ConvexShape2D(topology);
        RoadSectionBounds bounds = new RoadSectionBounds();
        bounds.GlobalYRange = new FloatRange(-height / 2, height / 2);
        bounds.GlobalTopology = shape;
        piece.Bounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartRotation = Quaternion.Euler(0, 0, 0);
        piece.OriginalGlobalEndPosition = new Vector3(0, 0, length);
        piece.OriginalGlobalEndRotation = Quaternion.Euler(0, 0, 0);
        piece.SetPosition(Vector3.zero);
        piece.SetRotation(Quaternion.identity);

        return piece;
    }

    public static RoadSectionMock GetBasic90RightAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        RoadSectionMock piece = new RoadSectionMock();

        // Generate bounds
        List<Vector2> topology = new List<Vector2>() {
            new Vector2(-width / 2, 0),
            new Vector2(width / 2, 0),
            new Vector2(length, length + width / 2),
            new Vector2(length, length - width / 2),
            new Vector2(0, length),
        };
        ConvexShape2D shape = new ConvexShape2D(topology);
        RoadSectionBounds bounds = new RoadSectionBounds();
        bounds.GlobalYRange = new FloatRange(-height / 2, height / 2);
        bounds.GlobalTopology = shape;
        piece.Bounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartRotation = Quaternion.Euler(0, 0, 0);
        piece.OriginalGlobalEndPosition = new Vector3(length, 0, length);
        piece.OriginalGlobalEndRotation = Quaternion.Euler(0, 90, 0);
        piece.SetPosition(Vector3.zero);
        piece.SetRotation(Quaternion.identity);

        return piece;
    }

    public static RoadSectionMock GetBasic90LeftAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        RoadSectionMock piece = new RoadSectionMock();

        // Generate bounds
        List<Vector2> topology = new List<Vector2>() {
            new Vector2(-width / 2, 0),
            new Vector2(width / 2, 0),
            new Vector2(-length, length + width / 2),
            new Vector2(-length, length - width / 2),
            new Vector2(0, length),
        };
        ConvexShape2D shape = new ConvexShape2D(topology);
        RoadSectionBounds bounds = new RoadSectionBounds();
        bounds.GlobalYRange = new FloatRange(-height / 2, height / 2);
        bounds.GlobalTopology = shape;
        piece.Bounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartRotation = Quaternion.Euler(0, 0, 0);
        piece.OriginalGlobalEndPosition = new Vector3(-length, 0, length);
        piece.OriginalGlobalEndRotation = Quaternion.Euler(0, -90, 0);
        piece.SetPosition(Vector3.zero);
        piece.SetRotation(Quaternion.identity);

        return piece;
    }
}