using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public class Renderer : IDisposable {

    public static RenderDrivers Drivers { get; } = new RenderDrivers();

    readonly internal SDL_RendererPtr renderer;

    internal Renderer(SDL_RendererPtr renderer) {
      this.renderer = renderer;
    }


    public RendererInfo Info {
      get {
        SDL_RendererInfo info;
        SDL_GetRendererInfo(renderer, out info);
        return new RendererInfo(info);
      }
    }

    public Texture? Target {
      get {
        var p = SDL_GetRenderTarget(renderer);
        if (p == IntPtr.Zero)
          return null;
        else
          return new Texture(new SDL_TexturePtr(p));
      }
      set {
        if (value == null)
          SDL_SetRenderTarget(renderer, IntPtr.Zero);
        else
          SDL_SetRenderTarget(renderer, value.texture);
      }
    }


    public BlendMode BlendMode {
      get {
        BlendMode mode;
        ErrorIfNegative(SDL_GetRenderDrawBlendMode(renderer, out mode));
        return mode;
      }
      set {
        ErrorIfNegative(SDL_SetRenderDrawBlendMode(renderer, value));
      }
    }

    public Color Color {
      get {
        Color c;
        ErrorIfNegative(SDL_GetRenderDrawColor(renderer, out c.r, out c.g, out c.b, out c.a));
        return c;
      }
      set { 
        ErrorIfNegative(SDL_SetRenderDrawColor(renderer, value.r, value.g, value.b, value.a));
      }
    }

    public System.Drawing.SizeF Scale {
      get {
        float w, h;
        SDL_RenderGetScale(renderer, out w, out h);
        return new System.Drawing.SizeF(w, h);
      }
      set {
        ErrorIfNegative(SDL_RenderSetScale(renderer, value.Width, value.Height));
      }
    }

    public unsafe Rect ClipRect {
      get {
        Rect clip;
        SDL_RenderGetClipRect(renderer, out clip);
        return clip;
      }
      set {
        ErrorIfNegative(SDL_RenderSetClipRect(renderer, Rect.IsEmpty(value) ? null : &value));
      }
    }

    public unsafe Rect Viewport {
      get {
        Rect clip;
        SDL_RenderGetViewport(renderer, out clip);
        return clip;
      }
      set {
        ErrorIfNegative(SDL_RenderSetViewport(renderer, Rect.IsEmpty(value) ? null : &value));
      }
    }

    public unsafe bool IntegerScale {
      get {
        var forced = SDL_RenderGetIntegerScale(renderer);
        if (forced == SDL_Bool.False) {
          var err = GetError();
          if (err != null)
            throw err;
        }
        return forced == SDL_Bool.True;
      }
      set {
        ErrorIfNegative(SDL_RenderSetIntegerScale(renderer, value ? SDL_Bool.True : SDL_Bool.False));
      }
    }

    public System.Drawing.Size OutputSize {
      get {
        int w, h;
        ErrorIfNegative(SDL_GetRendererOutputSize(renderer, out w, out h));
        return new System.Drawing.Size(w, h);
      }
    }

    public System.Drawing.Size LogicalSize {
      get {
        int w, h;
        SDL_RenderGetLogicalSize(renderer, out w, out h);
        return new System.Drawing.Size(w, h);
      }
      set {
        ErrorIfNegative(SDL_RenderSetLogicalSize(renderer, value.Width, value.Height));
      }
    }

    public void Clear() {
      ErrorIfNegative(SDL_RenderClear(renderer));
    }

    public void Present() {
      SDL_RenderPresent(renderer);
    }

    public unsafe void Copy(
      Texture texture,
      in Rect dst,
      double angle = 0,
      RendererFlip flip = RendererFlip.None
    ) {
      fixed (Rect* ptr = &dst) {
        if (angle != 0 || flip !=  RendererFlip.None)
          SDL_RenderCopyEx(renderer, texture.texture, null, ptr, angle, null, flip);
        else
          SDL_RenderCopy(renderer, texture.texture, null, ptr);
      }
    }

    public unsafe void Copy(
      Texture texture,
      in Rect src,
      in Rect dst,
      double angle = 0,
      RendererFlip flip = RendererFlip.None
    ) {
      fixed (Rect* ptr = &dst)
      fixed (Rect* sptr = &src) {
        if (angle != 0 || flip !=  RendererFlip.None)
          SDL_RenderCopyEx(renderer, texture.texture, sptr, ptr, angle, null, flip);
        else
          SDL_RenderCopy(renderer, texture.texture, sptr, ptr);
      }
    }

    public unsafe void Copy(
      Texture texture,
      in Rect dst,
      in Point center,
      double angle = 0,
      RendererFlip flip = RendererFlip.None
    ) {
      fixed (Point* pt = &center)
      fixed (Rect* ptr = &dst) {
        SDL_RenderCopyEx(renderer, texture.texture, null, ptr, angle, pt, flip);
      }
    }

    public unsafe void Copy(
      Texture texture,
      in Rect src,
      in Rect dst,
      in Point center,
      double angle = 0,
      RendererFlip flip = RendererFlip.None
    ) {
      fixed (Point* pt = &center)
      fixed (Rect* ptr = &dst)
      fixed (Rect* sptr = &src) {
        SDL_RenderCopyEx(renderer, texture.texture, sptr, ptr, angle, pt, flip);
      }
    }

    public void DrawLine(Point a, Point b)
      => DrawLine(a.x, a.y, b.x, b.y);

    public void DrawLine(int x1, int y1, int x2, int y2) {
      ErrorIfNegative(SDL_RenderDrawLine(renderer, x1, y2, x2, y2));
    }

    public unsafe void DrawLines(ReadOnlySpan<Point> points) {
      fixed (Point* ptr = &System.Runtime.InteropServices.MemoryMarshal.GetReference(points))
        ErrorIfNegative(SDL_RenderDrawLines(renderer, ptr, points.Length));
    }

    public void DrawPoint(Point p)
      => DrawPoint(p.x, p.y);

    public void DrawPoint(int x, int y) {
      ErrorIfNegative(SDL_RenderDrawPoint(renderer, x, y));
    }

    public unsafe void DrawPoints(ReadOnlySpan<Point> points) {
      fixed (Point* ptr = &System.Runtime.InteropServices.MemoryMarshal.GetReference(points))
        ErrorIfNegative(SDL_RenderDrawPoints(renderer, ptr, points.Length));
    }

    public unsafe void DrawRect(Rect rect) {
      ErrorIfNegative(SDL_RenderDrawRect(renderer, &rect));
    }

    public unsafe void DrawRect(int x, int y, int w, int h) {
      Rect r;
      r.x = x;
      r.y = y;
      r.w = w;
      r.h = h;
      ErrorIfNegative(SDL_RenderDrawRect(renderer, &r));
    }

    public unsafe void DrawRects(ReadOnlySpan<Rect> rect) {
      fixed (Rect* ptr = &System.Runtime.InteropServices.MemoryMarshal.GetReference(rect))
        ErrorIfNegative(SDL_RenderDrawRects(renderer, ptr, rect.Length));
    }

    public unsafe void FillRect(Rect rect) {
      SDL_RenderFillRect(renderer, &rect);
    }

    public unsafe void FillRects(ReadOnlySpan<Rect> rect) {
      fixed (Rect* ptr = &System.Runtime.InteropServices.MemoryMarshal.GetReference(rect))
        ErrorIfNegative(SDL_RenderFillRects(renderer, ptr, rect.Length));
    }

    public void Dispose() {
      renderer.Dispose();
    }

    public static Renderer Create(Window window, int index, RendererFlags flags)
      => new Renderer(ErrorIfInvalid(SDL_CreateRenderer(window.handle, index, flags)));

    public Texture CreateTexture(Surface surface) {
      return new Texture(ErrorIfInvalid(SDL_CreateTextureFromSurface(renderer, surface.surface)));
    }
    public Texture CreateTexture(uint format, TextureAccess access, int width, int height) {
      return new Texture(ErrorIfInvalid(SDL_CreateTexture(renderer, format, access, width, height)));
    }
  }
}
