using System;

namespace Maps.Data
{
    /// <summary>
    /// Attribute for members to be stored on a db
    /// </summary>
    public class DbStoreMember : Attribute
    {
        /// <summary>
        /// The ordinal of the member
        /// </summary>
        public int Ordinal
        {
            get;
        }

        /// <summary>
        /// The name of the column
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Is the column a primary key?
        /// </summary>
        public bool PrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// Is the column unique?
        /// </summary>
        public bool Unique
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of DbColumn
        /// </summary>
        public DbStoreMember(int ordinal)
        {
            Ordinal = ordinal;
        }
    }
}