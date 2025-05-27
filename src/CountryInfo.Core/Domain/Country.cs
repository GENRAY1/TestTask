namespace CountryInfo.Core.Domain;

public class Country
{
    public required Guid Id { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string Region { get; init; }
    public string? Subregion { get; init; }
    public required string Capital { get; init; }
    public required long Population { get; init; }
    public required string Currency { get; init; }

    public required CountryStatistic Statistic { get; init; }
}