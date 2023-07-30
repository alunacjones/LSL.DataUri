using System.Collections.Generic;

namespace LSL.DataUri
{
    internal struct InnerTryParseResult
    {
        public bool Success;
        public string Error;

        public InnerTryParseResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public override bool Equals(object obj)
        {
            return obj is InnerTryParseResult other &&
                   Success == other.Success &&
                   Error == other.Error;
        }

        public override int GetHashCode()
        {
            int hashCode = -1489853407;
            hashCode = hashCode * -1521134295 + Success.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Error);
            return hashCode;
        }
    }
}