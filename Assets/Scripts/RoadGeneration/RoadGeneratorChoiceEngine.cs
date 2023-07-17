using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Other;

namespace RoadGeneration
{
    /// <summary>
    /// Must call Reset() before anything else
    /// </summary>
    public class RoadGeneratorChoiceEngine
    {
        public class NoChoiceFoundException : Exception { }

        private DFSCombinationGenerator _combinationGenerator;
        private List<IRoadSection> _currentPiecesInWorld;
        private List<IRoadSection> _candidatePrototypes;

        public void Reset(List<IRoadSection> currentPiecesInWorld, List<IRoadSection> possibleChoicesInPreferenceOrder, int checkDepth)
        {
            _combinationGenerator = new DFSCombinationGenerator(possibleChoicesInPreferenceOrder.Count, checkDepth);
            _currentPiecesInWorld = currentPiecesInWorld;
            _candidatePrototypes = possibleChoicesInPreferenceOrder;
        }

        public void StepUntilChoiceIsFound()
        {
            throw new NotImplementedException();
        }

        public void Step(int choiceEngineStepsPerFrame)
        {
            if (_DoesLastCandidateSectionOverlapWithOthers())
            {
                _combinationGenerator.StepInvalid();
            }
            else
            {
                _combinationGenerator.StepValid();
            }
        }

        private bool _DoesLastCandidateSectionOverlapWithOthers()
        {
            List<RoadSectionShape> allPiecesAligned = _GetCandidatesAndCurrentPiecesInWorldAligned();
            // TODO can allPiecesAligned.Count ever be zero? If so, prevent it from breaking
            RoadSectionShape currentCandidate = allPiecesAligned[allPiecesAligned.Count - 1];

            foreach (RoadSectionShape worldRoadSectionShape in allPiecesAligned.Take(allPiecesAligned.Count - 1))
            {
                if (currentCandidate.DoesOverlapWith(worldRoadSectionShape)) return true;
            }
            return false;
        }

        private List<RoadSectionShape> _GetCandidatesAndCurrentPiecesInWorldAligned()
        {
            return _GetCurrentPiecesInWorldShapes().Concat(_GetAlignedCandidates()).ToList();
        }

        private List<RoadSectionShape> _GetCurrentPiecesInWorldShapes()
        {
            throw new NotImplementedException();
        }

        private List<RoadSectionShape> _GetAlignedCandidates()
        {
            throw new NotImplementedException();
        }

        public bool HasFoundChoice()
        {
            throw new NotImplementedException();
        }

        public IRoadSection GetChoicePrototype()
        {
            throw new NotImplementedException();
        }
    }
}