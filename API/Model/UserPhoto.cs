using System;

namespace API.Model
{
    public class UserPhoto
    {
        public string Id { get; set; }
        public string Url { get; set; }

        internal object FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}