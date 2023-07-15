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
            _roadGeneratorEngine.Run(1);
        }
    }
}