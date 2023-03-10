using System;


namespace TatBlog.Core.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public DateTime SubscribeDate { get; set; }

        public DateTime UnsubscribeDate { get; set; }

        public string ResonUnsubscribe { get; set; }

        public bool Voluntary { get; set; }

        public string Notes { get; set; }
    }
}
