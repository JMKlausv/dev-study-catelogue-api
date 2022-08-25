namespace dev_study_catelogue_api.Extensions
{
    public  static class DictionaryExtensions
    {
        public static string ToDictString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}
