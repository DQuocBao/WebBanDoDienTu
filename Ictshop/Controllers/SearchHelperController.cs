using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Controllers
{
    public class SearchHelperController : Controller
    {
        // GET: ProductHelper 
        //Lấy giá trị RAM đầu tiên ở bên trái và tất cả giá trị ở dung lượng
        public static string GetMainMemoryValue(string memoryString)
        {
            if (string.IsNullOrEmpty(memoryString)) return "";

            // Loại bỏ các từ không cần thiết như "tối đa", "max", "upto"
            memoryString = memoryString.Replace("tốiđa", "").Replace("max", "").Replace("upto", "");

            // Chuẩn hóa chuỗi: Xóa khoảng trắng, chuyển về chữ thường
            memoryString = memoryString.Trim().Replace(" ", "").ToLower();

            // Regex tìm giá trị đầu tiên có đơn vị KB, MB, GB, TB, PB
            Match match = Regex.Match(memoryString, @"(\d+)\s*(KB|MB|GB|TB|PB)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int value = int.Parse(match.Groups[1].Value); // Lấy số lượng
                string unit = match.Groups[2].Value.ToUpper(); // Lấy đơn vị (KB, MB, GB, TB, PB)

                // Chuyển đổi đơn vị nếu cần
                if (unit == "KB")
                {
                    if (value >= 1024) return $"{value / 1024}MB"; // 1024KB → 1MB
                    return $"{value}KB"; // VD: 512KB → 512KB
                }
                else if (unit == "MB")
                {
                    if (value == 1024) return "1GB";  // 1024MB = 1GB
                    if (value % 1024 == 0) return $"{value / 1024}GB"; // VD: 2048MB → 2GB
                    return $"{value}MB"; // VD: 512MB → 512MB
                }
                else if (unit == "GB")
                {
                    if (value == 1024) return "1TB";  // 1024GB = 1TB
                    if (value % 1024 == 0) return $"{value / 1024}TB"; // VD: 2048GB → 2TB
                    return $"{value}GB"; // VD: 64GB → 64GB
                }
                else if (unit == "TB")
                {
                    if (value == 1024) return "1PB";  // 1024TB = 1PB
                    if (value % 1024 == 0) return $"{value / 1024}PB"; // VD: 2048TB → 2PB
                    return $"{value}TB"; // VD: 10TB → 10TB
                }
                else // "PB"
                {
                    return $"{value}PB"; // VD: 5PB → 5PB
                }
            }
            return ""; // Không tìm thấy giá trị hợp lệ
        }

        

        public static Dictionary<string, string> GetFirstSSDAndHDD(string storageString) //Lấy SSD, HDD Đầu tiên
        {
            if (string.IsNullOrEmpty(storageString)) return new Dictionary<string, string>();

            // Chuẩn hóa chuỗi: Xóa khoảng trắng dư thừa
            storageString = storageString.Trim();

            // Loại bỏ các từ không cần thiết như "mở rộng tối đa lên đến", "max", "upto"
            storageString = storageString.Replace("mở rộng tối đa lên đến", "")
                               .Replace("max", "")
                               .Replace("upto", "");

            // Regex tìm tất cả giá trị có đơn vị KB, MB, GB, TB, PB kèm theo SSD hoặc HDD
            MatchCollection matches = Regex.Matches(storageString, @"(\d+)\s*(KB|MB|GB|TB|PB)\s*(SSD|HDD)", RegexOptions.IgnoreCase);

            string firstSSD = null;
            string firstHDD = null;

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    int value = int.Parse(match.Groups[1].Value);
                    string unit = match.Groups[2].Value.ToUpper();
                    string type = match.Groups[3].Value.ToUpper(); // SSD hoặc HDD

                    // Chuyển đổi đơn vị nếu cần
                    string formattedValue = $"{value}{unit}";
                    if (unit == "KB" && value >= 1024) formattedValue = $"{value / 1024}MB";
                    if (unit == "MB" && value >= 1024) formattedValue = $"{value / 1024}GB";
                    if (unit == "GB" && value >= 1024) formattedValue = $"{value / 1024}TB";
                    if (unit == "TB" && value >= 1024) formattedValue = $"{value / 1024}PB";

                    // Lưu giá trị đầu tiên của SSD và HDD
                    if (type == "SSD" && firstSSD == null) firstSSD = formattedValue;
                    if (type == "HDD" && firstHDD == null) firstHDD = formattedValue;

                    // Nếu đã có cả SSD và HDD thì dừng vòng lặp
                    if (firstSSD != null && firstHDD != null) break;
                }
            }

            // Trả về dictionary chứa SSD và HDD đầu tiên (nếu có)
            return new Dictionary<string, string>
            {
                { "SSD", firstSSD },
                { "HDD", firstHDD }
            };
        }

        /* // Hàm lấy tất cả giá trị dung lượng lưu trữ từ chuỗi (có thể có nhiều dung lượng khác nhau)
        public static List<string> GetAllStorageValues(string storageString)
        {
            if (string.IsNullOrEmpty(storageString)) return new List<string>();

            // Chuẩn hóa chuỗi: Xóa khoảng trắng
            storageString = storageString.Trim().Replace(" ", "");

            //// Loại bỏ các từ không cần thiết như "tối đa", "max", "upto"
            storageString = storageString.Replace("mở rộng tối đa lên đến", "")
                           .Replace("max", "")
                           .Replace("upto", "");

            // Regex tìm tất cả giá trị có đơn vị KB, MB, GB, TB, PB
            MatchCollection matches = Regex.Matches(storageString, @"(\d+)\s*(KB|MB|GB|TB|PB)", RegexOptions.IgnoreCase);
            List<string> storageValues = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    int value = int.Parse(match.Groups[1].Value);
                    string unit = match.Groups[2].Value.ToUpper();

                    // Chuyển đổi đơn vị nếu cần
                    if (unit == "KB")
                    {
                        if (value >= 1024) storageValues.Add($"{value / 1024}MB"); // 1024KB → 1MB
                        else storageValues.Add($"{value}KB"); // VD: 512KB → 512KB
                    }
                    else if (unit == "MB")
                    {
                        if (value == 1024) storageValues.Add("1GB");
                        else if (value % 1024 == 0) storageValues.Add($"{value / 1024}GB");
                        else storageValues.Add($"{value}MB");
                    }
                    else if (unit == "GB")
                    {
                        if (value == 1024) storageValues.Add("1TB");
                        else if (value % 1024 == 0) storageValues.Add($"{value / 1024}TB");
                        else storageValues.Add($"{value}GB");
                    }
                    else if (unit == "TB")
                    {
                        if (value == 1024) storageValues.Add("1PB");
                        else if (value % 1024 == 0) storageValues.Add($"{value / 1024}PB");
                        else storageValues.Add($"{value}TB");
                    }
                    else // PB
                    {
                        storageValues.Add($"{value}PB");
                    }
                }
            }
            return storageValues;
        }*/
    }
}