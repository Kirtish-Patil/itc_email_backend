namespace Email_data.Dtos
{
    public class GenericApiResponse<T>
    {
        /// <summary>
        /// Status Code
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Error description of processing
        /// </summary>
        public string Message { get; set; }


        public bool IsSuccess { get; set; }

        /// <summary>
        /// Data associated with the request processing if any
        /// </summary>
        public T Data { get; set; }
    }
}
