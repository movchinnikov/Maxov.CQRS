using System;

namespace MaxovCQRS.Common
{
    public class ExecutingTypeAttribute : Attribute
    {
        public ExecutingType ExecutingType { get; }

        public ExecutingTypeAttribute(ExecutingType executingType)
        {
            ExecutingType = executingType;
        }
    }
}