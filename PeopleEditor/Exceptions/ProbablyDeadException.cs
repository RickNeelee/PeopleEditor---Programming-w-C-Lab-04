using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleEditor.Exceptions
{
    internal class ProbablyDeadException : Exception
    {
        public ProbablyDeadException() { }
        public ProbablyDeadException(string message) : base(message) { }
        public ProbablyDeadException(string message, Exception inner) : base(message, inner) { }

    }
}
