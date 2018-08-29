using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evvosoft.VariableEngine.Business.Processor;

namespace Evvosoft.VariableEngine.Tests
{
    [TestClass]
    public class ExtractVariableTests
    {
        private VariableProcessor processor;

        [TestInitialize]
        public void Init()
        {
            // Arrange
            processor = new VariableProcessor();
        }

        [TestMethod]
        public void VariableProcessor_ExtractVariables_ReturnVariableList1()
        {
            // Arrange 

            // Act 
            var expression = "(x + max(x1, 5)) / d - sqrt(z) + b * CalculateSalary(\"Ivanov\", -1+x)";
            var match = processor.ExtractVariables(expression);
            var result = match.Variables;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual(5, result.Count);

            Assert.IsTrue(result.Contains("x"));
            Assert.IsTrue(result.Contains("x1"));
            Assert.IsTrue(result.Contains("b"));
            Assert.IsTrue(result.Contains("z"));
            Assert.IsTrue(result.Contains("d"));
        }

        [TestMethod]
        public void VariableProcessor_ExtractComplexVariables_ReturnVariableList()
        {
            // Arrange 
            var expression = "-x12 + IsValid + min(x1, -3.14) + max(a+b, x+1) / -sqrt(X11) + m_name * CalculateSalary(\"Salary (includes taxes)\", -1+x, 2.5x)";
            processor = new VariableProcessor();

            // Act 
            var match = processor.ExtractVariables(expression);
            var result = match.Variables;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual(8, result.Count);

            Assert.IsTrue(result.Contains("x12"));
            Assert.IsTrue(result.Contains("IsValid"));
            Assert.IsTrue(result.Contains("x1"));
            Assert.IsTrue(result.Contains("a"));
            Assert.IsTrue(result.Contains("b"));
            Assert.IsTrue(result.Contains("x"));
            Assert.IsTrue(result.Contains("X11"));
            Assert.IsTrue(result.Contains("m_name"));
        }
    }
}
