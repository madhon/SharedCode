using NUnit.Framework;

 

namespace Madhon.BddFromScratch.Framework
{

    public class ThenAttribute : TestAttribute
    {
    }


    [TestFixture]
    public abstract class BehaviourTest
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            Given();
            When();
        }

        protected abstract void Given();
        
        protected abstract void When();
    }
}

public class ExampleBehaviour : BehaviourTest
{
  protected override void Given()
  {
    // the system is setup in a certain state
  }

  protected override void When()
  {
    // a defined action is performed on the system
  }

  [Then]
  public void ThisAssertionShouldBeSatisfied()
  {
    Assert.IsTrue(true);
  }

  [Then]
  public void AnotherAssertionShouldBeSatisfied()
  {
    Assert.IsTrue(true);
  }
}

