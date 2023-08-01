using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public abstract class ARoadGenerator : MonoBehaviour
    {
        [SerializeField] private int _choiceEngineStepsPerFrame = 1;
        [SerializeField] private int _choiceEngineCheckDepth = 5;
        [SerializeField] private List<RoadSection> _roadSectionChoices; // Cannot serialize interfaces for inspector :(
        [SerializeField] private Transform _roadSectionContainer;
        private RoadGeneratorChoiceEngine _choiceEngine;
        protected List<IRoadSection> currentPieces;
        protected List<RoadSection> prototypes;

        protected void Awake()
        {
            currentPieces = new List<IRoadSection>();
            _CreatePrototypes();

            _choiceEngine = new RoadGeneratorChoiceEngine();
            _ResetEngine();

            foreach (Transform child in _roadSectionContainer)
            {
                if (!child.gameObject.activeInHierarchy) continue;
                currentPieces.Add(child.GetComponent<IRoadSection>());
            }
        }

        private void _CreatePrototypes()
        {
            prototypes = new List<RoadSection>();
            foreach (RoadSection roadSection in _roadSectionChoices)
            {
                GameObject prototype = Instantiate(roadSection.gameObject);
                prototype.transform.parent = transform;
                prototype.SetActive(false);
                RoadSection instantiatedSection = prototype.GetComponent<RoadSection>();
                prototypes.Add(instantiatedSection);
            }
        }

        protected void Update()
        {
            _choiceEngine.Step(_choiceEngineStepsPerFrame);
            if (ShouldPlaceNewPiece())
            {
                try
                {
                    _choiceEngine.StepUntilChoiceIsFound();
                }
                catch (RoadGeneratorChoiceEngine.NoChoiceFoundException _)
                {
                    // Debug.Log("Could not find valid road section choice!");
                }
                if (_choiceEngine.HasFoundChoice())
                {
                    OnNewPiecePlaced();
                    _PlaceNewPiece();
                }
                else
                {
                    // Debug.Log("Could not find valid road section choice!");
                }
            }

            if (ShouldRemoveLastPiece())
            {
                _RemoveLastPiece();
                OnNewPieceRemoved();
            }
        }

        protected virtual void OnNewPiecePlaced() { }
        protected virtual void OnNewPieceRemoved() { }
        protected abstract bool ShouldPlaceNewPiece();
        protected abstract bool ShouldRemoveLastPiece();

        private List<IRoadSection> _GetCurrentPiecesInWorld()
        {
            return currentPieces;
        }

        private List<IRoadSection> _GetNextPossiblePiecesInPreferenceOrder()
        {
            return prototypes.Cast<IRoadSection>().ToList();
        }

        private void _RemoveLastPiece()
        {
            if (currentPieces.Count == 0) return;
            IRoadSection lastSection = currentPieces[0];
            currentPieces.RemoveAt(0);
            // TODO not this
            Destroy(((RoadSection)lastSection).gameObject);
            _ResetEngine();
        }

        private void _PlaceNewPiece()
        {
            IRoadSection newSection = _choiceEngine.GetChoicePrototype().Clone();
            newSection.AlignByStartPoint(_GetNextPieceStartPosition());
            currentPieces.Add(newSection);
            _ResetEngine();
        }

        private void _ResetEngine()
        {
            _choiceEngine.Reset(_GetCurrentPiecesInWorld(), _GetNextPossiblePiecesInPreferenceOrder(), _choiceEngineCheckDepth);
        }

        private TransformData _GetNextPieceStartPosition()
        {
            if (currentPieces.Count == 0)
            {
                return new TransformData(Vector3.zero, new Quaternion(0, 0, 0, 1), Vector3.one);
            }
            return currentPieces[currentPieces.Count - 1].GetShape().End;
        }
    }
}