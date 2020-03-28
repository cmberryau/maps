namespace Maps
{
    /// <summary>
    /// Immutable color with 32 bit depth per channel
    /// </summary>
    public struct Colorf
    {
        /// <summary>
        /// The red component
        /// </summary>
        public readonly float r;

        /// <summary>
        /// The green component
        /// </summary>
        public readonly float g;

        /// <summary>
        /// The blue component
        /// </summary>
        public readonly float b;

        /// <summary>
        /// The alpha component
        /// </summary>
        public readonly float a;

        /// <summary>
        /// Solid red
        /// </summary>
        public static readonly Colorf Red = new Colorf(1f, 0f, 0f, 1f);

        /// <summary>
        /// Solid green
        /// </summary>
        public static readonly Colorf Green = new Colorf(0f, 1f, 0f, 1f);

        /// <summary>
        /// Solid blue
        /// </summary>
        public static readonly Colorf Blue = new Colorf(0f, 0f, 1f, 1f);

        /// <summary>
        /// Solid white
        /// </summary>
        public static readonly Colorf White = new Colorf(1f, 1f, 1f, 1f);

        /// <summary>
        /// Solid black
        /// </summary>
        public static readonly Colorf Black = new Colorf(0f, 0f, 0f, 1f);

        /// <summary>
        /// Solid yellow
        /// </summary>
        public static readonly Colorf Yellow = new Colorf(1f, 1f, 0f, 1f);

        /// <summary>
        /// Solid cyan
        /// </summary>
        public static readonly Colorf Cyan = new Colorf(0f, 1f, 1f, 1f);

        /// <summary>
        /// Solid magenta
        /// </summary>
        public static readonly Colorf Magenta = new Colorf(1f, 0f, 1f, 1f);

        /// <summary>
        /// Grey
        /// </summary>
        public static readonly Colorf Grey = new Colorf(0.5f, 0.5f, 0.5f, 1f);

        /// <summary>
        /// Clear
        /// </summary>
        public static readonly Colorf Clear = new Colorf(0f, 0f, 0f, 0f);

        /// <summary>
        /// Initializes a new instance of Color
        /// </summary>
        /// <param name="r">The red component</param>
        /// <param name="g">The green component</param>
        /// <param name="b">The blue component</param>
        /// <param name="a">The alpha component</param>
        public Colorf(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// Initializes a new instance of Color, alpha set to 1
        /// </summary>
        /// <param name="r">The red component</param>
        /// <param name="g">The green component</param>
        /// <param name="b">The blue component</param>
        public Colorf(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1f;
        }

        /// <summary>
        /// Returns the color with the rgb elements multiplied by the given factor
        /// </summary>
        public static Colorf operator *(Colorf lhs, double factor)
        {
            return new Colorf(lhs.r * (float)factor, lhs.g * (float)factor, 
                lhs.b * (float)factor, lhs.a);
        }

        /// <summary>
        /// Returns the color with the rgb elements multiplied by the given factor
        /// </summary>
        public static Colorf operator *(double factor, Colorf lhs)
        {
            return lhs * factor;
        }

        /// <summary>
        /// Evaluates is the two Colors are equal
        /// </summary>
        public static bool operator ==(Colorf lhs, Colorf rhs)
        {
            return lhs.r == rhs.r && 
                   lhs.g == rhs.g &&
                   lhs.b == rhs.b &&
                   lhs.a == rhs.a;
        }

        /// <summary>
        /// Evaluates is the two Colors are not equal
        /// </summary>
        public static bool operator !=(Colorf lhs, Colorf rhs)
        {
            return !(lhs == rhs);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"r[{r}f], g[{g}f], b[{b}f], a[{a}f]";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Colorf))
            {
                return false;
            }

            var other = (Colorf) obj;

            return this == other;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = r.GetHashCode();
                hash = (hash * 397) ^ g.GetHashCode();
                hash = (hash * 397) ^ b.GetHashCode();
                hash = (hash * 397) ^ a.GetHashCode();
                return hash;
            }
        }
    }
}