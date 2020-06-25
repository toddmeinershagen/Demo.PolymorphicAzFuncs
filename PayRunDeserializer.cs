using Newtonsoft.Json;

public class PayRunDeserializer : IPayRunDeserializer
{
    public PayRun Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject<PayRun>(json);
        return response;
    }
}
