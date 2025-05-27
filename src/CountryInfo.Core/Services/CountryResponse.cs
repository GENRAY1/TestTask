namespace CountryInfo.Core.Services;

public class CountryResponse
{
    public required NameInfo Name { get; set; }
    public required string Cca2 { get; set; }
    public required string Region { get; set; }
    public required string Subregion { get; set; }
    public required List<string> Capital { get; set; }
    public required long Population { get; set; }
    public required Dictionary<string, CurrencyInfo> Currencies { get; set; }
    public class NameInfo
    {
        public required string Common { get; set; }
        public required string Official { get; set; }
        public required Dictionary<string, NativeNameInfo> NativeName { get; set; }
    }

    public class NativeNameInfo
    {
        public required string Official { get; set; }
        public required string Common { get; set; }
    }

    public class CurrencyInfo
    {
        public required string Name { get; set; }
        public required string Symbol { get; set; }
    }
}