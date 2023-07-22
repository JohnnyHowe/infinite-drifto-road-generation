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
        [SerializeField] private Transform _roadSectionContainer;
        private RoadGeneratorChoiceEngine _choiceEngine;
        private List<IRoadSection> _currentPieces;
        private List<RoadSection> _prototypes;

        private void Awake()
        {
            _currentPieces = new List<IRoadSection>();
            _CreatePrototypes();

            _choiceEngine = new RoadGeneratorChoiceEngine();
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);

            foreach (Transform child in _roadSectionContainer)
            {
                _currentPieces.Add(child.GetComponent<IRoadSection>());
            }
        }

        private void _CreatePrototypes()
        {
            _prototypes = new List<RoadSection>();
            foreach (RoadSection roadSection in _roadSectionChoices)
            {
                GameObject prototype = Instantiate(roadSection.gameObject);
                prototype.transform.parent = transform;
                prototype.SetActive(false);
                RoadSection instantiatedSection = prototype.GetComponent<RoadSection>();
                _prototypes.Add(instantiatedSection);
            }
        }

        public int ptp = 2;
        public int piecesPlaced = 0;
        private void Update()
        {
            if (piecesPlaced >= ptp) return;
            // _choiceEngine.Step(_choiceEngineStepsPerFrame);

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
                piecesPlaced++;
            }
            if (_ShouldRemovePiece())
            {
                _RemoveLastPiece();
            }
        }

        private List<IRoadSection> _GetCurrentPiecesInWorld()
        {
            return _currentPieces;
        }

        private List<IRoadSection> _GetNextPossiblePiecesInPreferenceOrder()
        {
            return _prototypes.Cast<IRoadSection>().ToList();
        }

        private void _RemoveLastPiece()
        {
            throw new NotImplementedException();
        }

        private void _PlaceNewPiece()
        {
            IRoadSection newSection = _choiceEngine.GetChoicePrototype().Clone();
            newSection.AlignByStartPoint(_GetNextPieceStartPosition());
            _currentPieces.Add(newSection);
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);
        }

        private RoadSectionShape.EndPoint _GetNextPieceStartPosition()
        {
            if (_currentPieces.Count == 0)
            {
                return new RoadSectionShape.EndPoint(Vector3.zero, Quaternion.Euler(0, 0, 1));
            }
            return _currentPieces[_currentPieces.Count - 1].GetShape().End;
        }

        private bool _ShouldPlacePiece()
        {
            return true;
        }

        private bool _ShouldRemovePiece()
        {
            // TODO
            return false;
        }
    }
}