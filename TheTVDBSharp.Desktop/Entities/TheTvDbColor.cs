using System;
using System.Drawing;

namespace TheTVDBSharp.Entities
{
    public class TheTvDbColor : IEquatable<TheTvDbColor>
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbColor() { }

        public TheTvDbColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color GetColor()
        {
            return Color.FromArgb(255, R, G, B);
        }

        public bool Equals(TheTvDbColor other)
        {
            return other != null
                   && R == other.R
                   && G == other.G
                   && B == other.B;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TheTvDbColor);
        }

        public override int GetHashCode()
        {
            return GetColor().GetHashCode();
        }


    }
}
