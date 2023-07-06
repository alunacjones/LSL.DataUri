using System;
using System.Text.RegularExpressions;

namespace LSL.DataUri
{
    /// <summary>
    /// Helper class for creating and parsing Data URIs
    /// </summary>
    public sealed class DataUri
    {
        private static Regex PartResolver = new Regex(@"^data:((?<mime>[\w/\-\.]+);)?(?<encoding>\w+),(?<data>.*)");

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
        public static DataUri Parse(string dataUri)
        {
            var matchedData = PartResolver.Match(dataUri);

            if (!matchedData.Success)
            {
                throw new ArgumentException($"The dataUri must be in base64 format to be parsed ({dataUri})");
            }

            var regexMatch = PartResolver.Match(dataUri);

            return new DataUri(Convert.FromBase64String(regexMatch.Groups["data"].Value), regexMatch.Groups["mime"].Value);
        }

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
    }
}