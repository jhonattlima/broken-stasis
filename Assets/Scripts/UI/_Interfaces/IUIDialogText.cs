using System;
using Utilities.UI;

namespace UI
{
    public interface IUIDialogText
    {
        void StartDialog(DialogEnum p_dialogName, Action p_onDialogEnd = null);
    }
}