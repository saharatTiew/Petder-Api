using petder.Models.ErrorModels;

namespace petder.Messages
{
    public class ErrorMessage
    {
        #region Authentication
        public static ErrorModel UserNameExist() => new ErrorModel { ErrorMessage = "Username has already existed." };
        public static ErrorModel AddressNotFound() => new ErrorModel { ErrorMessage = "Address is not valid." };

        #endregion

        #region Pet
        public static ErrorModel PetNotFound() => new ErrorModel { ErrorMessage = "User is not found or not have current pet." };
        public static ErrorModel UserNotFound() => new ErrorModel { ErrorMessage = "User is not found." };
        public static ErrorModel BreedNotFound() => new ErrorModel { ErrorMessage = "Breed is not found." };
        public static ErrorModel ImageNotFound() => new ErrorModel { ErrorMessage = "Image is not found." };

        #endregion

        #region general
        public static ErrorModel Exception(string message) => new ErrorModel { ErrorMessage = message };
        public static ErrorModel DateTimeWrongFormat() => new ErrorModel { ErrorMessage = "Datetime is in wrong format." };
        #endregion

        #region request
        public static ErrorModel RequestNotFound() => new ErrorModel { ErrorMessage = "Matched pet is not found." };
        public static ErrorModel RequestedPetNotFound() => new ErrorModel { ErrorMessage = "Requested pet is not found." };
        public static ErrorModel PendingRequestNotFound() => new ErrorModel { ErrorMessage = "Pending request is not found." };

        #endregion

        #region chat
        public static ErrorModel SessionNotFound() => new ErrorModel { ErrorMessage = "Session is not found." };
        public static ErrorModel PetNotBelongToSession() => new ErrorModel { ErrorMessage = "This pet does not belong to this session." };
        public static ErrorModel MessageNotFound() => new ErrorModel { ErrorMessage = "Message is not found." };

        #endregion

        #region DbError
        public static ErrorModel SaveChangesException() => new ErrorModel
        {
            ErrorMessage =
            "Exception occured during save changes to database. This might occur because parameter(s) are invalid."
        };

        public static ErrorModel ConcurrencyException() => new ErrorModel
        {
            ErrorMessage =
            "Exception occured during save changes to database. This might occur because the record does not exist" +
            "or the record is being changed while you are trying to save changes."
        };

        public static ErrorModel DeletePet() => new ErrorModel { ErrorMessage = "Pet is deleted successfully!" };

        #endregion

    }
}