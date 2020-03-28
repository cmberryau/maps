using Maps.Appearance;

namespace Maps.Unity.UI
{
    /// <summary>
    /// An interface for poolable label control
    /// </summary>
    public interface IPoolableLabel : ILabelAppearance
    {
        /// <summary>
        /// The text content of the label
        /// </summary>
        string Text
        {
            get;
            set;
        }

        /// <summary>
        /// The appearance of the label
        /// </summary>
        ILabelAppearance Appearance
        {
            get;
            set;
        }
    }
}