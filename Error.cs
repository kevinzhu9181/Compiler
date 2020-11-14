using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Error
    {
        public int RowNumber { get; set; }
        public string ErrorChar { get; set; }
        public string ErrorNote { get; set; }

        public Error(int rowNumber, string errorChar, string errorNote)
        {
            RowNumber = rowNumber;
            ErrorChar = errorChar;
            ErrorNote = errorNote;
        }
    }
}
