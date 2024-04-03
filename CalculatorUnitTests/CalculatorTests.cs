using CalculatorApp;
using System.Reflection;

namespace CalculatorUnitTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod, TestCategory("Calculation")]
        public void Calculation_WrongOperatorName()
        {
            Calculator calc = new Calculator();
            MethodInfo? calcMethod = calc.GetType().GetMethod("ExecuteOperation", BindingFlags.NonPublic | BindingFlags.Instance);

            Exception? exception = null;
            try
            {
                calcMethod?.Invoke(calc, new object[] { new CalcData { objectName = "o1", @operator = "fff", value1 = 5, value2 = 3 } });
            }
            catch (TargetInvocationException ex)
            {
                exception = ex;
            }
            Assert.IsTrue(exception?.InnerException is ArgumentException);
        }

        [TestMethod, TestCategory("Calculation")]
        public void Calculation_TestOperations()
        {
            Assert.IsTrue(CalculationCheck("../../../TestData/AddTestInput.json", "../../../TestData/AddTestOutput.txt"));
            Assert.IsTrue(CalculationCheck("../../../TestData/SubTestInput.json", "../../../TestData/SubTestOutput.txt"));
            Assert.IsTrue(CalculationCheck("../../../TestData/MulTestInput.json", "../../../TestData/MulTestOutput.txt"));
            Assert.IsTrue(CalculationCheck("../../../TestData/SqrtTestInput.json", "../../../TestData/SqrtTestOutput.txt"));
        }

        private bool CalculationCheck(string inputFilePath,  string outputFilePath)
        {
            Assert.IsTrue(File.Exists(inputFilePath));
            Assert.IsTrue(File.Exists(outputFilePath));

            string input = File.ReadAllText(inputFilePath);
            string output = File.ReadAllText(outputFilePath);

            Calculator calc = new Calculator();
            calc.SetupOperationsFromJSON(input);

            List<CalcResult> result = calc.GetResultSafe();
            result.Sort((x, y) => x.result.CompareTo(y.result));
            string resultOutput = "";

            for (int i = 0; i < result.Count; i++)
            {
                resultOutput += result[i].ToString();
                if (i < result.Count - 1) resultOutput += "\r\n";
            }

            return resultOutput == output;
        }

        [TestMethod, TestCategory("Calculation")]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Calculation_WrongJSONData()
        {
            Assert.IsTrue(File.Exists("../../../TestData/WrongData.json"));
            string input = File.ReadAllText("../../../TestData/WrongData.json");

            Calculator calc = new Calculator();
            calc.SetupOperationsFromJSON(input);
        }
    }
}