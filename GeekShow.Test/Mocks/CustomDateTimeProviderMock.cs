using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.Mocks
{
    public class CustomDateTimeProviderMock : ICustomDateTimeProvider
    {
        public DateTime Now
        {
            get;
            set;
        }

        public DateTime UtcNow
        {
            get;
            set;
        }
    }
}
