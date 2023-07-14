using System;

namespace RoadGeneration
{
    public struct FloatRange
    {
        private float _min;
        private float _max;

        public void Set(float value1, float value2)
        {
            _min = Math.Min(value1, value2);
            _max = Math.Max(value1, value2);
        }

        public float GetMin()
        {
            return _min;
        }

        public float GetMax()
        {
            return _max;
        }
    }
}