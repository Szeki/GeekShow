using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GeekShow.Component
{
    public class MessageBoxPopupService : IPopupMessageService
    {
        #region IPopupMessageService implementation

        public async void DisplayMessage(string content, string title)
        {
            var msgBox = new MessageDialog(content, title);

            await msgBox.ShowAsync();
        }

        #endregion
    }
}
