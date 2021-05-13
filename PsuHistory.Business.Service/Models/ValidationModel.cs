using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Models
{
    public class ValidationModel
    {
        public Dictionary<string, string> Errors { get; set; }

        private int countError;

        public int CountError 
        { 
            get 
            {
                return Errors.Count();
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
