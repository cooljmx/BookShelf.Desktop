using System.Runtime.Serialization;

namespace BookShelf.Infrastructure.Settings;

[DataContract]
internal abstract class WindowMemento
{
    [DataMember(Name = "left")]
    public double Left { get; set; }

    [DataMember(Name = "top")]
    public double Top { get; set; }

    [DataMember(Name = "width")]
    public double Width { get; set; }

    [DataMember(Name = "height")]
    public double Height { get; set; }

    [DataMember(Name = "isMaximized")]
    public bool IsMaximized { get; set; }
}