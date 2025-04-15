namespace apiPayment.methods
{
    public class GenerateCode
    {
        public static string GeneratePixCode()
        {
            var pixCode = new Guid();

            if (pixCode != Guid.Empty)
            {
                return pixCode.ToString();
            }
            else
            {
                return "Error! Pix code is required";
            }
        }
        public static string GenerateBoletoCode()
        {
            var boletoCode = Guid.NewGuid().ToString().Replace("-", ".");
            return boletoCode;
        }
    }
}
