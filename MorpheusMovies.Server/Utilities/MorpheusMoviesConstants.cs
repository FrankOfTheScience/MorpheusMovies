namespace MorpheusMovies.Server.Utilities;

public static class MorpheusMoviesConstants
{

    public const string CRUD_GET = "GET";
    public const string CRUD_POST = "POST";
    public const string CRUD_PUT = "PUT";
    public const string CRUD_DELETE = "DELETE";

    public static class ResponseConstants
    {
        public const string STATUS_OK = "OK";
        public const string STATUS_KO = "KO";

        /// <summary>
        /// Case when password is updated
        /// </summary>
        public const string PASSWORD_UPDATED = "Password for the user '{0}' has been successfully updated";
        /// <summary>
        /// Case when entity has been created
        /// </summary>
        public const string ENTITY_CREATED = "Entity {0} has been successfully created";
        /// <summary>
        /// Case when entity has been created
        /// </summary>
        public const string ENTITY_UPDATED = "Entity {0} has been successfully updated";
        /// <summary>
        /// Case when entity has been created
        /// </summary>
        public const string ENTITY_DELETED = "Entity {0} has been successfully deleted";
        /// <summary>
        /// Case when user has not been found for the email provided
        /// </summary>
        public const string USER_NOT_FOUND = "No registered users were found with the email '{0}'";
        /// <summary>
        /// Case when wrong credential has been submitted
        /// </summary>
        public const string INVALID_CREDENTIALS = "CredentialS provided were not valid";
        /// <summary>
        /// Case when wrong credential has been submitted
        /// </summary>
        public const string SIGNIN_SUCCESS = "You have successfully signed-in!";
        /// <summary>
        /// Case when trying to signup but user already exists for the provided email
        /// </summary>
        public const string USER_ALREADY_EXISTS = "Another user already exists with the email '{0}'";
        /// <summary>
        /// Case when trying to signup but user already exists for the provided email
        /// </summary>
        public const string SIGNUP_SUCCESS = "You have successfully signed-up with the email '{0}' !";
        /// <summary>
        /// Cases when parameters are missing
        /// </summary>
        public const string PARAMETER_NOT_DEFINED = "Missing parameter(s): {0}";
        /// <summary>
        /// Cases when parameters are wrong
        /// </summary>
        public const string PARAMETER_NOT_VALID = "The parameter(s) provided: {0} is/are not valid";
        /// <summary>
        /// Cases when entity is not found
        /// </summary>
        public const string ENTITY_NOT_FOUND_FOR_THE_OPERATION = "The entity {0} for the {1} operation was not found";
    }
}
