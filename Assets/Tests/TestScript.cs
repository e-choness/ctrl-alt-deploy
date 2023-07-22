using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestScript: InputTestFixture
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
            var keyboard = InputSystem.AddDevice<Keyboard>();
            Press(keyboard.spaceKey);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}


