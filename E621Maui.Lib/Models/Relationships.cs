namespace E621Maui.Lib.Models
{
    public class Relationships
    {
        public long? ParentId { get; }
        public bool HasChildren { get; }
        public bool HasActiveChildren { get; }
        public string[] Children { get; }
    }
}
