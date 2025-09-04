using Discount.Commands;

namespace Discount.Utility
{
    public class UtilManager
    {
        public static Dictionary<string, string> ErrorValidationCheck(CreateDiscountCommand request)
        {
            var validationErrors = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(request.ProductName)) validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.Description)) validationErrors["Description"] = "Description must not be empty";
            if (request.Amount <= 0) validationErrors["Amount"] = "Amout can not be zero.";
            return validationErrors;
        }

        public static Dictionary<string, string> ErrorValidationCheck(UpdateDiscountCommand request)
        {
            var validationErrors = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(request.ProductName)) validationErrors["ProductName"] = "Product name must not be empty";
            if (string.IsNullOrWhiteSpace(request.Description)) validationErrors["Description"] = "Description must not be empty";
            if (request.Amount <= 0) validationErrors["Amount"] = "Amout can not be zero.";
            return validationErrors;
        }

    }
}
