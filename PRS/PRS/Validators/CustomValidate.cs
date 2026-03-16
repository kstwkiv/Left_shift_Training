using System.ComponentModel.DataAnnotations;

namespace PRS.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CompareWithAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public bool IsLessThan { get; set; }
        public bool IsGreaterThan { get; set; }

        public CompareWithAttribute(string comparisonProperty)
            => _comparisonProperty = comparisonProperty;

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            var prop = ctx.ObjectType.GetProperty(_comparisonProperty);
            if (prop == null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");

            var other = prop.GetValue(ctx.ObjectInstance);
            if (value == null || other == null) return ValidationResult.Success;

            if (value is IComparable a && other is IComparable b)
            {
                int cmp = a.CompareTo(b);
                if (IsLessThan && cmp >= 0) return new ValidationResult(ErrorMessage);
                if (IsGreaterThan && cmp <= 0) return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>Ensures a DateTime value is in the future.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
            => value is not DateTime d || d > DateTime.Now;

        public override string FormatErrorMessage(string name)
            => $"{name} must be a future date.";
    }

    /// <summary>Ensures a DateTime value is in the past.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
            => value is not DateTime d || d < DateTime.Now;

        public override string FormatErrorMessage(string name)
            => $"{name} must be a past date.";
    }

    /// <summary>Validates that a string is a valid SKU (uppercase letters, digits, hyphens).</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SkuFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not string sku || string.IsNullOrWhiteSpace(sku)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(sku, @"^[A-Z0-9\-]+$");
        }

        public override string FormatErrorMessage(string name)
            => $"{name} must contain only uppercase letters, digits, and hyphens.";
    }
}
