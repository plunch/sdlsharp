using System.Runtime.InteropServices;

namespace SDLSharp {
  static unsafe partial class NativeMethods {
    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_EnclosePoints(
      /*const*/ Point* points,
      int count,
      /*const*/ Rect* clip,
      out Rect result
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_HasIntersection(
      /*const*/ Rect* a,
      /*const*/ Rect* b
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_IntersectRect(
      /*const*/ Rect* a,
      /*const*/ Rect* b,
      out Rect result
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_IntersectRectAndLine(
      /*const*/ Rect* rect,
      int* x1,
      int* y1,
      int* x2,
      int* y2
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_PointInRect(
      /*const*/ Point* p,
      /*const*/ Rect* r
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_RectEmpty(
      /*const*/ Rect* r
    );

    [DllImport("SDL2")]
    public static extern SDL_Bool SDL_RectEquals(
      /*const*/ Rect* a,
      /*const*/ Rect* b
    );

    [DllImport("SDL2")]
    public static extern void SDL_UnionRect(
      /*const*/ Rect* a,
      /*const*/ Rect* b,
      out Rect result
    );
  }

  [StructLayout(LayoutKind.Sequential)]
  public partial struct Point {
    public int x, y;
  }

  [StructLayout(LayoutKind.Sequential)]
  public partial struct Rect {
    public int x, y, w, h;
  }
}
