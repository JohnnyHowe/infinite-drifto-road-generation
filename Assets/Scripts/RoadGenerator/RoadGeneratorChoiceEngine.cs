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
            IRoadSection lastPiece = candidatesAligned[candidatesAligned.Count - 1];

            // Create an enumerable with all the pieces we want to compare the end one with
            IEnumerable<IRoadSection> candidatesExcludingLast = candidatesAligned.Take(candidatesAligned.Count - 1);
            IEnumerable<IRoadSection> roadSectionsToCheckOverlapFor = _currentPiecesInWorld.Concat(candidatesExcludingLast);

            foreach (IRoadSection worldRoadSection in roadSectionsToCheckOverlapFor)
            {
                if (_AreOverlapping(lastPiece, worldRoadSection)) return true;
            }

            return false;
        }

        private bool _AreOverlapping(IRoadSection piece1, IRoadSection piece2)
        {
            return piece1.GetGlobalBounds().WillCauseOverlapWith(piece2.GetGlobalBounds(), Vector3.zero, 0);
        }

        internal List<IRoadSection> _GetCandidatesAligned()
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
                IRoadSection sectionAligned = sectionPrototype.GetAlignedTo(nextPieceStartPosition, nextPieceStartRotation);
                candidatesAligned.Add(sectionAligned);

                nextPieceStartPosition = sectionAligned.GetGlobalEndPosition();
                nextPieceStartRotation = sectionAligned.GetGlobalEndRotation().y;
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