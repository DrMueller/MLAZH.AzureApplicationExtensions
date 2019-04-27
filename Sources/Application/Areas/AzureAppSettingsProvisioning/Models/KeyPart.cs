using System.Linq;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class KeyPart
    {
        public bool HasTrailingNumber => int.TryParse(Value.Last().ToString(), out _);
        public string Value { get; }

        public KeyPart(string value)
        {
            Value = value;
        }

        public string GetTrailingNumbers()
        {
            var numbers = string.Empty;

            for (var i = Value.Length - 1; i >= 0; i--)
            {
                var charElement = Value.ElementAt(i);
                if (int.TryParse(charElement.ToString(), out _))
                {
                    numbers = charElement + numbers;
                }
                else
                {
                    break;
                }
            }

            return numbers;
        }

        public string GetValueWithoutTrailingNumbers()
        {
            var trailingNumbers = GetTrailingNumbers();
            if (string.IsNullOrEmpty(trailingNumbers))
            {
                return Value;
            }

            return Value.Replace(trailingNumbers, string.Empty);
        }
    }
}