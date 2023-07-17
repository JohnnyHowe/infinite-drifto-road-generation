using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private int _choiceEngineStepsPerFrame = 1;
        [SerializeField] private int _choiceEngineCheckDepth = 5;
        [SerializeField] private List<RoadSection> _roadSectionChoices; // Cannot serialize interfaces for inspector :(
        private RoadGeneratorChoiceEngine _choiceEngine;

        private void Awake()
        {
            _choiceEngine = new RoadGeneratorChoiceEngine();
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);
        }

        private void Update()
        {
            _choiceEngine.Step(_choiceEngineStepsPerFrame);

            if (_ShouldPlacePiece())
            {
                try
                {
                    _choiceEngine.StepUntilChoiceIsFound();
                }
                catch (RoadGeneratorChoiceEngine.NoChoiceFoundException _)
                {
                    // This is really bad, the whole point of the generator is to avoid this
                    Debug.LogError("Could not find valid road section choice!");
                    return;
                }
                _PlaceNewPiece();
            }
            if (_ShouldRemovePiece())
            {
                _RemoveLastPiece();
            }
        }

        private List<IRoadSection> _GetCurrentPiecesInWorld()
        {
            throw new NotImplementedException();
        }

        private List<IRoadSection> _GetNextPossiblePiecesInPreferenceOrder()
        {
            throw new NotImplementedException();
        }

        private void _RemoveLastPiece()
        {
            throw new NotImplementedException();
        }

        private void _PlaceNewPiece()
        {
            throw new NotImplementedException();
        }

        private bool _ShouldPlacePiece()
        {
            throw new NotImplementedException();
        }

        private bool _ShouldRemovePiece()
        {
            throw new NotImplementedException();
        }
    }
}