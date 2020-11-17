using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public class RendererInfo {
    private SDL_RendererInfo info;

    internal RendererInfo(SDL_RendererInfo info) {
      this.info = info;
    }

    public unsafe string Name => UTF8ToString(info.name);
    public RendererFlags Flags => info.flags;
    public int MaxTextureWidth => info.max_texture_width;
    public int MaxTextureHeight => info.max_texture_height;

    public unsafe RendererInfoFormats Formats => new RendererInfoFormats(this);

    public struct RendererInfoFormats: IReadOnlyList<uint> {
      RendererInfo i;

      internal RendererInfoFormats(RendererInfo i) {
        this.i = i;
      }

      public int Count => (int)i.info.num_texture_formats;

      public unsafe uint this[int index] {
        get {
          return i.info.texture_formats[index];
        }
      }

      IEnumerable<uint> Enumerate() {
        int c = Count;
        for (int i = 0; i < c; ++i)
          yield return this[i];
      }

      public IEnumerator<uint> GetEnumerator() {
        return this.Enumerate().GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.GetEnumerator();
      }
    }
  }
}
