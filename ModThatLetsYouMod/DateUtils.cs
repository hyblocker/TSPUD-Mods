using System;

namespace ModThatLetsYouMod
{
    public class DateUtils
    {
        /// <summary>
        /// Returns the current time as a unix timestamp
        /// </summary>
        /// <returns></returns>
        public static long CurrentUnixTimestamp()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }
    }
}
