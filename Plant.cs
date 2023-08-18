public class Plant
{
    public string Species { get; set; }
    public int LightNeeds { get; set; } // scale 1 - 5 1 = shade  5 = full sun
    public decimal AskingPrice { get; set; }
    public string City { get; set; }
    public int ZIP { get; set; }
    public bool Sold { get; set; }
    public DateTime AvailableUntil { get; set; }
}