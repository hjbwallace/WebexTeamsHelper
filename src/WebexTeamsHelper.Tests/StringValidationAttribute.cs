using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace WebexTeamsHelper.Tests
{
    public class StringValidationAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return new object[][]
            {
                new string[] { null },
                new string[] { "" },
                new string[] { "   " },
                new string[] { "\t" },
                new string[] { "\n" }
            };
        }
    }
}