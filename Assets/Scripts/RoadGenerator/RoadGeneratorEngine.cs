using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Other;

namespace RoadGeneration
{
    public class RoadGeneratorEngine
    {
        private List<IRoadSection> _currentPiecesInWorld;
        private List<IRoadSection> _possibleNextPieces;
        private int _forwardPiecesToCheck;
        private bool _lastSearchStateContainsOverlap;
        private DFSCombinationGenerator _combinationGenerator;

        public void UpdateInputsAndResetSearch(List<IRoadSection> currentPiecesInWorld, List<IRoadSection> possibleNextPieces, int forwardPiecesToCheck)
        {
            _currentPiecesInWorld = currentPiecesInWorld;
            _possibleNextPieces = possibleNextPieces;
            _forwardPiecesToCheck = forwardPiecesToCheck;
            _combinationGenerator = new DFSCombinationGenerator(possibleNextPieces.Count, forwardPiecesToCheck);
            _lastSearchStateContainsOverlap = false;
        }

        public void Run(int maxChecks)
        {
            for (int i = 0; i < maxChecks; i++)
            {
                _CheckOneIteration();
            }
        }

        private void _CheckOneIteration()
        {
            Debug.Log($"State: {string.Join(", ", _combinationGenerator.GetState())}, Contains Overlap: {_lastSearchStateContainsOverlap}");

            // We're already done -> early exit
            if (FoundValidChoice()) return;

            if (!_DoesLastPieceOverlapWithAnyOthers())
            {
                _combinationGenerator.StepValid();
            }
            else
            {
                _combinationGenerator.StepInvalid();
            }
        }

        private bool _DoesLastPieceOverlapWithAnyOthers()
        {
            IRoadSection previousPiece;
            IRoadSection thisPiece = _possibleNextPieces[_combinationGenerator.GetCurrentEnd()];

            // Vector3 rotation = previousPiece.GetGlobalEndRotation().eulerAngles - thisPiece.GetLocalStartRotation().eulerAngles;

            // RoadSectionBounds alignedBounds = thisPiece.GetBounds().GetOffsetBy();

            // Does it overlap with any pieces in the world?
            foreach (IRoadSection worldRoadSection in _currentPiecesInWorld)
            {
                // if this piece will cause an overlap, return true;
                // if (thisPiece.GetBounds().WillCauseOverlapWith(worldRoadSection, ));
            }

            // Does it overlap with any pieces we're thinking about using?
            for (int pieceIndex = 0; pieceIndex < _combinationGenerator.GetCurrentDepth(); pieceIndex++)
            {
                // if this piece will cause an overlap, return true;
            }

            // No overlap, must be a-okay
            return false;
        }

        public bool FoundValidChoice()
        {
            return !_lastSearchStateContainsOverlap && _combinationGenerator.HasFoundSolution();
        }

        public IRoadSection GetNextChoice()
        {
            return _possibleNextPieces[0];
        }
    }
}