using System;

namespace WinterIsComing.Server
{
    static class RandExtensions
    {
        private static Random rand = new Random();

        public static int Random(this int minValue, int maxValue)
        {
            var res = rand.Next(minValue, maxValue);
            return res;
        }
    }
}