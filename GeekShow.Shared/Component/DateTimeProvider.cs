using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public static class DateTimeProvider
    {
        #region Properties

        public static ICustomDateTimeProvider CustomProvider
        {
            get;
            set;
        }

        public static DateTime Now
        {
            get
            {
                return CustomProvider?.Now ?? DateTime.Now;
            }
        }

        public static DateTime UtcNow
        {
            get
            {
                return CustomProvider?.UtcNow ?? DateTime.UtcNow;
            }
        }

        #endregion
    }
}
