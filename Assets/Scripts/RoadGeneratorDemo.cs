using System.Collections;
using System.Collections.Generic;
using RoadGeneration;
using UnityEngine;

public class RoadGeneratorDemo : ARoadGenerator
{
    [SerializeField] private int _roadLength = 10;
    [SerializeField] private float _timeBetweenPiecePlacing = 0.5f;
    private float _timeUntilNextPiece;

    private new void Awake()
    {
        base.Awake();
        _timeUntilNextPiece = 0;
    }

    private new void Update()
    {
        base.Update();
        _timeUntilNextPiece -= Time.deltaTime;
    }

    protected override bool ShouldPlaceNewPiece()
    {
        return _timeUntilNextPiece <= 0;
    }

    protected override bool ShouldRemoveLastPiece()
    {
        return currentPieces.Count > _roadLength;
    }

    protected override void OnNewPiecePlaced()
    {
        _timeUntilNextPiece += _timeBetweenPiecePlacing;
    }
}
