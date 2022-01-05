using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.Enum
{
    public class Result
    {
        public const int SUCCESS = 0;
        public const int FAIL = 1;
        public const int EXIST = 2;
        public const int NOTFOUND = 3;
        public const int NOTREMOVE = 4;
        public const int NOTEDIT = 5;
        public const int NOTBOOKING = 6;
        public const int IGNOREPRICE = 7;
        public const int NOTFOUNDHALL = 8;
        public const int NOTFOUNDFOOD = 9;
        public const int NOTFOUNDSV = 8;
    }
}
