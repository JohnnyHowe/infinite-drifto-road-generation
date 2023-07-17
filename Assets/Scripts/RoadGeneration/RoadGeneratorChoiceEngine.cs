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

        public void Reset(List<IRoadSection> currentPiecesInWorld, List<IRoadSection> possibleChoicesInPreferenceOrder, int checkDepth)
        {
            _combinationGenerator = new DFSCombinationGenerator(possibleChoicesInPreferenceOrder.Count, checkDepth);
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