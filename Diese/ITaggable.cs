using System.Collections.Generic;

namespace Glyph
{
    public interface ITaggable
    {
        ICollection<object> Tags { get; }
    }
}