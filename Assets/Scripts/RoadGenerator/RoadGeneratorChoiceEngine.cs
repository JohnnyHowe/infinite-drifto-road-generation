using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Other;

namespace RoadGeneration
{
    public class RoadGeneratorChoiceEngine
    {
        private List<IRoadSection> _currentPiecesInWorld;
        private List<IRoadSection> _possibleNextPieces;
        private int _forwardPiecesToCheck;
        private bool _lastSearchStateContainsOverlap;
        private IDFSCombinationGenerator _combinationGenerator;

        public void UpdateInputsAndResetSearch(List<IRoadSection> currentPiecesInWorld, List<IRoadSection> possibleNextPiecesInPreferenceOrder, int forwardPiecesToCheck)
        {
            _currentPiecesInWorld = currentPiecesInWorld;
            _possibleNextPieces = possibleNextPiecesInPreferenceOrder;
            _forwardPiecesToCheck = forwardPiecesToCheck;
            _combinationGenerator = new DFSCombinationGenerator(possibleNextPiecesInPreferenceOrder.Count, forwardPiecesToCheck);
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
            List<IRoadSection> candidatesAligned = _GetCandidatesAligned();
            IRoadSection lastPiece = candidatesAligned[candidatesAligned.Count];

            // Create an enumerable with all the pieces we want to compare the end one with
            IEnumerable<IRoadSection> candidatesExcludingLast = candidatesAligned.Take(candidatesAligned.Count - 1);
            IEnumerable<IRoadSection> roadSectionsToCheckOverlapFor = _currentPiecesInWorld.Concat(candidatesExcludingLast);

            foreach (IRoadSection worldRoadSection in roadSectionsToCheckOverlapFor)
            {
                // If overlap, return true
            }

            return false;
        }

        private static bool _AreOverlapping(IRoadSection section1, IRoadSection section2)
        {
            return false;
        }

        private List<IRoadSection> _GetCandidatesAligned()
        {
            List<IRoadSection> candidatesAligned = new List<IRoadSection>();

            // Setup where the first candidate piece should be aligned from
            // If there are no current pieces in the world, default to zero
            Vector3 nextPieceStartPosition = Vector3.zero;
            float nextPieceStartRotation = 0;
            if (_currentPiecesInWorld.Count > 0)
            {
                nextPieceStartPosition = _currentPiecesInWorld[_currentPiecesInWorld.Count - 1].GetGlobalEndPosition();
                nextPieceStartRotation = _currentPiecesInWorld[_currentPiecesInWorld.Count - 1].GetGlobalEndRotation().y;
            }

            // Align
            foreach (int candidateIndex in _combinationGenerator.GetState())
            {
                if (candidateIndex == -1) break;

                IRoadSection sectionPrototype = _possibleNextPieces[candidateIndex];
                candidatesAligned.Add(sectionPrototype.GetAlignedTo(nextPieceStartPosition, nextPieceStartRotation));
            }
            return candidatesAligned;
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