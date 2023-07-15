using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private List<RoadSection> _cornerPrefabs;  // Cannot serialize IRoadSection for inspector, so this will do
        [SerializeField] private RoadGeneratorChoiceEngine _roadGeneratorEngine;
        [SerializeField] private int _piecesToGenerate = 10;

        private void Start()
        {
            _roadGeneratorEngine = new RoadGeneratorChoiceEngine();
            _roadGeneratorEngine.UpdateInputsAndResetSearch(new List<IRoadSection>(), _cornerPrefabs.Cast<IRoadSection>().ToList(), 10);

            for (int i = 0; i < _piecesToGenerate; i++)
            {
                while (!_roadGeneratorEngine.FoundValidChoice())
                {
                    _roadGeneratorEngine.Run(1);
                }
                _InstantiateNewPiece(_roadGeneratorEngine.GetNextChoice());
            }
        }

        private void _InstantiateNewPiece(IRoadSection roadSection)
        {
            Debug.Log(roadSection);
        }
    }
}