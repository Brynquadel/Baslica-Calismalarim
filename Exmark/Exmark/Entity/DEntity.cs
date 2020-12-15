
namespace Exmark
{
    internal class DEntity
    {
        public string Name { get; set; }
        public string Root { get; set; }
        public string MainLocation { get; set; }
        public string TargetLocation { get; set; }
        public string Key { get; set; }
        public bool Continuity { get; set; }
        public string CategorizeName { get; set; } = null;
        public bool IsCategorized { get; set; }
    }
}
