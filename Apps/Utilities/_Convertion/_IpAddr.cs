
using System.Net;

namespace Apps.Utilities._Convertion
{
    public sealed class _IpAddr
    {
        public static int Atoi(string? ipAddress)
        {
            if (ipAddress == null)
            {
                return 0;
            }
            var addressBytes = IPAddress.Parse(ipAddress).GetAddressBytes();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(addressBytes);
            }
            return BitConverter.ToInt32(addressBytes, 0);
        }

        public static string Itoa(int ipAddressInt)
        {
            var addressBytes = BitConverter.GetBytes(ipAddressInt);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(addressBytes);
            }
            return new IPAddress(addressBytes).ToString();
        }
    }
}