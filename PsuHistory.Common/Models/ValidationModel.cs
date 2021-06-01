using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Common.Models
{
    public class ValidationModel<TResult>
    {
        public TResult Result;
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        private int countError;
        public int CountError
        {
            get
            {
                return Errors?.Count() ?? default;
            }
            private set
            {
                countError = value;
            }
        }

        private bool isValid;
        public bool IsValid
        {
            get
            {
                return (CountError == 0);
            }
            private set
            {
                isValid = value;
            }
        }
    }
}
