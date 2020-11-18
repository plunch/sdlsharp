using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public class Window : IDisposable {
    internal SDL_WindowPtr handle;

    internal Window(SDL_WindowPtr handle) {
      this.handle = handle;
    }

    public UInt32 ID {
      get { return ErrorIfZero(SDL_GetWindowID(handle)); }
    }

    public Display Display {
      get { return new Display(ErrorIfNegative(SDL_GetWindowDisplayIndex(handle))); }
    }

    public WindowFlags Flags {
      get { return SDL_GetWindowFlags(handle); }
    }

    public PixelDataFormat PixelFormat {
      get { return (PixelDataFormat)SDL_GetWindowPixelFormat(handle); }
    }

    public unsafe string Title {
      get {
        return UTF8ToString(SDL_GetWindowTitle(handle)) ?? "";
      }
      set {
        Span<byte> utf8 = stackalloc byte[Encoding.UTF8.GetByteCount(value)+1];
        var written = Encoding.UTF8.GetBytes(value, utf8);
        utf8[written] = 0;
        fixed (byte* utf8title = &MemoryMarshal.GetReference(utf8)) {
          SDL_SetWindowTitle(handle, utf8title);
        }
      }
    }

    public Point Position {
      get { 
        int x, y;
        SDL_GetWindowPosition(handle, out x, out y);
        return new Point(x, y);
      }
      set {
        SDL_SetWindowPosition(handle, value.x, value.y);
      }
    }

    public Size Size {
      get {
        int w, h;
        SDL_GetWindowSize(handle, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowSize(handle, value.Width, value.Height);
      }
    }

    public float Brightness {
      get { return SDL_GetWindowBrightness(handle); }
      set { SDL_SetWindowBrightness(handle, value); }
    }

    public float Opacity {
      get { 
        float opacity;
        ErrorIfNegative(SDL_GetWindowOpacity(handle, out opacity));
        return opacity;
      }
      set { SDL_SetWindowOpacity(handle, value); }
    }

    public unsafe DisplayMode DisplayMode {
      get {
        SDL_DisplayMode mode;
        if (SDL_GetWindowDisplayMode(handle, out mode) == 0) {
          return new DisplayMode(mode);
        } else {
          throw GetError2();
        }
      }
      set {
        SDL_DisplayMode mode;
        mode.format = (uint)value.Format;
        mode.w = value.Width;
        mode.h = value.Height;
        mode.refresh_rate = value.RefreshRate;
        mode.driverdata = null;

        ErrorIfNegative(SDL_SetWindowDisplayMode(handle, &mode));
      }
    }

    public bool Resizable {
      get {
        return this.Flags.HasFlag(WindowFlags.Resizable);
      }
      set {
        SDL_SetWindowResizable(handle, value ? SDL_Bool.True : SDL_Bool.False);
      }
    }

    public Size MinimumSize {
      get {
        int w, h;
        SDL_GetWindowMinimumSize(handle, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowMinimumSize(handle, value.Width, value.Height);
      }
    }

    public Size MaximumSize {
      get {
        int w, h;
        SDL_GetWindowMaximumSize(handle, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowMaximumSize(handle, value.Width, value.Height);
      }
    }

    public bool IsGrabbingInput {
      get {
        return SDL_GetWindowGrab(handle) == SDL_Bool.True;
      }
      set {
        SDL_SetWindowGrab(handle, value ? SDL_Bool.True : SDL_Bool.False);
      }
    }

    public void Hide() {
      SDL_HideWindow(handle);
    }

    public void Show() {
      SDL_ShowWindow(handle);
    }

    public void Maximize() {
      SDL_MaximizeWindow(handle);
    }

    public void Minimize() {
      SDL_MinimizeWindow(handle);
    }

    public void Raise() {
      SDL_RaiseWindow(handle);
    }

    public void Restore() {
      SDL_RaiseWindow(handle);

    }
    public void UpdateSurface() {
      ErrorIfNegative(SDL_UpdateWindowSurface(handle));
    }

    public unsafe void UpdateSurfaceRects(ReadOnlySpan<Rect> rects) {
      fixed (Rect* ptr = &System.Runtime.InteropServices.MemoryMarshal.GetReference(rects)) {
        ErrorIfNegative(SDL_UpdateWindowSurfaceRects(handle, ptr, rects.Length));
      }
    }

    public void SetBordered(bool bordered) {
      SDL_SetWindowBordered(handle, bordered ? SDL_Bool.True : SDL_Bool.False);
    }

    public void SetFullscreen(WindowFlags flags) {
      ErrorIfNegative(SDL_SetWindowFullscreen(handle, flags));
    }

    public void SetModalFor(Window parent) {
      ErrorIfNegative(SDL_SetWindowModalFor(handle, parent.handle));
    }

    public void SetIcon(Surface surface) {
      SDL_SetWindowIcon(handle, surface.surface);
   }

    public void Dispose() {
      handle.Dispose();
    }

    public static Window FromID(uint id) {
      var ptr = ErrorIfNull(SDL_GetWindowFromID(id));
      return new Window(new SDL_WindowPtr(ptr));
    }

    public static Window? CurrentlyGrabbingInput() {
      var ptr = SDL_GetGrabbedWindow();
      if (ptr != IntPtr.Zero)
        return new Window(new SDL_WindowPtr(ptr));
      else
        return null;
    }

    public static Window? Focused() {
      var ptr = SDL_GetKeyboardFocus();
      if (ptr != IntPtr.Zero)
        return new Window(new SDL_WindowPtr(ptr));
      else
        return null;
    }

    public static int UndefinedPosition => WINDOWPOS_UNDEFINED;
    public static int CenteredPosition => WINDOWPOS_CENTERED;

    public unsafe static Window Create(string title, int x, int y, int w, int h, WindowFlags flags) {
      Span<byte> utf8 = stackalloc byte[Encoding.UTF8.GetByteCount(title)+1];
      var written = Encoding.UTF8.GetBytes(title, utf8);
      utf8[written] = 0;
      fixed (byte* utf8title = &System.Runtime.InteropServices.MemoryMarshal.GetReference(utf8)) {
        var handle = ErrorIfNull(SDL_CreateWindow(utf8title, x, y, w, h, flags));
        return new Window(handle);
      }
    }
  }
}
