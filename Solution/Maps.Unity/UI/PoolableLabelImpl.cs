using System;
using Maps.Appearance;
using Maps.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implmentation of a poolable label
    /// </summary>
    internal sealed class PoolableLabelImpl : IPoolableLabel
    {
        /// <inheritdoc />
        public string Text
        {
            get => _text.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Text));
                }

                _text.text = value;
            }
        }

        /// <inheritdoc />
        public ILabelAppearance Appearance
        {
            get => _appearance;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appearance));
                }

                _appearance = value;
                ZIndex = _appearance.ZIndex;
                Padding = _appearance.Padding;
                IgnoreOthers = _appearance.IgnoreOthers;
                RotateWithMap = _appearance.RotateWithMap;
                FontBold = _appearance.FontBold;
                FontColor = _appearance.FontColor;
                FontSize = _appearance.FontSize;
                FontOutline = _appearance.FontOutline;
                FontOutlineColor = _appearance.FontOutlineColor;
            }
        }

        /// <inheritdoc />
        public int ZIndex
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public float Padding
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IgnoreOthers
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool RotateWithMap
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool FontBold
        {
            get => _appearance.FontBold;
            private set
            {
                if (value)
                {
                    if (_text.fontStyle == FontStyle.Italic)
                    {
                        _text.fontStyle = FontStyle.BoldAndItalic;
                    }
                    else
                    {
                        _text.fontStyle = FontStyle.Bold;
                    }
                }
                else
                {
                    if (_text.fontStyle == FontStyle.BoldAndItalic)
                    {
                        _text.fontStyle = FontStyle.Italic;
                    }
                    else
                    {
                        _text.fontStyle = FontStyle.Normal;
                    }
                }
            }
        }

        /// <inheritdoc />
        public Colorf FontColor
        {
            get => _appearance.FontColor;
            private set => _text.color = value.Color();
        }

        /// <inheritdoc />
        public float FontSize
        {
            get => _appearance.FontSize;
            private set => _text.fontSize = (int)value;
        }

        /// <inheritdoc />
        public bool FontOutline
        {
            get => _appearance.FontOutline;
            private set => _outline.enabled = value;
        }

        /// <inheritdoc />
        public Colorf FontOutlineColor
        {
            get => _appearance.FontOutlineColor;
            private set => _outline.effectColor = value.Color();
        }

        private readonly Text _text;
        private readonly Outline _outline;
        private ILabelAppearance _appearance;

        /// <summary>
        /// Initializes a new PoolableLabelImpl
        /// </summary>
        /// <param name="text">The text element associated with the impl</param>
        /// <param name="outline">The outline element associated with the impl</param>
        public PoolableLabelImpl(Text text, Outline outline)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (outline == null)
            {
                throw new ArgumentNullException(nameof(outline));
            }

            _text = text;
            _outline = outline;
        }
    }
}