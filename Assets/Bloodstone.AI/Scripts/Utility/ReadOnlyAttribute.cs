using System;
using UnityEngine;

namespace Bloodstone.AI.Utility
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
}