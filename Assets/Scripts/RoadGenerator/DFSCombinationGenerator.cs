using System;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class DFSCombinationGenerator
    {
        private int[] _state;
        private int _maxDepth;
        private int _nBranches;
        private const int MAX_ITERATIONS = 10000;

        public DFSCombinationGenerator(int branches, int depth)
        {
            SetState(Enumerable.Range(0, depth).Select(_ => -1).ToArray()); // init to { -1, -1, ...}
            _maxDepth = depth;
            _nBranches = branches;
        }

        public int[] GetState()
        {
            return _state;
        }

        public void SetState(int[] newState)
        {
            _state = newState;
        }

        public void StepInvalid()
        {
            int currentDepth = GetCurrentDepthIndex();
            int indexInQuestion = Mathf.Max(0, currentDepth);

            if (_state[indexInQuestion] < _nBranches - 1)
            {
                // no backtrack
                _state[indexInQuestion]++;
            }
            else
            {
                _Backtrack();
            }
        }

        private void _Backtrack()
        {
            int currentDepth = GetCurrentDepthIndex();
            int indexInQuestion = Mathf.Max(0, currentDepth);

            // Acting as a while loop - I'm sick of breaking Unity through infinite loops 
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                // exit clause, imagine the contents in while(!...)
                if (_state[indexInQuestion] < _nBranches - 1) break;

                // do backtrack
                _state[indexInQuestion] = -1;
                _state[indexInQuestion - 1]++;
                indexInQuestion -= 1;
            }
        }

        public void StepValid()
        {
            if (HasFoundSolution()) return;

            _state[GetCurrentDepthIndex() + 1] = 0;
        }

        public bool HasFoundSolution()
        {
            return _state[_maxDepth - 1] != -1;
        }

        public int GetCurrentDepthIndex()
        {
            int firstNegativeOneIndex = Array.IndexOf(_state, -1);
            if (firstNegativeOneIndex >= 0) return firstNegativeOneIndex - 1;
            return _maxDepth - 1;
        }

        public int GetCurrentEnd()
        {
            return _state[Mathf.Max(0, GetCurrentDepthIndex())];
        }
    }
}