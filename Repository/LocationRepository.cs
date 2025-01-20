using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CountryStateCityApp.Model;

public class LocationRepository
{
    private readonly string _connectionString;

    public LocationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgresConnection");
    }

    // Countries
    public IEnumerable<Country> GetCountries()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<Country>(@"SELECT * FROM location.""Countries""").ToList();
        }
    }

    public void AddCountry(Country country)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"INSERT INTO location.""Countries"" (""CountryName"") VALUES (@CountryName)", country);
        }
    }

    public void UpdateCountry(Country country)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"UPDATE location.""Countries"" SET ""CountryName"" = @CountryName WHERE ""CountryId"" = @CountryId", country);
        }
    }

    public void DeleteCountry(int countryId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"DELETE FROM location.""Countries"" WHERE ""CountryId"" = @CountryId", new { CountryId = countryId });
        }
    }

    // States
    public IEnumerable<State> GetStatesByCountryId(int countryId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<State>(@"SELECT * FROM location.""States"" WHERE ""CountryId"" = @CountryId", new { CountryId = countryId }).ToList();
        }
    }

    public void AddState(State state)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"INSERT INTO location.""States"" (""StateName"", ""CountryId"") VALUES (@StateName, @CountryId)", state);
        }
    }

    public void UpdateState(State state)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"UPDATE location.""States"" SET ""StateName"" = @StateName WHERE ""StateId"" = @StateId", state);
        }
    }

    public void DeleteState(int stateId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"DELETE FROM location.""States"" WHERE ""StateId"" = @StateId", new { StateId = stateId });
        }
    }

    // Cities
    public IEnumerable<City> GetCitiesByStateId(int stateId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<City>(@"SELECT * FROM location.""Cities"" WHERE ""StateId"" = @StateId", new { StateId = stateId }).ToList();
        }
    }

    public void AddCity(City city)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"INSERT INTO location.""Cities"" (""CityName"", ""StateId"") VALUES (@CityName, @StateId)", city);
        }
    }

    public void UpdateCity(City city)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"UPDATE location.""Cities"" SET ""CityName"" = @CityName WHERE ""CityId"" = @CityId", city);
        }
    }

    public void DeleteCity(int cityId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(@"DELETE FROM location.""Cities"" WHERE ""CityId"" = @CityId", new { CityId = cityId });
        }
    }
}
