namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class DidReceiveFontAttributePayload
{
    public string Family { get; set; }
    public int Size { get; set; }
    public string Color { get; set; }
    public string HAlignment { get; set; }
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
}

