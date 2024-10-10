using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    // Eşitlik kontrolü için override metodları
    public override bool Equals(object obj)
    {
        if (obj is Address address)
        {
            return Street == address.Street &&
                   City == address.City &&
                   State == address.State &&
                   PostalCode == address.PostalCode &&
                   Country == address.Country;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City, State, PostalCode, Country);
    }
}
