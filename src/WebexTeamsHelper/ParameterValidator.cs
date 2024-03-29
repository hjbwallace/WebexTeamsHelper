﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebexTeamsHelper
{
    internal static class ParameterValidator
    {
        public static void IsPopulated(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{name} must be populated");
        }

        public static void IsPopulated<T>(IEnumerable<T> values, string name)
        {
            if (values?.Any() != true)
                throw new ArgumentException($"{name} must be populated");
        }

        public static void AreValuesPopulated(IEnumerable<string> values, string name)
        {
            if (values?.Where(x => !string.IsNullOrWhiteSpace(x)).Any() != true)
                throw new ArgumentException($"{name} must contain at least one valid item");
        }
    }
}