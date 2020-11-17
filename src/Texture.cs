using System;
using System.Text;
using System.Drawing;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public class Texture : IDisposable {
    internal readonly SDL_TexturePtr texture;

    internal Texture(SDL_TexturePtr ptr) {
      this.texture = ptr;
    }

    public unsafe int Width {
      get {
        int w;
        ErrorIfNegative(SDL_QueryTexture(texture, null, null, &w, null));
        return w;
      }
    }

    public unsafe int Height {
      get {
        int h;
        ErrorIfNegative(SDL_QueryTexture(texture, null, null, null, &h));
        return h;
      }
    }

    public unsafe System.Drawing.Size Size {
      get {
        int w, h;
        ErrorIfNegative(SDL_QueryTexture(texture, null, null, &w, &h));
        return new System.Drawing.Size(w, h);
      }
    }

    public unsafe TextureAccess Access {
      get {
        TextureAccess access;
        ErrorIfNegative(SDL_QueryTexture(texture, null, &access, null, null));
        return access;
      }
    }

    public unsafe uint Format {
      get {
        uint format;
        ErrorIfNegative(SDL_QueryTexture(texture, &format, null, null, null));
        return format;
      }
    }

    public byte AlphaMod {
      get {
        byte alpha;
        ErrorIfNegative(SDL_GetTextureAlphaMod(texture, out alpha));
        return alpha;
      }
      set {
        ErrorIfNegative(SDL_SetTextureAlphaMod(texture, value));
      }
    }

    public Color ColorMod {
      get {
        byte r, g, b;
        ErrorIfNegative(SDL_GetTextureColorMod(texture, out r, out g, out b));
        return new Color(r, g, b);
      }
      set {
        ErrorIfNegative(SDL_SetTextureColorMod(texture, value.r, value.g, value.b));
      }
    }

    public BlendMode BlendMode {
      get {
        BlendMode mode;
        ErrorIfNegative(SDL_GetTextureBlendMode(texture, out mode));
        return mode;
      }
      set {
        ErrorIfNegative(SDL_SetTextureBlendMode(texture, value));
      }
    }

    public void Dispose() {
      texture.Dispose();
    }
  }
}
