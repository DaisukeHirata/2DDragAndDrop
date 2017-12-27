using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class DragObjectTests : MonoBehaviour
{
    [SetUp]
    public void RunBeforeAnyTests()
    {
    }

    [TearDown]
    public void Cleanup()
    {
    }

    [Test]
    public void InitialPosition()
    {
        //       Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(0, 0)));
    }
}
