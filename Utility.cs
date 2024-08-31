using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    internal class Utility
    {
        public static void onlyNumber(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static class UniqueIdGenerator
        {
            private static readonly object lockObject = new object();
            private static long lastTimestamp = Stopwatch.GetTimestamp();

            public static string GetUniqueId(string prefix)
            {
                long timestamp;
                lock (lockObject)
                {
                    timestamp = Stopwatch.GetTimestamp();
                    if (timestamp == lastTimestamp)
                    {
                        // If the timestamp hasn't changed, add a small increment to ensure uniqueness
                        timestamp++;
                    }
                    lastTimestamp = timestamp;
                }

                long nano = 10000L * timestamp;
                nano /= TimeSpan.TicksPerMillisecond;
                nano *= 100L;
                return prefix + DateTime.UtcNow.ToString("yyyyMMddHHmm") + new Random().Next(10, 99); // Format for fixed length
            }
        }

        public static string getImageStorePath(string imageName)
        {
            return Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Images\\" + imageName + ".png";
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                // Simple email validation regex pattern
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static class SimpleUniquePassIdGenerator
        {
            private static readonly Random random = new Random();

            // Generate a unique pass ID with "PASS" prefix
            public static string GetUniquePassId()
            {
                // Generate a random number
                int randomNumber = random.Next(10, 99); // 2-digit random number

                // Combine "PASS" prefix with the random number and a short timestamp
                string passId = $"PASS{DateTime.Now:yyyyMMddHHmm}{randomNumber}";

                return passId;
            }
        }

    }
}
