namespace TheTVDBSharp.Entities
{
    public class TheTvDbColor
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

        public bool Equals(TheTvDbColor other)
        {
            return other != null
                   && R == other.R
                   && G == other.G
                   && B == other.B;
        }

    }
}
