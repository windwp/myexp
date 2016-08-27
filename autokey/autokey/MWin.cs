using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ManagedWinapi.Windows;


namespace AppHook
{
    /// <summary>
    /// 
    ///  MessageType list http://www.autohotkey.com/docs/misc/SendMessageList.htm
    /// http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/Messages.html
    /// </summary>
    public class MWin
    {

        public const UInt32 WM_KEYDOWN = 0x100;
        public const UInt32 WM_KEYUP = 0x101;
        public const UInt32 WM_CHAR = 0x101;
        public const UInt32 BM_CLICK = 0x00F5;

        public const UInt32 WM_LBUTTONDOWN = 0x0201;
        public const UInt32 WM_LBUTTONUP = 0x0202;
        public const UInt32 WM_MOUSEMOVE = 0x0200;
        public const UInt32 WM_SETFOCUS = 0x0007 ;
        public const UInt32 MK_LBUTTON = 0x0001;
        public const UInt32 WM_SETCURSOR = 0x0020;
        public const UInt32 WM_MOUSEACTIVATE = 0x0021;




        /// <summary>
        /// The FindWindow API
        /// </summary>
        /// <param name="lpClassName">the class name for the window to search for</param>
        /// <param name="lpWindowName">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// The SendMessage API
        /// </summary>
        /// <param name="hWnd">handle to the required window</param>
        /// <param name="msg">the system/Custom message to send</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The FindWindowEx API
        /// </summary>
        /// <param name="parentHandle">a handle to the parent window </param>
        /// <param name="childAfter">a handle to the child window to start search after</param>
        /// <param name="className">the class name for the window to search for</param>
        /// <param name="windowTitle">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        /// <summary>
        /// Get Windown Rect
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);
        // Or use System.Drawing.Point (Forms only)

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetCursorPos(uint x, uint y);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern Boolean AdjustWindowRect(ref RECT lpRect, UInt32 dwStyle, bool bMenu);

        public static void KeyDown(IntPtr hWnd, VirtualKeyStates key)
        {
            SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
        }

        public static void KeyUp(IntPtr hWnd, VirtualKeyStates key)
        {
            SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
        }

        public static void KeyDownAndUp(IntPtr hWnd, VirtualKeyStates key)
        {
            KeyDown(hWnd, key);
            Thread.Sleep(50);
            KeyUp(hWnd, key);
        }
        public static void KeyChar(IntPtr hWnd, int keychar)
        {
            SendMessage(hWnd, WM_CHAR, (IntPtr)keychar, (IntPtr)0);
        }
        public static void KeyDown(string winTitle, VirtualKeyStates key)
        {
            IntPtr hWnd = FindWindow(null, winTitle);
            SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
        }

        /// <summary>
        /// Sends a Keydup message(0x101) to the specified window with a Virtual Key
        /// </summary>
        /// <param name="winTitle">Window Title</param>
        /// <param name="Key">Virtual Key to Send</param>
        public static void KeyUp(string winTitle, VirtualKeyStates Key)
        {
            IntPtr hWnd = FindWindow(null, winTitle);
            SendMessage(hWnd, 0x101, (IntPtr)Key, (IntPtr)0);
        }
        public static void ControlSendMessage(IntPtr hWnd, VirtualKeyStates key, bool shift, bool ctrl)
        {
            hWnd = MWin.FindWindowEx(hWnd, IntPtr.Zero, "Edit", "");
            if (shift == true)
            {
                //send shift down
                SendMessage(hWnd, WM_KEYDOWN, (IntPtr)VirtualKeyStates.VK_SHIFT, (IntPtr)0);
                //send key down
                SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
                //sleep 50ms
                Thread.Sleep(50);
                //send key up
                SendMessage(hWnd, WM_KEYUP, (IntPtr)key, (IntPtr)0);
                //send shift up
                SendMessage(hWnd, WM_KEYUP, (IntPtr)VirtualKeyStates.VK_SHIFT, (IntPtr)0);
            }
            else if (ctrl == true)
            {
                //send shift down
                SendMessage(hWnd, WM_KEYDOWN, (IntPtr)VirtualKeyStates.VK_CONTROL, (IntPtr)0);
                //send key down
                SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
                //sleep 50ms
                Thread.Sleep(50);
                //send key up
                SendMessage(hWnd, WM_KEYUP, (IntPtr)key, (IntPtr)0);
                //send shift up
                SendMessage(hWnd, WM_KEYUP, (IntPtr)VirtualKeyStates.VK_SHIFT, (IntPtr)0);
            }
            else
            {
                //send key down
                SendMessage(hWnd, WM_KEYDOWN, (IntPtr)key, (IntPtr)0);
                //sleep 50ms
                Thread.Sleep(50);
                //send key up
                SendMessage(hWnd, WM_KEYUP, (IntPtr)key, (IntPtr)0);
            }
        }

