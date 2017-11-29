using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class PositionTests : MonoBehaviour
{
    private GameObject block;
    private Position position;

    [SetUp]
    public void RunBeforeAnyTests()
    {
        block = new GameObject("Block");
        block.AddComponent<Position>();
        position = block.GetComponent<Position>();
    }

    [TearDown]
    public void Cleanup()
    {
    }

    [Test]
    public void InitialPosition()
    {
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(0, 0)));
    }

    [Test]
    public void MoveTo1x0()
    {
        var hIndex = 1;
        position.HIndex = hIndex;
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(hIndex * position.XDelta, 0)));
    }

    [Test]
    public void MoveTo5x0()
    {
        var hIndex = 5;
        position.HIndex = hIndex;
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(hIndex * position.XDelta, 0)));
    }

    [Test]
    public void MoveTo0x1()
    {
        var vIndex = 1;
        position.VIndex = vIndex;
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(0, vIndex * position.YDelta)));
    }

    [Test]
    public void MoveTo0x5()
    {
        var vIndex = 5;
        position.VIndex = vIndex;
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(0, vIndex * position.YDelta)));
    }

    [Test]
    public void MoveTo5x5()
    {
        var hIndex = 5;
        var vIndex = 5;
        position.SetIndex(hIndex, vIndex);
        Assert.That(position.GetPosition(), Is.EqualTo(new Vector2(hIndex * position.XDelta, vIndex * position.YDelta)));
    }
}
