using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExampleTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void ExampleTestSimplePasses()
    {
        // Use the Assert class to test conditions
        Example e = new Example();
        float actual = e.DoCalculation();
        Assert.AreEqual(4.2f, actual);
    }
}
