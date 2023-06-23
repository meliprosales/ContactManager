using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace ContactManager.Web.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class BingMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public BingMapsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("BingMaps"); 
            _apiKey = configuration["BingMaps:ApiKey"]; // Retrieve the API key from your appsettings.json or configuration source
        }

        public async Task<BingMapsResponse> GetMapData(string location)
        {
            var response = await _httpClient.GetAsync($"REST/v1/Locations?q={location}&key={_apiKey}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var mapData = JsonConvert.DeserializeObject<BingMapsResponse>(json);
                return mapData;
            }

            // Handle error response here if needed

            return null;
        }

        public async Task<AddressValidationResult> ValidateAddress(string address)
        {
            var response = await _httpClient.GetAsync($"REST/v1/Locations?query={address}&key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var validationData = JsonConvert.DeserializeObject<BingMapsValidationResponse>(json);

                if (validationData?.ResourceSets?.Count > 0 && validationData.ResourceSets[0].Resources.Count > 0)
                {
                    var addressResult = validationData.ResourceSets[0].Resources[0] as BingMapsAddressResult;

                    if (addressResult.Confidence != "High")
                    {
                        // Address validation failed with low or medium confidence
                        return new AddressValidationResult
                        {
                            IsValid = false,
                            ValidationMessage = "Address validation failed with low or medium confidence."
                        };
                    }
                    else
                    {
                        // Address is valid with high confidence
                        return new AddressValidationResult
                        {
                            IsValid = true,
                            ValidatedAddress = addressResult.Address
                        };
                    }
                }
            }

            // Handle error response or address not found

            return new AddressValidationResult
            {
                IsValid = false,
                ValidationMessage = "Address not found or error occurred during validation."
            };
        }
    }

    public class BingMapsResponse
    {
        // Add properties to deserialize the response JSON for GetMapData method
    }

    public class BingMapsValidationResponse
    {
        public List<BingMapsResourceSet> ResourceSets { get; set; }
    }

    public class BingMapsResourceSet
    {
        public List<BingMapsResource> Resources { get; set; }
    }

    public class BingMapsResource
    {
        // Add properties to deserialize the response JSON for address validation
    }

    public class BingMapsAddressResult: BingMapsResource
    {
        public string Confidence { get; set; }
        public string MatchCode { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        // Add properties to deserialize the validated address JSON
    }

    public class AddressValidationResult
    {
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
        public Address ValidatedAddress { get; set; }
    }


}
