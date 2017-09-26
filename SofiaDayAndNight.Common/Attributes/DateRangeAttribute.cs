using System;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Common.Attributes
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute()
          : base(typeof(DateTime), DateTime.Now.ToShortDateString(), DateTime.Now.AddYears(1).ToShortDateString()) { }
    }
}
