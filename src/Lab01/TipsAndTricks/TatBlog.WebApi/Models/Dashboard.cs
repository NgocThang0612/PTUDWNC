using TatBlog.Core.Entities;

namespace TatBlog.WebApi.Models.Dashboard
{
    public class Dashboards
    {
        public int TotalPost { get; set; }

        public int TotalAuthor { get; set; }
        public int TotalCategorie { get; set; }
        public int TotalSubscriber { get; set; }
        public int TotalUnpublishedPost { get; set; }
        public int TotalUnapprovedComment { get; set; }
        public int TotalNewSubscriberToday { get; set; }
    }
}