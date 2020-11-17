using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using static SDLSharp.NativeMethods;
using System.Runtime.InteropServices;

namespace SDLSharp {
  public class Palette : IDisposable, IReadOnlyList<Color> {
    internal readonly SDL_PalettePtr palette;

    public Palette(int count) {
      this.palette = ErrorIfInvalid(SDL_AllocPalette(count));
    }

    internal Palette(SDL_PalettePtr ptr) {
      this.palette = ptr;
    }

    public unsafe int Count {
      get {
        var ptr = (SDL_Palette*)palette.DangerousGetHandle();
        return ptr->ncolors;
      }
    }

    public unsafe Color this[int index] {
      get {
        var ptr = (SDL_Palette*)palette.DangerousGetHandle();
        if (index < 0 || index > ptr->ncolors)
          throw new IndexOutOfRangeException();
        return ptr->colors[index];
      }
      set{
        var ptr = (SDL_Palette*)palette.DangerousGetHandle();
      }
    }

    public unsafe void CopyFrom(ReadOnlySpan<Color> colors, int start, int length) {
      if (start + length > colors.Length)
        throw new IndexOutOfRangeException();

      fixed(Color* ptr = &MemoryMarshal.GetReference(colors)) {
        SDL_SetPaletteColors(palette, ptr, start, length);
      }
    }

    public void Dispose() {
      palette.Dispose();
    }


    IEnumerable<Color> Enumerate() {
      int idx = 0;
      while(true) {
        if (idx++ < Count)
          yield return this[idx];
        else
          break;
      }
    }

    public IEnumerator<Color> GetEnumerator() {
      return Enumerate().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return this.GetEnumerator();
    }
  }
}
