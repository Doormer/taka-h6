namespace Chat.Api.Models.Responses
{
    /// <summary>
    /// Generic wrapper for API responses
    /// Provides a standardized response format for all API endpoints
    /// </summary>
    /// <typeparam name="T">The type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets whether the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the response message
        /// Can be a success message or error description
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the response data
        /// Will be null in case of error
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Creates a successful response with data and optional message
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <param name="message">Optional success message</param>
        /// <returns>A new successful ApiResponse instance</returns>
        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error response with a message
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns>A new error ApiResponse instance</returns>
        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}