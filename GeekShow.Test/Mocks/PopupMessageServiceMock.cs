using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.Mocks
{
    public class PopupMessageServiceMock : MockBase, IPopupMessageService
    {
        public void DisplayMessage(string content, string title)
        {
            RecordCall(nameof(IPopupMessageService.DisplayMessage), content, title);

            ProcessMethodAction(nameof(IPopupMessageService.DisplayMessage));
        }
    }
}
