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
        public class NoChoiceFoundException : Exception { }

        public void Reset(List<IRoadSection> currentPiecesInWorld, List<IRoadSection> possibleChoicesInPreferenceOrder, int checkDepth)
        {
            throw new NotImplementedException();
        }

        public void Step(int choiceEngineStepsPerFrame)
        {
            throw new NotImplementedException();
        }

        public void StepUntilChoiceIsFound()
        {
            throw new NotImplementedException();
        }
    }
}