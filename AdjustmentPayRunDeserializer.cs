using Newtonsoft.Json;

public class AdjustmentPayRunDeserializer : IPayRunDeserializer
{
    public PayRun Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject<AdjustmentPayRun>(json);
        return response;
    }
}