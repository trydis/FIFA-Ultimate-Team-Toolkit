using System.Globalization;
using System.Text.Json.Serialization;

namespace UltimateTeam.Toolkit.Models.Auth
{
    public class Pid
    {
        public string? ExternalRefType { get; set; }
        public string? ExternalRefValue { get; set; }
        public long PidId { get; set; }
        public string? Email { get; set; }
        public string? EmailStatus { get; set; }
        public string? Strength { get; set; }
        public string? Dob { get; set; }
        public string? Country { get; set; }
        public string? Language { get; set; }
        public string? Locale { get; set; }
        public string? Status { get; set; }
        public string? ReasonCode { get; set; }
        public string? TosVersion { get; set; }
        public string? ParentalEmail { get; set; }
        public bool ThirdPartyOptin { get; set; }
        public bool GlobalOptin { get; set; }

        private DateTimeOffset _dateCreated;
        private DateTimeOffset _dateModified;
        private DateTimeOffset _lastAuthDate;

        [JsonPropertyName("dateCreated")]
        public string DateCreated
        {
            get => _dateCreated.ToString("yyyy-MM-ddTHH:mmZ");
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _dateCreated = ParseDateTime(value);
                }
            }
        }

        [JsonPropertyName("dateModified")]
        public string DateModified
        {
            get => _dateModified.ToString("yyyy-MM-ddTHH:mmZ");
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _dateModified = ParseDateTime(value);
                }
            }
        }

        [JsonPropertyName("lastAuthDate")]
        public string LastAuthDate
        {
            get => _lastAuthDate.ToString("yyyy-MM-ddTHH:mmZ");
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _lastAuthDate = ParseDateTime(value);
                }
            }
        }

        [JsonIgnore]
        public DateTimeOffset ParsedDateCreated => _dateCreated;

        [JsonIgnore]
        public DateTimeOffset ParsedDateModified => _dateModified;

        [JsonIgnore]
        public DateTimeOffset ParsedLastAuthDate => _lastAuthDate;

        private DateTimeOffset ParseDateTime(string value)
        {
            // Try to parse with single-digit hours/minutes first
            if (DateTimeOffset.TryParseExact(value, "yyyy-MM-ddTH:mZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTimeOffset parsedDate))
            {
                return parsedDate;
            }
            // Fallback to the standard ISO 8601 format
            return DateTimeOffset.ParseExact(value, "yyyy-MM-ddTHH:mmZ", CultureInfo.InvariantCulture);
        }

        public string? RegistrationSource { get; set; }
        public string? AuthenticationSource { get; set; }
        public string? ShowEmail { get; set; }
        public string? DiscoverableEmail { get; set; }
        public bool AnonymousPid { get; set; }
        public bool UnderagePid { get; set; }
        public string? DefaultBillingAddressUri { get; set; }
        public string? DefaultShippingAddressUri { get; set; }
        public int PasswordSignature { get; set; }
    }
}