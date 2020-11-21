using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public class Window : SafeHandle {

    protected Window() : base(IntPtr.Zero, true) {
    }

    public Window(IntPtr h, bool owned) : base(IntPtr.Zero, owned) {
      SetHandle(h);
    }

    public UInt32 ID {
      get { return ErrorIfZero(SDL_GetWindowID(this)); }
    }

    public Display Display {
      get { return new Display(ErrorIfNegative(SDL_GetWindowDisplayIndex(this))); }
    }

    public WindowFlags Flags {
      get { return SDL_GetWindowFlags(this); }
    }

    public PixelDataFormat PixelFormat {
      get { return (PixelDataFormat)SDL_GetWindowPixelFormat(this); }
    }

    public unsafe string Title {
      get {
        return UTF8ToString(SDL_GetWindowTitle(this)) ?? "";
      }
      set {
        Span<byte> utf8 = stackalloc byte[Encoding.UTF8.GetByteCount(value)+1];
        var written = Encoding.UTF8.GetBytes(value, utf8);
        utf8[written] = 0;
        fixed (byte* utf8title = utf8) {
          SDL_SetWindowTitle(this, utf8title);
        }
      }
    }

    public Point Position {
      get { 
        int x, y;
        SDL_GetWindowPosition(this, out x, out y);
        return new Point(x, y);
      }
      set {
        SDL_SetWindowPosition(this, value.x, value.y);
      }
    }

    public Size Size {
      get {
        int w, h;
        SDL_GetWindowSize(this, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowSize(this, value.Width, value.Height);
      }
    }

    public float Brightness {
      get { return SDL_GetWindowBrightness(this); }
      set { SDL_SetWindowBrightness(this, value); }
    }

    public float Opacity {
      get { 
        float opacity;
        ErrorIfNegative(SDL_GetWindowOpacity(this, out opacity));
        return opacity;
      }
      set { SDL_SetWindowOpacity(this, value); }
    }

    public unsafe DisplayMode DisplayMode {
      get {
        SDL_DisplayMode mode;
        if (SDL_GetWindowDisplayMode(this, out mode) == 0) {
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
        mode.driverdata = IntPtr.Zero;

        ErrorIfNegative(SDL_SetWindowDisplayMode(this, &mode));
      }
    }

    public bool Resizable {
      get {
        return this.Flags.HasFlag(WindowFlags.Resizable);
      }
      set {
        SDL_SetWindowResizable(this, value ? SDL_Bool.True : SDL_Bool.False);
      }
    }

    public Size MinimumSize {
      get {
        int w, h;
        SDL_GetWindowMinimumSize(this, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowMinimumSize(this, value.Width, value.Height);
      }
    }

    public Size MaximumSize {
      get {
        int w, h;
        SDL_GetWindowMaximumSize(this, out w, out h);
        return new Size(w, h);
      }
      set {
        SDL_SetWindowMaximumSize(this, value.Width, value.Height);
      }
    }

    public bool IsGrabbingInput {
      get {
        return SDL_GetWindowGrab(this) == SDL_Bool.True;
      }
      set {
        SDL_SetWindowGrab(this, value ? SDL_Bool.True : SDL_Bool.False);
      }
    }

    public Surface Surface {
      get {
        return new Surface(ErrorIfNull(SDL_GetWindowSurface(this)), false);
      }
    }

    public void Hide() {
      SDL_HideWindow(this);
    }

    public void Show() {
      SDL_ShowWindow(this);
    }

    public void Maximize() {
      SDL_MaximizeWindow(this);
    }

    public void Minimize() {
      SDL_MinimizeWindow(this);
    }

    public void Raise() {
      SDL_RaiseWindow(this);
    }

    public void Restore() {
      SDL_RaiseWindow(this);

    }
    public void UpdateSurface() {
      ErrorIfNegative(SDL_UpdateWindowSurface(this));
    }

    public unsafe void UpdateSurfaceRects(ReadOnlySpan<Rect> rects) {
      fixed (Rect* ptr = &MemoryMarshal.GetReference(rects)) {
        ErrorIfNegative(SDL_UpdateWindowSurfaceRects(this, ptr, rects.Length));
      }
    }

    public void SetBordered(bool bordered) {
      SDL_SetWindowBordered(this, bordered ? SDL_Bool.True : SDL_Bool.False);
    }

    public void SetFullscreen(WindowFlags flags) {
      ErrorIfNegative(SDL_SetWindowFullscreen(this, flags));
    }

    public void SetModalFor(Window parent) {
      ErrorIfNegative(SDL_SetWindowModalFor(this, parent));
    }

    public void SetIcon(Surface surface) {
      SDL_SetWindowIcon(this, surface);
    }

    public unsafe void GetDisplayGammaRamp(GammaRamp ramp) {
      fixed(ushort* rp = ramp.R != null ? ramp.R.AsSpan() : null)
      fixed(ushort* gp = ramp.G != null ? ramp.G.AsSpan() : null)
      fixed(ushort* bp = ramp.B != null ? ramp.B.AsSpan() : null)
        SDL_GetWindowGammaRamp(this, rp, gp, bp);
    }

    public GammaRamp GetDisplayGammaRamp() {
      var ramp = new GammaRamp() {
        R = new GammaRampChannel(),
        G = new GammaRampChannel(),
        B = new GammaRampChannel(),
      };
      GetDisplayGammaRamp(ramp);
      return ramp;
    }

    public unsafe void SetDisplayGammaRamp(GammaRamp ramp) {
      fixed(ushort* rp = ramp.R != null ? ramp.R.AsSpan() : null)
      fixed(ushort* gp = ramp.G != null ? ramp.G.AsSpan() : null)
      fixed(ushort* bp = ramp.B != null ? ramp.B.AsSpan() : null)
        SDL_SetWindowGammaRamp(this,rp, gp, bp);
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    override protected bool ReleaseHandle() {
      NativeMethods.SDL_DestroyWindow(this.handle);
      return true;
    }

    public static Window FromID(uint id) {
      var ptr = ErrorIfNull(SDL_GetWindowFromID(id));
      return new Window(ptr, false);
    }

    public static Window? CurrentlyGrabbingInput() {
      var ptr = SDL_GetGrabbedWindow();
      if (ptr != IntPtr.Zero)
        return new Window(ptr, false);
      else
        return null;
    }

    public static int UndefinedPosition => WINDOWPOS_UNDEFINED;
    public static int CenteredPosition => WINDOWPOS_CENTERED;

    public unsafe static Window Create(string title, int x, int y, int w, int h, WindowFlags flags) {
      Span<byte> utf8 = stackalloc byte[SL(title)];
      var written = Encoding.UTF8.GetBytes(title, utf8);
      utf8[written] = 0;
      fixed (byte* utf8title = utf8) {
        return ErrorIfNull(SDL_CreateWindow(utf8title, x, y, w, h, flags));
      }
    }
  }
}
