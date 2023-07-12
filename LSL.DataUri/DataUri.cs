using System;
using System.Text.RegularExpressions;

namespace LSL.DataUri
{
    /// <summary>
    /// Helper class for creating and parsing Data URIs
    /// </summary>
    public sealed class DataUri
    {
        private static Regex PartResolver = new Regex(@"^data:((?<mime>[\w\/\-\.\+]+);)?(?<encoding>\w+),(?<data>.+)$");

        /// <summary>
        /// Create a DataUri instance with the given data and mime type
        /// </summary>
        /// <param name="data">The binary data to encode</param>
        /// <param name="mimeType">The mime-type of the data</param>
        public DataUri(byte[] data, string mimeType)
        {
            Data = data;
            MimeType = mimeType;
        }

        /// <summary>
        /// Parse a data Uri and return an instance of DataUri
        /// </summary>
        /// <param name="dataUri"></param>
        /// <returns></returns>
        /// <exception cref="System.FormatException">Gets thrown if the provide string was not in the correct format</exception>
        public static DataUri Parse(string dataUri)
        {
            var parseResult = InnerTryParse(dataUri, out var result);

            if (!parseResult.Success)
            {
                throw new FormatException(parseResult.Error);
            }

            return result;
        }

        /// <summary>
        /// Try to parse a satring into
        /// </summary>
        /// <param name="dataUri"></param>
        /// <param name="result"></param>
        /// <returns>true if it worked and also sets the out parameter to the result</returns>
        public static bool TryParse(string dataUri, out DataUri result) => InnerTryParse(dataUri, out result).Success;

        /// <summary>
        /// Provides the byte array of the data URI
        /// </summary>
        /// <value></value>
        public byte[] Data { get; }

        /// <summary>
        /// Provides the optional mime type of the data URI
        /// </summary>
        /// <value></value>
        public string MimeType { get; }

        /// <summary>
        /// Returns the string representation of the data URI
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"data:{MimeType};base64,{Convert.ToBase64String(Data)}";

        private static (bool Success, string Error) InnerTryParse(string dataUri, out DataUri result)
        {
            var regexMatch = PartResolver.Match(dataUri);

            if (!regexMatch.Success)
            {
                result = null;
                return (false, $"The dataUri must be in base64 format to be parsed ({dataUri})");
            }

            try
            {
                var bytes = Convert.FromBase64String(regexMatch.Groups["data"].Value);
                result = new DataUri(bytes, regexMatch.Groups["mime"].Value);

                return (true, string.Empty);                
            }
            catch (FormatException e)
            {
                result = null;
                return (false, e.Message);            
            }
        }
    }
}