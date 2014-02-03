using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStructures.Models
{
    public class IntComparer : Comparer<int>
    {
        public override int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}