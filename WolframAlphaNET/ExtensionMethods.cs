﻿using System.Collections.Generic;

namespace WolframAlphaNET
{
    public static class ExtensionMethods
    {
        public static bool HasElements<T>(this List<T> list)
        {
            return (list != null && list.Count >= 1);
        }
    }
}