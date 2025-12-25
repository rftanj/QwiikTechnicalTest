namespace QwiikTechnicalTest.Utilities
{
    public class NewTokenGenerator
    {
        private const string Prefix = "Q";
        private const int PaddingLength = 3;

        public static string GenerateToken(string? lastToken)
        {
            if (string.IsNullOrWhiteSpace(lastToken))
            {
                return $"{Prefix}{1.ToString().PadLeft(PaddingLength, '0')}";
            }

            var numericPart = lastToken.Substring(Prefix.Length);

            if (!int.TryParse(numericPart, out var lastNumber))
            {
                throw new InvalidOperationException("Invalid token format");
            }

            var nextNumber = lastNumber + 1;

            return $"{Prefix}{nextNumber.ToString().PadLeft(PaddingLength, '0')}";
        }
    }
}
