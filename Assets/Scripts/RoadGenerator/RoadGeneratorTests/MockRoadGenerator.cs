using System.Collections.Generic;
using System.Linq;
using RoadGeneration;
using UnityEngine;
using Other;
using Shapes;

public class MockRoadGenerator
{
    public static List<IRoadSection> GetAlignedRoad(List<MockRoadSection> piecesToAlign)
    {
        List<IRoadSection> alignedRoad = new List<IRoadSection>();
        foreach (IRoadSection pieceToAlign in piecesToAlign)
        {
            if (alignedRoad.Count == 0)
            {
                alignedRoad.Add(pieceToAlign);
                continue;
            }

            IRoadSection lastPiece = alignedRoad[alignedRoad.Count - 1];
            alignedRoad.Add(pieceToAlign.GetAlignedTo(lastPiece.GetGlobalEndPosition(), lastPiece.GetGlobalEndRotation().eulerAngles.y));
        }
        return alignedRoad;
    }

    public static MockRoadSection GetBasicStraightAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        MockRoadSection piece = new MockRoadSection();

        // Generate bounds
        List<Vector2> topology = new List<Vector2>() {
            new Vector2(-width / 2, 0),
            new Vector2(width / 2, 0),
            new Vector2(-width / 2, length),
            new Vector2(width / 2, length),
        };
        ConvexShape2D shape = new ConvexShape2D(topology);
        RoadSectionBounds bounds = new RoadSectionBounds();
        bounds.HeightRange = new FloatRange(-height / 2, height / 2);
        bounds.Topology = shape;
        piece.LocalBounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartYRotation = 0;
        piece.LocalEndPosition = new Vector3(0, 0, length);
        piece.LocalEndYRotation = 0;

        return piece;
    }

    public static MockRoadSection GetBasic90RightAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        MockRoadSection piece = new MockRoadSection();

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
        bounds.HeightRange = new FloatRange(-height / 2, height / 2);
        bounds.Topology = shape;
        piece.LocalBounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartYRotation = 0;
        piece.LocalEndPosition = new Vector3(length, 0, length);
        piece.LocalEndYRotation = 90;

        return piece;
    }

    public static MockRoadSection GetBasic90LeftAtOrigin(float length = 2, float width = 1, float height = 1)
    {
        MockRoadSection piece = new MockRoadSection();

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
        bounds.HeightRange = new FloatRange(-height / 2, height / 2);
        bounds.Topology = shape;
        piece.LocalBounds = bounds;

        // Generate start and end
        piece.LocalStartPosition = Vector3.zero;
        piece.LocalStartYRotation = 0;
        piece.LocalEndPosition = new Vector3(-length, 0, length);
        piece.LocalEndYRotation = -90;

        return piece;
    }
}