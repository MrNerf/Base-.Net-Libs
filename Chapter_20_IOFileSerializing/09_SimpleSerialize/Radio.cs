using System;

namespace _09_SimpleSerialize
{
    [Serializable]
    public class Radio
    {
        [NonSerialized]
        public const string RadioId = "XF-552RR6";

        public bool HasTweeters;

        public bool HasSubWoofers;

        public double[] StationPresets;
    }
}
