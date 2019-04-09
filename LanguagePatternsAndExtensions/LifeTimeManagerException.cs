using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguagePatternsAndExtensions
{
    public class LifeTimeManagerException : Exception
    {
        public LifeTimeManagerException(string message) : base(message)
        { }
    }
}
