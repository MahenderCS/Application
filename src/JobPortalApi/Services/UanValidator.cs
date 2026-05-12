using System.Text.RegularExpressions;

namespace JobPortalApi.Services;

public sealed class UanValidator
{
    private static readonly Regex DigitsOnly = new("^\\d{12}$", RegexOptions.Compiled);

    public bool IsValid(string? uan)
    {
        if (string.IsNullOrWhiteSpace(uan))
        {
            return false;
        }

        return DigitsOnly.IsMatch(uan);
    }
}
