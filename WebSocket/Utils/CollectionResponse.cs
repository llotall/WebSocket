using System.Collections;

namespace Utils
{
    public class CollectionResponse
    {
        public int Count { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public IList Items { get; set; }
    }
}
