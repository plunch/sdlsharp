using System;
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

    public IReadOnlyList<uint> Formats => info.texture_formats.Take((int)info.num_texture_formats).ToList();
  }
}