        public static void MSendMessage(IntPtr hWnd, UInt32 msg, VirtualKeyStates key, int num)
        {
            SendMessage(hWnd, msg, (IntPtr)key, (IntPtr)num);
        }



        /// <summary> Capture a specific window and return it as a bitmap </summary>
        /// <param name="handle">hWnd (handle) of the window to capture</param>
        public static Bitmap Capture(IntPtr handle)
        {
            Rectangle bounds;
            var rect = new RECT();
            GetWindowRect(handle, out rect);
            bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);

            var result = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(result))
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

            return result;
        }

        /// <summary> Position of the cursor relative to the start of the capture </summary>
        public static Point CursorPosition;


        public static void CaptureBoxAndSave(Rectangle rect, string filename, IntPtr handle, ImageFormat format = null)
        {
            var bitmap = Capture(handle);

        }

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        public static void ClickPosition(IntPtr hWnd, int posX, int posY)
        {
            //SendMessage(hWnd, BM_CLICK, (IntPtr)posX ,(IntPtr) posY);
            //var rect = new RECT();

            //GetWindowRect(hWnd, out rect);
            //var x = posX - rect.Left;
            //var y = posY - rect.Top;
            IntPtr lParam = (IntPtr)(posX | (posY << 16));
            SetMousePos(hWnd, posX, posY);
            //SendMessage(hWnd, WM_SETCURSOR, IntPtr.Zero, lParam);
            ////SendMessage(hWnd, WM_MOUSEACTIVATE, hWnd, lParam);
            //SendMessage(hWnd, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
            SendMessage(hWnd, WM_MOUSEMOVE, IntPtr.Zero, lParam);
            Thread.Sleep(20);
            SendMessage(hWnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, lParam);
            SendMessage(hWnd, WM_LBUTTONUP, (IntPtr)MK_LBUTTON, lParam);
            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, posX, posY, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTDOWN, posX, posY, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTUP, posX, posY, 0, 0);

            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, posX, posY, 0, 0);
            //mouse_event( MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, posX, posY, 0, 0);
        }


        public static void ClickPosition(IntPtr hWnd, Point pos)
        {
            ClickPosition(hWnd, pos.X, pos.Y);
        }





        public static void ClickControl(IntPtr hWnd)
        {
            SendMessage(hWnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
        private static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
        public static void SetMousePos(IntPtr hWnd, int posX, int posY)
        {
            SetCursorPos((uint)posX, (uint)posY);
        }

        public static void SetWindowPos(IntPtr hWnd, Point point)
        {
            RECT rect;
            SetForegroundWindow(hWnd);
            GetWindowRect(hWnd, out rect);
            var size = MWinUtil.GetControlSize(rect);
            SetWindowPos(hWnd, (IntPtr)SpecialWindowHandles.HWND_TOP, point.X, point.Y, size.Width,
                size.Height, SetWindowPosFlags.SWP_SHOWWINDOW);

        }
        public static void SetWindowPos(IntPtr hWnd, Point point,int width,int height)
        {
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, (IntPtr)SpecialWindowHandles.HWND_TOP, point.X, point.Y, width,
                height, SetWindowPosFlags.SWP_SHOWWINDOW);

        }
        /// <summary>
        /// Transform screen point to local point in application
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="screenPos"></param>
        public static bool ScreenToClient(IntPtr hWnd, Point screenPos,out Point clientPoint)
        {
            RECT rect;
            GetWindowRect(hWnd, out rect);
            clientPoint = new Point(screenPos.X - rect.Left, screenPos.Y - rect.Top);
            if (clientPoint.X < 0 || clientPoint.Y < 0)
            {
                return false;
            }
            return true;
        }

        public static void ShowWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, ShowWindowCommands.Maximize);
            Thread.Sleep(500);
            SetForegroundWindow(hWnd);
        }
    }

    /// <summary>
    ///     Special window handles
    /// </summary>
    public enum SpecialWindowHandles
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        ///     Places the window at the top of the Z order.
        /// </summary>
        HWND_TOP = 0,
        /// <summary>
        ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        HWND_BOTTOM = 1,
        /// <summary>
        ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        HWND_TOPMOST = -1,
        /// <summary>
        ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        HWND_NOTOPMOST = -2
        // ReSharper restore InconsistentNaming
    }

    [Flags]
    public enum SetWindowPosFlags : uint
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        SWP_ASYNCWINDOWPOS = 0x4000,

        /// <summary>
        ///     Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        SWP_DEFERERASE = 0x2000,

        /// <summary>
        ///     Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        SWP_DRAWFRAME = 0x0020,

        /// <summary>
        ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        SWP_FRAMECHANGED = 0x0020,

        /// <summary>
        ///     Hides the window.
        /// </summary>
        SWP_HIDEWINDOW = 0x0080,

        /// <summary>
        ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOACTIVATE = 0x0010,

        /// <summary>
        ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS = 0x0100,

        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002,

        /// <summary>
        ///     Does not change the owner window's position in the Z order.
        /// </summary>
        SWP_NOOWNERZORDER = 0x0200,

        /// <summary>
        ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        SWP_NOREDRAW = 0x0008,

        /// <summary>
        ///     Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        SWP_NOREPOSITION = 0x0200,

        /// <summary>
        ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING = 0x0400,

        /// <summary>
        ///     Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        SWP_NOSIZE = 0x0001,

        /// <summary>
        ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOZORDER = 0x0004,

        /// <summary>
        ///     Displays the window.
        /// </summary>
        SWP_SHOWWINDOW = 0x0040,

        // ReSharper restore InconsistentNaming
    }
    
    public enum VirtualKeyStates : int
    {
        VK_LBUTTON = 0x01,
        VK_RBUTTON = 0x02,
        VK_CANCEL = 0x03,
        VK_MBUTTON = 0x04,
        //
        VK_XBUTTON1 = 0x05,
        VK_XBUTTON2 = 0x06,
        //
        VK_BACK = 0x08,
        VK_TAB = 0x09,
        //
        VK_CLEAR = 0x0C,
        VK_RETURN = 0x0D,
        //
        VK_SHIFT = 0x10,
        VK_CONTROL = 0x11,
        VK_MENU = 0x12,
        VK_PAUSE = 0x13,
        VK_CAPITAL = 0x14,
        //
        VK_KANA = 0x15,
        VK_HANGEUL = 0x15, /* old name - should be here for compatibility */
        VK_HANGUL = 0x15,
        VK_JUNJA = 0x17,
        VK_FINAL = 0x18,
        VK_HANJA = 0x19,
        VK_KANJI = 0x19,
        //
        VK_ESCAPE = 0x1B,
        //
        VK_CONVERT = 0x1C,
        VK_NONCONVERT = 0x1D,
        VK_ACCEPT = 0x1E,
        VK_MODECHANGE = 0x1F,
        //
        VK_SPACE = 0x20,
        VK_PRIOR = 0x21,
        VK_NEXT = 0x22,
        VK_END = 0x23,
        VK_HOME = 0x24,
        VK_LEFT = 0x25,
        VK_UP = 0x26,
        VK_RIGHT = 0x27,
        VK_DOWN = 0x28,
        VK_SELECT = 0x29,
        VK_PRINT = 0x2A,
        VK_EXECUTE = 0x2B,
        VK_SNAPSHOT = 0x2C,
        VK_INSERT = 0x2D,
        VK_DELETE = 0x2E,
        VK_HELP = 0x2F,
        //
        VK_LWIN = 0x5B,
        VK_RWIN = 0x5C,
        VK_APPS = 0x5D,
        //
        VK_SLEEP = 0x5F,
        //
        VK_NUMPAD0 = 0x60,
        VK_NUMPAD1 = 0x61,
        VK_NUMPAD2 = 0x62,
        VK_NUMPAD3 = 0x63,
        VK_NUMPAD4 = 0x64,
        VK_NUMPAD5 = 0x65,
        VK_NUMPAD6 = 0x66,
        VK_NUMPAD7 = 0x67,
        VK_NUMPAD8 = 0x68,
        VK_NUMPAD9 = 0x69,
        VK_MULTIPLY = 0x6A,
        VK_ADD = 0x6B,
        VK_SEPARATOR = 0x6C,
        VK_SUBTRACT = 0x6D,
        VK_DECIMAL = 0x6E,
        VK_DIVIDE = 0x6F,
        VK_F1 = 0x70,
        VK_F2 = 0x71,
        VK_F3 = 0x72,
        VK_F4 = 0x73,
        VK_F5 = 0x74,
        VK_F6 = 0x75,
        VK_F7 = 0x76,
        VK_F8 = 0x77,
        VK_F9 = 0x78,
        VK_F10 = 0x79,
        VK_F11 = 0x7A,
        VK_F12 = 0x7B,
        VK_F13 = 0x7C,
        VK_F14 = 0x7D,
        VK_F15 = 0x7E,
        VK_F16 = 0x7F,
        VK_F17 = 0x80,
        VK_F18 = 0x81,
        VK_F19 = 0x82,
        VK_F20 = 0x83,
        VK_F21 = 0x84,
        VK_F22 = 0x85,
        VK_F23 = 0x86,
        VK_F24 = 0x87,
        //
        VK_NUMLOCK = 0x90,
        VK_SCROLL = 0x91,
        //
        VK_OEM_NEC_EQUAL = 0x92, // '=' key on numpad
        //
        VK_OEM_FJ_JISHO = 0x92, // 'Dictionary' key
        VK_OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key
        VK_OEM_FJ_TOUROKU = 0x94, // 'Register word' key
        VK_OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key
        VK_OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key
        //
        VK_LSHIFT = 0xA0,
        VK_RSHIFT = 0xA1,
        VK_LCONTROL = 0xA2,
        VK_RCONTROL = 0xA3,
        VK_LMENU = 0xA4,
        VK_RMENU = 0xA5,
        //
        VK_BROWSER_BACK = 0xA6,
        VK_BROWSER_FORWARD = 0xA7,
        VK_BROWSER_REFRESH = 0xA8,
        VK_BROWSER_STOP = 0xA9,
        VK_BROWSER_SEARCH = 0xAA,
        VK_BROWSER_FAVORITES = 0xAB,
        VK_BROWSER_HOME = 0xAC,
        //
        VK_VOLUME_MUTE = 0xAD,
        VK_VOLUME_DOWN = 0xAE,
        VK_VOLUME_UP = 0xAF,
        VK_MEDIA_NEXT_TRACK = 0xB0,
        VK_MEDIA_PREV_TRACK = 0xB1,
        VK_MEDIA_STOP = 0xB2,
        VK_MEDIA_PLAY_PAUSE = 0xB3,
        VK_LAUNCH_MAIL = 0xB4,
        VK_LAUNCH_MEDIA_SELECT = 0xB5,
        VK_LAUNCH_APP1 = 0xB6,
        VK_LAUNCH_APP2 = 0xB7,
        //
        VK_OEM_1 = 0xBA, // ';:' for US
        VK_OEM_PLUS = 0xBB, // '+' any country
        VK_OEM_COMMA = 0xBC, // ',' any country
        VK_OEM_MINUS = 0xBD, // '-' any country
        VK_OEM_PERIOD = 0xBE, // '.' any country
        VK_OEM_2 = 0xBF, // '/?' for US
        VK_OEM_3 = 0xC0, // '`~' for US
        //
        VK_OEM_4 = 0xDB, // '[{' for US
        VK_OEM_5 = 0xDC, // '|' for US
        VK_OEM_6 = 0xDD, // ']}' for US
        VK_OEM_7 = 0xDE, // ''"' for US
        VK_OEM_8 = 0xDF,
        //
        VK_OEM_AX = 0xE1, // 'AX' key on Japanese AX kbd
        VK_OEM_102 = 0xE2, // "<>" or "|" on RT 102-key kbd.
        VK_ICO_HELP = 0xE3, // Help key on ICO
        VK_ICO_00 = 0xE4, // 00 key on ICO
        //
        VK_PROCESSKEY = 0xE5,
        //
        VK_ICO_CLEAR = 0xE6,
        //
        VK_PACKET = 0xE7,
        //
        VK_OEM_RESET = 0xE9,
        VK_OEM_JUMP = 0xEA,
        VK_OEM_PA1 = 0xEB,
        VK_OEM_PA2 = 0xEC,
        VK_OEM_PA3 = 0xED,
        VK_OEM_WSCTRL = 0xEE,
        VK_OEM_CUSEL = 0xEF,
        VK_OEM_ATTN = 0xF0,
        VK_OEM_FINISH = 0xF1,
        VK_OEM_COPY = 0xF2,
        VK_OEM_AUTO = 0xF3,
        VK_OEM_ENLW = 0xF4,
        VK_OEM_BACKTAB = 0xF5,
        //
        VK_ATTN = 0xF6,
        VK_CRSEL = 0xF7,
        VK_EXSEL = 0xF8,
        VK_EREOF = 0xF9,
        VK_PLAY = 0xFA,
        VK_ZOOM = 0xFB,
        VK_NONAME = 0xFC,
        VK_PA1 = 0xFD,
        VK_OEM_CLEAR = 0xFE,
        //
        VK_0 = 0x30,
        VK_1 = 0x31,
        VK_2 = 0x32,
        VK_3 = 0x33,
        VK_4 = 0x34,
        VK_5 = 0x35,
        VK_6 = 0x36,
        VK_7 = 0x37,
        VK_8 = 0x38,
        VK_9 = 0x39
    }

    enum ShowWindowCommands
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,
        /// <summary>
        /// Activates and displays a window. If the window is minimized or 
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window 
        /// for the first time.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2,
        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        Maximize = 3, // is this the right value?
        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>       
        ShowMaximized = 3,
        /// <summary>
        /// Displays a window in its most recent size and position. This value 
        /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
        /// the window is not activated.
        /// </summary>
        ShowNoActivate = 4,
        /// <summary>
        /// Activates the window and displays it in its current size and position. 
        /// </summary>
        Show = 5,
        /// <summary>
        /// Minimizes the specified window and activates the next top-level 
        /// window in the Z order.
        /// </summary>
        Minimize = 6,
        /// <summary>
        /// Displays the window as a minimized window. This value is similar to
        /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
        /// window is not activated.
        /// </summary>
        ShowMinNoActive = 7,
        /// <summary>
        /// Displays the window in its current size and position. This value is 
        /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
        /// window is not activated.
        /// </summary>
        ShowNA = 8,
        /// <summary>
        /// Activates and displays the window. If the window is minimized or 
        /// maximized, the system restores it to its original size and position. 
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9,
        /// <summary>
        /// Sets the show state based on the SW_* value specified in the 
        /// STARTUPINFO structure passed to the CreateProcess function by the 
        /// program that started the application.
        /// </summary>
        ShowDefault = 10,
        /// <summary>
        ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
        /// that owns the window is not responding. This flag should only be 
        /// used when minimizing windows from a different thread.
        /// </summary>
        ForceMinimize = 11
    }
}
