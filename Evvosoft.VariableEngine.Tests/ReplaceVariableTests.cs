using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evvosoft.VariableEngine.Business.Processor;

namespace Evvosoft.VariableEngine.Tests
{
    [TestClass]
    public class ReplaceVariableTests
    {
        private VariableProcessor processor;

        [TestInitialize]
        public void Init()
        {
            // Arrange
            processor = new VariableProcessor();
        }

        [TestMethod]
        public void VariableProcessor_ReplaceVariables_ReturnReplacedString()
        {
            // Arrange 
            var expression = "(x + max(x1, 5)) / d - sqrt(z) + b * CalculateSalary(\"Ivanov\", -1+x)";
            var match = processor.ExtractVariables(expression);
            var variables = new Dictionary<string, string>
            {
                { "x", "5" },
                { "x1", "556" },
                { "b", "22.55" },
                { "z", "1" },
                { "d",  "0" }
            };

            // Act 
            var result = processor.ReplaceVariables(match, variables);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual("(5 + max(556, 5)) / 0 - sqrt(1) + 22.55 * CalculateSalary(\"Ivanov\", -1+5)", result);
        }

        [TestMethod]
        public void VariableProcessor_ReplaceComplexVariables_ReturnReplacedString()
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
