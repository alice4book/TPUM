using TPUM;
namespace Tests
{
    [TestClass]
    public class UnitTestCalculator
    {
        [TestMethod]
        public void TestCalculatorAdd()
        {
            Calculator calculator = new Calculator();
            float a = 1.5f;
            float b = 0.5f;
            Assert.AreEqual(2.0f, calculator.Add(a, b));
        }
    }
}