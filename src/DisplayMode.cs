﻿using System;
using static SDLSharp.NativeMethods;

namespace SDLSharp
{
  public class DisplayMode {
    public UInt32 Format { get; }
    public int Width { get; }
    public int Height { get; }
    public int RefreshRate { get; }
    public IntPtr DriverData { get; }

    public DisplayMode(UInt32 format, int width, int height, int refreshRate, IntPtr driverData) {
      this.Format = format;
      this.Width = width;
      this.Height = height;
      this.RefreshRate = refreshRate;
      this.DriverData = driverData;
    }

    internal unsafe DisplayMode(SDL_DisplayMode mode) 
      : this(mode.format,
            mode.w,
            mode.h,
            mode.refresh_rate,
            (IntPtr)mode.driverdata) {}
  }
}
