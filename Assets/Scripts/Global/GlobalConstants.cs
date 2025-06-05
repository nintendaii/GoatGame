using System.Text.RegularExpressions;
using UnityEngine;

namespace Global
{
    public static class GlobalConstants
    {
        public static class Validation
        {
            public static readonly Regex NAME_VALIDATION_REGEX =
                new("^[A-Za-zА-Яа-яЁё][A-Za-zА-Яа-яЁё\\s]{1,18}[A-Za-zА-Яа-яЁё]$");
        }

        public static class Balance
        {
            public static readonly float ACTION_THRESHOLD = 1f;
        }
        
        public static class Combat
        {
            public static readonly float CRITICAL_HIT_MULTIPLIER = 1.5f;
        }
    }
}