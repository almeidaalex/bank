using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain
{
    public struct Balance
    {
        public decimal Value { get; private set; }
        public static Balance Zero => new();
    }
}
