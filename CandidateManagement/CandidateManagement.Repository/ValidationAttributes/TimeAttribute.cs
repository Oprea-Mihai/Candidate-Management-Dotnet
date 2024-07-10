using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Repository.ValidationAttributes
{
    public class TimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string timeString;
            if (value is not string)
                return false;
            else
                timeString = (string)value;
            TimeSpan time;
            return TimeSpan.TryParse(timeString,out time);
        }
    }
}
