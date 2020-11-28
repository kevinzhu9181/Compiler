using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Quadruple
    {
        public string Operation { get; set; }
        public string StrLeft { get; set; }
        public string StrRight { get; set; }
        public string Result { get; set; }
        public bool Input { get; set; }
        public Quadruple() { }
        public Quadruple(string operation, string strLeft, string strRight, string result)
        {
            this.Operation = operation;
            this.StrLeft = strLeft;
            this.StrRight = strRight;
            this.Result = result;
            this.Input = false;
        }
    }
}
