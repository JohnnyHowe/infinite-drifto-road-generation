using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RoadGeneration.Tests
{
    public class RoadGeneratorChoiceEngineTester
    {
        [Test]
        public void TestHasFoundSolutionFalseOnNoSteps()
        {
            RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();
            // engine.Reset(new List<IRoadSection>(), );
            Assert.False(engine.HasFoundChoice());
        }

        [Test]
        public void TestGetChoicePrototypeThrowsErrorNoStep()
        {
            RoadGeneratorChoiceEngine engine = new RoadGeneratorChoiceEngine();
            // engine.Reset(new List<IRoadSection>(), );
            try
            {
                engine.GetChoicePrototype();
                Assert.Fail("Engine should have thrown a NoChoiceFoundException");
            }
            catch (RoadGeneratorChoiceEngine.NoChoiceFoundException _) { }
        }
    }
}