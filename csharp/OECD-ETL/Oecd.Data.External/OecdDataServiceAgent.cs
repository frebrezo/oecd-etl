using Oecd.Data.External.Dto;
using Newtonsoft.Json;

namespace Oecd.Data.External;

/// <summary>
/// https://data.oecd.org/api/sdmx-json-documentation/
/// </summary>
public class OecdDataServiceAgent : IOecdDataServiceAgent
{
    private static readonly HttpClient _httpClient;
    private readonly string _dataSetId;

    static OecdDataServiceAgent()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://stats.oecd.org/SDMX-JSON/data/")
        };
    }

    public OecdDataServiceAgent(string dataSetId)
    {
        _dataSetId = dataSetId;
    }

    public async Task<OecdDataResponse?> Get()
    {
        OecdDataResponse? data = null;

        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_dataSetId}");
        using HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string? responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                data = JsonConvert.DeserializeObject<OecdDataResponse>(responseString);
            }
            else
            {
                throw new Exception($"[{response.RequestMessage?.Method} {response.RequestMessage?.RequestUri}] : No response received. Response string expected. : [{responseString}]");
            }
        }
        else
        {
            throw new Exception($"[{(int)response.StatusCode}]/[{response.ReasonPhrase}] : [{response.RequestMessage?.Method} {response.RequestMessage?.RequestUri}].");
        }

        return data;
    }
}
