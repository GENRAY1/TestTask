namespace CountryInfo.Core.Abstractions.Common;

public interface IQueryHandler <in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
}