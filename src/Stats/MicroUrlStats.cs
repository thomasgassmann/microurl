namespace MicroUrl.Stats
{
    public class MicroUrlStats
    {
        public string Key { get; set; }

        public string TargetUrl { get; set; }

        public HitStats AllTime { get; set; }

        public HitStats[] Recents { get; set; }
    }
}