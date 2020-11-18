using System;
using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {

    [DllImport("SDL2")]
    public static extern int SDL_VideoInit(/*const char*/ byte* driver_name);

    [DllImport("SDL2")]
    public static extern void SDL_VideoQuit();

    [DllImport("SDL2")]
    public static extern SDL_WindowPtr SDL_CreateWindow(
        /*const char*/ byte* title,
        int x,
        int y,
        int w,
        int h,
        WindowFlags flags
    );

    [DllImport("SDL2")]
    public static extern SDL_WindowPtr SDL_CreateWindowFrom(/*const*/ void* data);

    [DllImport("SDL2")]
    public static extern int SDL_CreateWindowAndRenderer(
        int width,
        int height,
        WindowFlags window_flags,
        out SDL_WindowPtr window,
        out SDL_RendererPtr renderer
    );

    [DllImport("SDL2")]
    public static extern void SDL_DestroyWindow(IntPtr window);

    [DllImport("SDL2")]
    public static extern void SDL_DisableScreenSaver();

    [DllImport("SDL2")]
    public static extern void SDL_EnableScreenSaver();

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_IsScreenSaverEnabled();

    [DllImport("SDL2")]
    public static extern SDL_DisplayMode* SDL_GetClosestDisplayMode(
      int displayIndex,
      /*const*/ SDL_DisplayMode* mode,
      out SDL_DisplayMode closest
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out SDL_DisplayMode mode);

    [DllImport("SDL2")]
    public static extern /*constchar */ byte* SDL_GetCurrentVideoDriver();

    [DllImport("SDL2")]
    public static extern int SDL_GetDesktopDisplayMode(
      int displayIndex,
      out SDL_DisplayMode mode
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetDisplayBounds(
      int displayIndex,
      out Rect rect
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetDisplayDPI(
      int displayIndex,
      out float ddpi,
      out float hdpi,
      out float vdpi
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetDisplayMode(
      int displayIndex,
      int modeIndex,
      out SDL_DisplayMode mode
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetDisplayUsableBounds(
      int displayIndex,
      out Rect rect
    );

    [DllImport("SDL2")]
    public static extern /*const char*/ byte* SDL_GetDisplayName(
      int displayIndex
    );

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetGrabbedWindow();

    [DllImport("SDL2")]
    public static extern int SDL_GetNumDisplayModes(int displayIndex);

    [DllImport("SDL2")]
    public static extern int SDL_GetNumVideoDisplays();

    [DllImport("SDL2")]
    public static extern int SDL_GetNumVideoDrivers();

    [DllImport("SDL2")]
    public static extern /*const char*/ byte* SDL_GetVideoDriver(int index);

    [DllImport("SDL2")]
    public static extern int SDL_GetWindowBordersSize(
      SDL_WindowPtr window,
      out int top,
      out int left,
      out int bottom,
      out int right
    );

    [DllImport("SDL2")]
    public static extern float SDL_GetWindowBrightness(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void* SDL_GetWindowData(
      SDL_WindowPtr window,
      /*const*/ char* name
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetWindowDisplayIndex(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetWindowDisplayMode(
      SDL_WindowPtr window,
      out SDL_DisplayMode mode
    );

    [DllImport("SDL2")]
    public static extern WindowFlags SDL_GetWindowFlags(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern IntPtr SDL_GetWindowFromID(UInt32 id);

    [DllImport("SDL2")]
    public static extern int SDL_GetWindowGammaRamp(
      SDL_WindowPtr window,
      out ushort red,
      out ushort green,
      out ushort blue
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_GetWindowGrab(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern UInt32 SDL_GetWindowID(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_GetWindowMaximumSize(
      SDL_WindowPtr window,
      out int w,
      out int h
    );
      
    [DllImport("SDL2")]
    public static extern void SDL_GetWindowMinimumSize(
      SDL_WindowPtr window,
      out int w,
      out int h
    );

    [DllImport("SDL2")]
    public static extern int SDL_GetWindowOpacity(
      SDL_WindowPtr window,
      out float opacity
    );

    [DllImport("SDL2")]
    public static extern UInt32 SDL_GetWindowPixelFormat(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_GetWindowPosition(
      SDL_WindowPtr window,
      out int x,
      out int y
    );

    [DllImport("SDL2")]
    public static extern void SDL_GetWindowSize(
      SDL_WindowPtr window,
      out int w,
      out int h
    );

    [DllImport("SDL2")]
    public static extern /*const char*/ byte* SDL_GetWindowTitle(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_HideWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_ShowWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_MaximizeWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_MinimizeWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_RaiseWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_RestoreWindow(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowBordered(
      SDL_WindowPtr window,
      SDL_Bool bordered
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowBrightness(
      SDL_WindowPtr window,
      float brightness
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowData(
      SDL_WindowPtr window,
      /*const char*/ byte* name,
      void* userdata
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetWindowDisplayMode(
      SDL_WindowPtr window,
      SDL_DisplayMode* mode
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetWindowFullscreen(
      SDL_WindowPtr window,
      WindowFlags flags
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowGammaRamp(
      SDL_WindowPtr window,
      /*const*/ ushort* red,
      /*const*/ ushort* green,
      /*const*/ ushort* blue
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowGrab(
      SDL_WindowPtr window,
      SDL_Bool grabbed
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowHitTest(
      SDL_WindowPtr window,
      SDL_HitTest callback,
      void* callback_data
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowIcon(
      SDL_WindowPtr window,
      SDL_SurfacePtr icon
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetWindowInputFocus(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowMaximumSize(
      SDL_WindowPtr window,
      int max_w,
      int max_h
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowMinimumSize(
      SDL_WindowPtr window,
      int min_w,
      int min_h
    );

    [DllImport("SDL2")]
    public static extern int SDL_SetWindowModalFor(
      SDL_WindowPtr modal_window,
      SDL_WindowPtr parent_window
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowOpacity(
      SDL_WindowPtr window,
      float opacity
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowPosition(
      SDL_WindowPtr window,
      int x,
      int y
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowResizable(
      SDL_WindowPtr window,
      SDL_Bool resizable
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowSize(
      SDL_WindowPtr window,
      int w,
      int h
    );

    [DllImport("SDL2")]
    public static extern void SDL_SetWindowTitle(
      SDL_WindowPtr window,
      /*const char*/ byte* title
    );

    [DllImport("SDL2")]
    public static extern int SDL_ShowMessageBox(
      /*const*/ SDL_MessageBoxData* messageboxdata,
      out int buttonid
    );

    [DllImport("SDL2")]
    public static extern int SDL_ShowSimpleMessageBox(
      UInt32 flags,
      /*const char*/ byte* title,
      /*const char*/ byte* message,
        SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern int SDL_UpdateWindowSurface(
      SDL_WindowPtr window
    );

    [DllImport("SDL2")]
    public static extern int SDL_UpdateWindowSurfaceRects(
      SDL_WindowPtr window,
      /*const*/ Rect* rects,
      int numrects
    );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SDL_HitTestResult SDL_HitTest(
      SDL_WindowPtr window,
      /*const*/ Point* area,
      void* data
    );

    public enum SDL_HitTestResult {
      Normal,
      Draggable,
      ResizeTopLeft,
      RezizeTop,
      ResizeTopRight,
      ResizeBottom,
      ResizeBottomLeft,
      ResizeLeft,
    }

    static readonly uint WINDOWPOS_UNDEFINED = 0x1FFF0000u;
    static readonly uint WINDOWPOS_CENTERED = 0x2FFF0000u;

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DisplayMode {
      public UInt32 format;
      public int w, h;
      public int refresh_rate;
      public void* driverdata;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxButtonData {
      public UInt32 flags;
      public int buttonid;
      public /*const char*/ byte* text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxColor {
      public byte r, g, b;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxColorScheme {
      public fixed byte bytes[5 * 3];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxData {
      public UInt32 flags;
      public IntPtr window;
      public /*const*/ char* title;
      public /*const*/ char* message;
      public int numbuttons;
      public /*const*/ SDL_MessageBoxButtonData* buttons;
      public /*const*/ SDL_MessageBoxColorScheme* colorScheme;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_WindowEvent {
      public UInt32 type, timestamp, windowID;
      public byte @event;
      public int data1, data2;
    }
  }

  class SDL_WindowPtr : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid {
    private SDL_WindowPtr() : base(true) {
    }

    internal SDL_WindowPtr(IntPtr ptr) : base(false) {
      SetHandle(ptr);
    }

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_DestroyWindow(this.handle);
      return true;
    }
  }

  public enum SDL_MessageBoxButtonFlags : UInt32 {
    ReturnkeyDefault = 0,
    EscapekeyDefault = 1,
  }

  [Flags]
  public enum WindowFlags : UInt32 {
    None = 0,
    Fullscreen = 0x1,
    OpenGL = 0x2,
    Shown = 0x4,
    Hidden = 0x8,
    Borderless = 0x10,
    Resizable = 0x20,
    Minimized = 0x40,
    Maximized = 0x80,
    InputGrabbed = 0x100,
    InputFocus = 0x200,
    MouseFocus = 0x400,
    FullscreenDesktop = Fullscreen|0x1000,
    Foreign = 0x800,
    AllowHighDPI = 0x2000,
    MouseCapture = 0x4000,
    AlwaysOnTop = 0x8000,
    SkipTaskbar = 0x10000,
    Utility = 0x20000,
    Tooltip = 0x40000,
    PopupMenu = 0x80000,
    Vulkan = 0x10000000,
    Metal = 0x20000000,
  }

  public enum DisplayOrientation { 
    Unknown,
    Landscape,
    LandscapeFlipped,
    Portrait,
    PortraitFlipped,
  }
}
