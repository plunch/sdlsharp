using System;
using System.Drawing;
using System.Runtime.InteropServices;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public partial struct Point {
    public Point(int x, int y) {
      this.x = x;
      this.y = y;
    }

    public void Deconstruct(out int x, out int y) {
      x = this.x;
      y = this.y;
    }

    public static implicit operator System.Drawing.Point(in Point p) {
      return new System.Drawing.Point(p.x, p.y);
    }

    public static implicit operator Point(in System.Drawing.Point p) {
      return new Point(p.X, p.Y);
    }
  }

  public partial struct Rect {

    public Rect(int x, int y, int w, int h) {
      this.x = x;
      this.y = y;
      this.w = w;
      this.h = h;
    }

    public Rect(in Point pt, in Size sz) {
      this.x = pt.x;
      this.y = pt.y;
      this.w = sz.Width;
      this.h = sz.Height;
    }

    public void Deconstruct(out int x, out int y, out int w, out int h) {
      x = this.x;
      y = this.y;
      w = this.w;
      h = this.h;
    }

    public static unsafe bool TryIntersect(in Rect a, in Rect b, out Rect result) {
      fixed(Rect* aptr = &a)
      fixed(Rect* bptr = &b)
        return SDL_IntersectRect(aptr, bptr, out result) == SDL_Bool.True;
    }

    public static unsafe bool Contains(in Point pt, in Rect rect) {
      fixed(Point* ptptr = &pt)
      fixed(Rect* rectptr = &rect)
        return SDL_PointInRect(ptptr, rectptr) == SDL_Bool.True;
    }

    public static unsafe Rect Union(in Rect a, in Rect b) {
      Rect result;
      fixed(Rect* aptr = &a)
      fixed(Rect* bptr = &b)
        SDL_UnionRect(aptr, bptr, out result);
      return result;
    }

    public static unsafe bool IsEmpty(in Rect a) {
      fixed(Rect* ptr = &a)
        return SDL_RectEmpty(ptr) == SDL_Bool.True;
    }

    public static unsafe bool Equals(in Rect a, in Rect b) {
      fixed(Rect* aptr = &a)
      fixed(Rect* bptr = &b)
        return SDL_RectEquals(aptr, bptr) == SDL_Bool.True;
    }

    public static unsafe bool HasIntersection(in Rect a, in Rect b) {
      fixed(Rect* aptr = &a)
      fixed(Rect* bptr = &b)
        return SDL_HasIntersection(aptr, bptr) == SDL_Bool.True;
    }

    public static unsafe bool HasIntersection(in Rect rect, int x1, int y1, int x2, int y2) {
      fixed(Rect* rectptr = &rect)
        return SDL_IntersectRectAndLine(rectptr, &x1, &y1, &x2, &y2) ==  SDL_Bool.True;
    }

    public static unsafe bool HasIntersection(in Rect rect, in Point a, in Point b) {
      fixed(Rect* rectptr = &rect)
      fixed(Point* aptr = &a)
      fixed(Point* bptr = &b)
        return SDL_IntersectRectAndLine(
            rectptr,
            &aptr->x,
            &aptr->y,
            &bptr->x,
            &bptr->y
        ) == SDL_Bool.True;
    }

    public static unsafe Rect EnclosePoints(ReadOnlySpan<Point> points) {
      Rect result;
      fixed (Point* pointptr = &MemoryMarshal.GetReference(points))
        SDL_EnclosePoints(pointptr, points.Length, null, out result);
      return result;
    }

    public static unsafe Rect EnclosePoints(ReadOnlySpan<Point> points, out bool enclosed) {
      Rect result;
      fixed (Point* pointptr = &MemoryMarshal.GetReference(points))
        enclosed = SDL_EnclosePoints(pointptr, points.Length, null, out result) == SDL_Bool.True;
      return result;
    }

    public static unsafe Rect EnclosePoints(ReadOnlySpan<Point> points, in Rect clip) {
      Rect result;
      fixed (Point* pointptr = &MemoryMarshal.GetReference(points))
      fixed (Rect* clipptr = &clip)
        SDL_EnclosePoints(pointptr, points.Length, clipptr, out result);
      return result;
    }

    public static unsafe Rect EnclosePoints(ReadOnlySpan<Point> points, in Rect clip, out bool enclosed) {
      Rect result;
      fixed (Point* pointptr = &MemoryMarshal.GetReference(points))
      fixed (Rect* clipptr = &clip)
        enclosed = SDL_EnclosePoints(pointptr, points.Length, clipptr, out result) == SDL_Bool.True;
      return result;
    }

    public static implicit operator Rectangle(in Rect r) {
      return new Rectangle(r.x, r.y, r.w, r.h);
    }

    public static implicit operator Rect(in Rectangle b) {
      return new Rect(b.X, b.Y, b.Width, b.Height);
    }
  }
}
