using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp
{
  public class Cursor : IDisposable {
    internal readonly SDL_CursorPtr cursor;

    internal Cursor(SDL_CursorPtr handle) {
      this.cursor = handle;
    }

    public Cursor(SystemCursor id) {
      this.cursor = ErrorIfInvalid(SDL_CreateSystemCursor(id));
    }

    public Cursor(Surface surface, int hotX, int hotY) {
      this.cursor = ErrorIfInvalid(SDL_CreateColorCursor(surface.surface, hotX, hotY));
    }

    public static Cursor? Current {
      get {
        var ptr = SDL_GetCursor();
        if (ptr == IntPtr.Zero)
          return null;
        return new Cursor(new SDL_CursorPtr(ptr));
      }
      set {
        if (value == null)
          SDL_SetCursor(IntPtr.Zero);
        else
          SDL_SetCursor(value.cursor);
      }
    }

    public static Cursor Default {
      get {
        return new Cursor(new SDL_CursorPtr(ErrorIfNull(SDL_GetDefaultCursor())));
      }
    }

    public static bool Visible {
      get {
        return ErrorIfNegative(SDL_ShowCursor(-1)) == 1;
      }
      set {
        ErrorIfNegative(SDL_ShowCursor(value ? 1 : 0 ));
      }
    }

    public void Dispose() {
      cursor.Dispose();
    }
  }
}
