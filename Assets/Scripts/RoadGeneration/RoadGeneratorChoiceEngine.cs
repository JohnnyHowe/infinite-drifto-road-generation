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
            while (!HasFoundChoice()) Step(1);
        }

        public void Step(int choiceEngineStepsPerFrame)
        {
            Debug.Log(String.Join(", ", _combinationGenerator.GetState()));
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
                // if (currentCandidate.DoesOverlapWith(worldRoadSectionShape)) return true;
                throw new NotImplementedException();
            }
            return false;
        }

        private List<RoadSectionShape> _GetCandidatesAndCurrentPiecesInWorldAligned()
        {
            return _GetCurrentPiecesInWorldShapes().Concat(_GetCandidatesAligned()).ToList();
        }

        private List<RoadSectionShape> _GetCurrentPiecesInWorldShapes()
        {
            return _currentPiecesInWorld.Select(section => section.GetShape()).ToList();
        }

        private List<RoadSectionShape> _GetCandidatesAligned()
        {
            // Figuring out the architecture so this method could exist was a nightmare.
            // Both big redesigns were a result of this.
            // I hope it looks obvious and easy to make yourself - that means I've done it right
            List<RoadSectionShape> alignedCandidates = new List<RoadSectionShape>();
            TransformData nextStartPoint = _GetFirstCandidateStartPoint();
            foreach (IRoadSection candidateSection in _GetCandidatesNotAligned())
            {
                // RoadSectionShape alignedCandidateShape = candidateSection.GetLocalShape().GetCopyWithStartAlignedTo(nextStartPoint);
                // alignedCandidates.Add(alignedCandidateShape);
                // nextStartPoint = alignedCandidateShape.End;
                throw new NotImplementedException();
            }
            return alignedCandidates;
        }

        private List<IRoadSection> _GetCandidatesNotAligned()
        {
            List<IRoadSection> candidates = new List<IRoadSection>();
            foreach (int candidateChoiceIndex in _combinationGenerator.GetState())
            {
                if (candidateChoiceIndex == -1) break;
                candidates.Add(_candidatePrototypes[candidateChoiceIndex]);
            }
            return candidates;
        }

        private TransformData _GetFirstCandidateStartPoint()
        {
            if (_currentPiecesInWorld.Count == 0)
            {
                // return new TransformData(Vector3.zero, Quaternion.Euler(0, 0, 1));
            throw new NotImplementedException();
            }
            return _currentPiecesInWorld[_currentPiecesInWorld.Count - 1].GetShape().End;

        }

        public bool HasFoundChoice()
        {
            return _combinationGenerator.HasFoundSolution();
        }

        public IRoadSection GetChoicePrototype()
        {
            if (!HasFoundChoice())
            {
                throw new NoChoiceFoundException();
            }
            return _candidatePrototypes[_combinationGenerator.GetState()[0]];
        }
    }
}