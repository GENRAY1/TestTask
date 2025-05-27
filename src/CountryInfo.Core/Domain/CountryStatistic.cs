namespace CountryInfo.Core.Domain;

public class CountryStatistic 
{
    public int TotalRequests { get; set; }
    public DateTime LastRequestedAt { get; set; }
    public DateTime AddedAt { get; init; }
}