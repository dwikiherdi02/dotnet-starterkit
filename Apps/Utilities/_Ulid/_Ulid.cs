namespace Apps.Utilities._Ulid
{
    public static class _Ulid
    {
        public static string ToFormattedString(this Ulid ulid)
        {
            var ulidStr = ulid.ToString();  // Contoh: "01F8MECHZX3TBDSZ7EXQ8PZ2AY"
            
            // Tambahkan tanda hubung
            return $"{ulidStr.Substring(0, 8)}-{ulidStr.Substring(8, 4)}-{ulidStr.Substring(12, 4)}-{ulidStr.Substring(16, 4)}-{ulidStr.Substring(20)}";
        }

        public static Ulid FromFormattedString(string formattedUlid)
        {
            // if (string.IsNullOrEmpty(formattedUlid))
            // {
            //     throw new ArgumentNullException(nameof(formattedUlid), "Formatted ULID cannot be null or empty.");
            // }

            // Hapus tanda hubung untuk kembali ke format asli ULID
            var ulidStr = formattedUlid!.Replace("-", "");
            return Ulid.Parse(ulidStr);
        }
    }
}