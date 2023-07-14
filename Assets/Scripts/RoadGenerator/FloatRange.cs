using System;

namespace RoadGeneration
{
    public class FloatRange
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public FloatRange(float value1, float value2)
        {
            Set(value1, value2);
        }

        public void Set(float value1, float value2)
        {
            Min = Math.Min(value1, value2);
            Max = Math.Max(value1, value2);
        }
    }
}