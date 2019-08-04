using System;
using Xunit;

namespace WebexTeamsHelper.Tests
{
    public abstract class ValidationTestBase
    {
        protected readonly string[] EmptyStrings = new[] { null, "", "  " };

        protected void ThrowsArgumentException(Action action)
        {
            Assert.Throws<ArgumentException>(action);
        }
    }
}
