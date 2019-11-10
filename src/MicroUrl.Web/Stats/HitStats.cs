namespace MicroUrl.Web.Stats
{
    using System;

    public class HitStats
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
        
        public long Visitors { get; set; }
        
        public long UniqueVisitors { get; set; }
    }
}