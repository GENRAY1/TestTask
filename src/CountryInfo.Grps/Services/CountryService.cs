using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Application.GetCountry;
using CountryInfo.Core.Domain;
using CountryInfo.Core.Exceptions;
using Grpc.Core;

namespace CountryInfo.Grps.Services;

public class CountryService(IQueryHandler<GetCountryQuery, Country> getCountryHandler)
    : Grps.CountryService.CountryServiceBase
{
    public override async Task<GetCountryResponse> GetCountry(
        GetCountryRequest request,
        ServerCallContext context)
    {
        try
        {
            Country country = await getCountryHandler.Handle(
                new GetCountryQuery(request.Name), 
                context.CancellationToken);

            return new GetCountryResponse
            {
                Id = country.Id.ToString(),
                Code = country.Code,
                Name = country.Name,
                Region = country.Region,
                Subregion = country.Subregion,
                Capital = country.Capital,
                Population = country.Population,
                Currency = country.Currency,
                Statistic = new CountryStatistic
                {
                    AddedAt = country.Statistic.AddedAt.ToString("o"),
                    LastRequestedAt = country.Statistic.LastRequestedAt.ToString("o"),
                    TotalRequests = country.Statistic.TotalRequests
                }
            };
        }
        catch (CountryNotFoundException ex)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound, 
                ex.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(
                StatusCode.Internal, 
                $"An unexpected error occurred: {ex.Message}"));
        }
    }
}