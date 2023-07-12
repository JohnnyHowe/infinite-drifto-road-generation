using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _cornerObjects;
        [SerializeField] private RoadGeneratorEngine _roadGeneratorEngine;

        private void Update()
        {

        }
    }
}