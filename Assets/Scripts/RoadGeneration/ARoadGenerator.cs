using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class ARoadGenerator : MonoBehaviour
    {
        [SerializeField] private int _roadLength = 10;
        [SerializeField] private float _timeBetweenPiecePlacing = 0.5f;
        [SerializeField] private int _choiceEngineStepsPerFrame = 1;
        [SerializeField] private int _choiceEngineCheckDepth = 5;
        [SerializeField] private List<RoadSection> _roadSectionChoices; // Cannot serialize interfaces for inspector :(
        [SerializeField] private Transform _roadSectionContainer;
        private RoadGeneratorChoiceEngine _choiceEngine;
        private List<IRoadSection> _currentPieces;
        private List<RoadSection> _prototypes;

        private float _timeUntilNextPiece;

        private void Awake()
        {
            _currentPieces = new List<IRoadSection>();
            _CreatePrototypes();

            _choiceEngine = new RoadGeneratorChoiceEngine();
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);

            foreach (Transform child in _roadSectionContainer)
            {
                if (!child.gameObject.activeInHierarchy) continue;
                _currentPieces.Add(child.GetComponent<IRoadSection>());
            }

            _timeUntilNextPiece = 0;

            _choiceEngine.StepUntilChoiceIsFound();
            _PlaceNewPiece();
            _choiceEngine.StepUntilChoiceIsFound();
            _PlaceNewPiece();
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

        private void Update()
        {
            _choiceEngine.Step(_choiceEngineStepsPerFrame);
            _timeUntilNextPiece -= Time.deltaTime;
            if (_timeUntilNextPiece <= 0)
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
                _timeUntilNextPiece += _timeBetweenPiecePlacing;
                _PlaceNewPiece();
            }

            if (_currentPieces.Count > _roadLength)
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
            IRoadSection lastSection = _currentPieces[0];
            _currentPieces.RemoveAt(0);
            // TODO not this
            Destroy(((RoadSection)lastSection).gameObject);
        }

        private void _PlaceNewPiece()
        {
            IRoadSection newSection = _choiceEngine.GetChoicePrototype().Clone();
            newSection.AlignByStartPoint(_GetNextPieceStartPosition());
            _currentPieces.Add(newSection);
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);
        }

        private TransformData _GetNextPieceStartPosition()
        {
            if (_currentPieces.Count == 0)
            {
                return new TransformData(Vector3.zero, new Quaternion(0, 0, 0, 1), Vector3.one);
            }
            return _currentPieces[_currentPieces.Count - 1].GetShape().End;
        }
    }
}