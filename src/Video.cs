using System;
using System.Collections.Generic;
using System.Text;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public static class Video {
    public static IEnumerable<Display> Displays => Display.All();

    public static unsafe void Init() {
      ErrorIfNegative(SDL_VideoInit(null));
    }

    public static unsafe void Init(string driverName) {
      Span<byte> utf8 = stackalloc byte[Encoding.UTF8.GetByteCount(driverName)+1];
      var written = Encoding.UTF8.GetBytes(driverName, utf8);
      utf8[written] = 0;
      fixed (byte* utf8driver = &System.Runtime.InteropServices.MemoryMarshal.GetReference(utf8))
        ErrorIfNegative(SDL_VideoInit(utf8driver));
    }

    public static void Quit() {
      SDL_VideoQuit();
    }

    public static bool ScreensaverEnabled {
      get { return SDL_IsScreenSaverEnabled() == SDL_Bool.True; }
      set {
        if (value)
          SDL_EnableScreenSaver();
        else
          SDL_DisableScreenSaver();
      }
    }

    public static Window CreateWindow(string title, int x, int y, int w, int h, WindowFlags flags)
      => Window.Create(title, x, y, w, h, flags);

    public static (Window window, Renderer renderer) CreateWindowAndRenderer(int width, int height, WindowFlags flags) {
      SDL_WindowPtr window;
      SDL_RendererPtr renderer;
      ErrorIfNegative(SDL_CreateWindowAndRenderer(width, height, flags, out window, out renderer));
      return (new Window(window), new Renderer(renderer));
    }
  }
}
