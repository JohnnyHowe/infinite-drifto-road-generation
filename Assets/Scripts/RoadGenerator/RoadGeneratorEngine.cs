using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadGeneratorEngine
    {
        private List<RoadSection> _currentPiecesInWorld;
        private List<RoadSection> _possibleNextPieces;
        private int _forwardPiecesToCheck;
        private int[] _lastSearchState;
        private bool _lastSearchStateContainsOverlap;

        public void UpdateInputsAndResetSearch(List<RoadSection> currentPiecesInWorld, List<RoadSection> possibleNextPieces, int forwardPiecesToCheck)
        {
            _currentPiecesInWorld = currentPiecesInWorld;
            _possibleNextPieces = possibleNextPieces;
            _forwardPiecesToCheck = forwardPiecesToCheck;
            _lastSearchState = Enumerable.Range(0, forwardPiecesToCheck).Select(_ => -1).ToArray(); // init to { -1, -1, ...}
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
            Debug.Log($"State: {string.Join(", ", _lastSearchState)}, Contains Overlap: {_lastSearchStateContainsOverlap}");

            // We're already done -> early exit
            if (IsNextChoiceReady()) return;

            // How deep into the search are we: what's the index of the first -1 in _searchState
            int searchDepth = Array.IndexOf(_lastSearchState, -1);

            if (_lastSearchStateContainsOverlap)
            {
                // Piece at search is invalid, change it

                int lastChoice = _lastSearchState[_lastSearchState.Length - 1];
                if (lastChoice < _possibleNextPieces.Count - 1)
                {
                    // There's more choices at this depth
                }
                else
                {
                    // No more choices at this depth, undo
                }
            }
            else
            {
                // Current path contains no overlap
                // What happens when another piece is added?
                _lastSearchState[searchDepth + 1] = 0;
                _lastSearchStateContainsOverlap = _DoesLastPieceOverlapWithAnyOthers();
            }
        }

        private bool _DoesLastPieceOverlapWithAnyOthers()
        {
            return false;
        }

        public bool IsNextChoiceReady()
        {
            return !_lastSearchStateContainsOverlap && _lastSearchState[_lastSearchState.Length - 1] != -1;
        }

        public RoadSection GetNextChoice()
        {
            return _possibleNextPieces[_lastSearchState[_lastSearchState.Length - 1]];
        }
    }
}