using System.Collections.Generic;

namespace Entity
{
    public class Friend
    {
        public string Name { set; get; }
        public bool IsSaleMan { set; get; }
        public List<Friend> Friends { set; get; }
    }
}
