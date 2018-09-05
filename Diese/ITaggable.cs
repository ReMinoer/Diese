using System.Collections.Generic;

namespace Diese
{
    public interface ITaggable
    {
        ICollection<object> Tags { get; }
    }
}