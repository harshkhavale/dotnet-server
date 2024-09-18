using System;

namespace SportsClubApi.Utils
{
    public static class VerificationCodeGenerator
    {
        private static readonly Random _random = new Random();

        public static string GenerateCode(int length = 6)
        {
            var randomNumber = _random.Next(0, (int)Math.Pow(10, length) - 1);
            return randomNumber.ToString().PadLeft(length, '0');
        }
    }
}
