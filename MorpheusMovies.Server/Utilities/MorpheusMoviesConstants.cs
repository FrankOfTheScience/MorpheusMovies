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
