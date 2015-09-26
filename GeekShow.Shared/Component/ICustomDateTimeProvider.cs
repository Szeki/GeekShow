using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public interface ICustomDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
