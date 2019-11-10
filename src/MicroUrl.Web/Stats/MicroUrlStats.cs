namespace MicroUrl.Web.Stats
{
    using System.Collections.Generic;

    public class MicroUrlStats
    {
        public string Key { get; set; }

        public string TargetUrl { get; set; }

        public HitStats AllTime { get; set; }

        public IList<HitStats> Recents { get; set; }
    }
}