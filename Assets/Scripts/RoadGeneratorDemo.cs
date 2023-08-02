using System.Collections;
using System.Collections.Generic;
using RoadGeneration;
using UnityEngine;

public class RoadGeneratorDemo : ARoadGenerator
{
    [SerializeField] private int _targetRoadLength = 10;
    [SerializeField] private float _timeBetweenPiecePlacing = 0.5f;
    [SerializeField] private float _maxTimeBetweenRemovingPieces = 1f;
    private float _timeUntilNextPiece;
    private float _timeUntilRemovingNextPiece;

    private new void Awake()
    {
        base.Awake();
        _timeUntilNextPiece = 0;
        _timeUntilRemovingNextPiece = 0;
    }

    private new void Update()
    {
        base.Update();
        _timeUntilNextPiece -= Time.deltaTime;
        _timeUntilRemovingNextPiece -= Time.deltaTime;
    }

    protected override bool ShouldPlaceNewPiece()
    {
        return _timeUntilNextPiece <= 0;
    }

    protected override bool ShouldRemoveLastPiece()
    {
        return currentPieces.Count > _targetRoadLength || _timeUntilRemovingNextPiece <= 0;
    }

    protected override void OnNewPiecePlaced()
    {
        _timeUntilNextPiece += _timeBetweenPiecePlacing;
        _timeUntilRemovingNextPiece = _maxTimeBetweenRemovingPieces;
    }

    protected override void OnNewPieceRemoved()
    {
        _timeUntilNextPiece = _timeBetweenPiecePlacing;
        _timeUntilRemovingNextPiece += _maxTimeBetweenRemovingPieces;
    }

    protected override List<RoadSection> GetPiecesInPreferenceOrder(List<RoadSection> sectionPrototypes)
    {
        return sectionPrototypes;
    }
}
