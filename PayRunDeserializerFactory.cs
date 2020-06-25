public class PayRunDeserializerFactory
    {
        public static IPayRunDeserializer GetDeserializer(dynamic payrun)
        {
            if (payrun.type == "adjustment")
            {
                return new AdjustmentPayRunDeserializer();
            }
            else {
                return new PayRunDeserializer();
            }
        }
    }
