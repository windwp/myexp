using System.Drawing;
using ManagedWinapi.Windows;

namespace AppHook
{
    public class MWinUtil
    {
        public static Rectangle GetControlSize(RECT pRect)
        {
            return new Rectangle(pRect.Top,pRect.Left,pRect.Right-pRect.Left,pRect.Bottom-pRect.Top);
        }
    }
}
