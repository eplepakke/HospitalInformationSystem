namespace HospitalInformationSystem.Processing
{
    public static class DataSearch
    {
        public static bool ContainsIgnoreCase(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return false;

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (char.ToLower(text[i + j]) != char.ToLower(pattern[j]))
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return true;
            }

            return false;
        }
    }
}
