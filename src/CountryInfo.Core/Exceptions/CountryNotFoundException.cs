namespace CountryInfo.Core.Exceptions;

public class CountryNotFoundException(string name)
    : Exception($"Country '{name}' not found.");