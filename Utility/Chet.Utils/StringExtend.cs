using System.Text;
using System.Text.RegularExpressions;

namespace Chet.Utils
{
    /// <summary>
    /// String 扩展方法类，提供常用的判断、正则表达式验证、类型转换和字符串操作方法。
    /// </summary>          
    public static class StringExtend
    {
        #region 判断

        /// <summary>
        /// 判断字符串是否为 null 或空字符串。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// 判断字符串是否为 null 或仅包含空白字符。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// 判断字符串是否为数字（可解析为 double）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsNumeric(this string value) =>
            !string.IsNullOrWhiteSpace(value) && double.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为整数。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsInt(this string value) =>
            !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为浮点数（float）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsFloat(this string value) =>
            !string.IsNullOrWhiteSpace(value) && float.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为十进制数（decimal）。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsDecimal(this string value) =>
            !string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为 Guid。
        /// </summary>
        /// <param name="value">待判断的字符串。</param>
        public static bool IsGuid(this string value) =>
            Guid.TryParse(value, out _);

        /// <summary>
        /// 忽略大小写判断字符串是否相等。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="compare">要比较的字符串。</param>
        public static bool EqualsIgnoreCase(this string value, string compare) =>
            string.Equals(value, compare, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 判断字符是否为中文字符
        /// </summary>
        /// <param name="value">要验证的字符</param>
        /// <returns>bool</returns>
        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5？，“”‘’。、；：]+$");
        }

        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasChinese(this string value)
        {
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="nullStrings">自定义空字符串，中间“|”分隔</param>
        /// <param name="isTrim">是否移除收尾空白字符串，默认：false</param>
        /// <returns>bool</returns>
        public static bool IsNull(this string value, string nullStrings = "null|{}|[]", bool isTrim = false)
        {
            var result = true;
            if (value != null)
            {
                if (isTrim)
                    result = value.Trim() == "";
                else
                    result = value == "";
                //是否为自定义空字符串
                if (!result && !string.IsNullOrWhiteSpace(nullStrings))
                {
                    if (isTrim)
                        result = nullStrings.Split('|').Contains(value.Trim().ToLower());
                    else
                        result = nullStrings.Split('|').Contains(value.ToLower());
                }
            }
            return result;
        }

        #endregion

        #region 正则表达式验证
        /// <summary>
        /// 验证字符是否是字母类型[如果是true则是字母类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsLetterByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }
        /// <summary>
        /// 验证字符是否是数字类型[如果是true则是数字类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsNumByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }
        /// <summary>
        /// 提取字符串中的数字部分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ExtractNumByRegex(this string source)
        {
            return Regex.Replace(source, @"[^0-9]+", "");
        }
        /// <summary>
        /// 验证字符是不是浮点类型[如果是true则是浮点类型]
        /// </summary>
        /// <param name="value">字符串名</param>
        /// <returns>bool</returns>
        public static bool IsFloatByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]{0,1}\d*$");
        }
        /// <summary>
        /// 验证字符是否是Email格式[如果是true则是Email格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsEmailByRegex(this string value)
        {
            return Regex.IsMatch(value, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        /// <summary>
        /// 验证字符是否是Tel格式[如果是true则是Tel格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsTelByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^[0-9]{3,4}\-[0-9]{3,8}\-[0-9]{1,4}$|(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)");
        }
        /// <summary>
        /// 验证是否是手机[如果是true则是Tel格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsMobileByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^1[34578]\d{9}$");
        }
        /// <summary>
        /// 验证是否是网址[如果是true则是网址格式]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsUrlByRegex(this string value)
        {
            return Regex.IsMatch(value, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        /// <summary>
        /// 验证字符串是否是日期类型[如果是true则是日期类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsDateByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{2}|\d{4})(-|\/)(\d{1,2})\2(\d{1,2})$");
        }
        /// <summary>
        /// 验证字符串是否是时间类型[如果是true则是时间类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsTimeByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^\d{1,2}\:\d{1,2}\:\d{1,2}$");
        }
        /// <summary>
        /// 验证字符串是否是日期时间类型[如果是true则是日期时间类型]
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>bool</returns>
        public static bool IsDateTimeByRegex(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{2}|\d{4})(-|\/)(\d{1,2})\2(\d{1,2})\s\d{1,2}\:\d{1,2}\:\d{1,2}$");
        }
        #endregion

        #region 类型转换

        /// <summary>
        /// 字符串转 int，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static int ToInt(this string value, int defaultValue = 0) =>
            int.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 float，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloat(this string value, float defaultValue = 0) =>
            float.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 double，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDouble(this string value, double defaultValue = 0) =>
            double.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 double，保留指定小数位，四舍五入，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDoubleRound(this string value, int digits = 2, double defaultValue = 0)
        {
            if (double.TryParse(value, out var result))
                return Math.Round(result, digits, MidpointRounding.AwayFromZero);
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 double，保留指定小数位，向零取整，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static double ToDoubleTruncate(this string value, int digits = 2, double defaultValue = 0)
        {
            if (double.TryParse(value, out var result))
            {
                double factor = Math.Pow(10, digits);
                return Math.Truncate(result * factor) / factor;
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 float，保留指定小数位，四舍五入，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloatRound(this string value, int digits = 2, float defaultValue = 0)
        {
            if (float.TryParse(value, out var result))
                return (float)Math.Round(result, digits, MidpointRounding.AwayFromZero);
            return defaultValue;
        }

        /// <summary>
        /// 字符串转 float，保留指定小数位，向零取整，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static float ToFloatTruncate(this string value, int digits = 2, float defaultValue = 0)
        {
            if (float.TryParse(value, out var result))
            {
                float factor = (float)Math.Pow(10, digits);
                return (float)(Math.Truncate(result * factor) / factor);
            }
            return defaultValue;
        }

        /// <summary>
        /// 保留数值型字符串的小数位（默认两位），非数值型原样返回。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="digits">保留的小数位数。</param>
        public static string KeepDecimal(this string value, int digits = 2)
        {
            if (double.TryParse(value, out var result))
                return Math.Round(result, digits, MidpointRounding.AwayFromZero).ToString($"F{digits}");
            return value;
        }

        /// <summary>
        /// 字符串转 decimal，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0) =>
            decimal.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 bool，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static bool ToBool(this string value, bool defaultValue = false) =>
            bool.TryParse(value, out var result) ? result : defaultValue;

        /// <summary>
        /// 字符串转 Guid，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static Guid ToGuid(this string value, Guid? defaultValue = null) =>
            Guid.TryParse(value, out var result) ? result : (defaultValue ?? Guid.Empty);

        /// <summary>
        /// 字符串转 DateTime，失败返回默认值。
        /// </summary>
        /// <param name="value">待转换的字符串。</param>
        /// <param name="defaultValue">转换失败时的默认值。</param>
        public static DateTime ToDateTime(this string value, DateTime? defaultValue = null) =>
            DateTime.TryParse(value, out var result) ? result : (defaultValue ?? DateTime.MinValue);

        #endregion

        #region 字符串操作

        /// <summary>
        /// 安全去除字符串首尾空白字符，null 返回空字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string TrimSafe(this string value) => value?.Trim() ?? string.Empty;

        /// <summary>
        /// 移除字符串中的所有空白字符。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string RemoveWhiteSpace(this string value) =>
            string.IsNullOrEmpty(value) ? string.Empty : string.Concat(value.Where(c => !char.IsWhiteSpace(c)));

        /// <summary>
        /// 安全截取字符串，超出范围返回空字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="startIndex">起始索引。</param>
        /// <param name="length">截取长度。</param>
        public static string SubstringSafe(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value) || startIndex < 0 || length <= 0 || startIndex >= value.Length)
                return string.Empty;
            return value.Length - startIndex < length
                ? value[startIndex..]
                : value.Substring(startIndex, length);
        }

        /// <summary>
        /// 获取字符串左侧指定长度的子串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="length">子串长度。</param>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
            return value.Length <= length ? value : value.Substring(0, length);
        }

        /// <summary>
        /// 获取字符串右侧指定长度的子串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="length">子串长度。</param>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
            return value.Length <= length ? value : value.Substring(value.Length - length, length);
        }

        /// <summary>
        /// 反转字符串。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string Reverse(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            var arr = value.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        /// <summary>
        /// 移除字符串中的特殊字符，仅保留字母、数字和空白。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string RemoveSpecialChars(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return new string(value.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// 转为 camelCase（首字母小写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length == 1) return value.ToLower();
            return char.ToLower(value[0]) + value[1..];
        }

        /// <summary>
        /// 转为 PascalCase（首字母大写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length == 1) return value.ToUpper();
            return char.ToUpper(value[0]) + value[1..];
        }

        /// <summary>
        /// 重复字符串指定次数。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        /// <param name="count">重复次数。</param>
        public static string Repeat(this string value, int count)
        {
            if (string.IsNullOrEmpty(value) || count <= 0) return string.Empty;
            return string.Concat(Enumerable.Repeat(value, count));
        }

        /// <summary>
        /// 忽略大小写替换字符串中的指定内容。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="oldValue">要替换的内容。</param>
        /// <param name="newValue">新内容。</param>
        public static string ReplaceIgnoreCase(this string value, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(oldValue)) return value;
            return Regex.Replace(
                value, Regex.Escape(oldValue), newValue ?? string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取字符串的 MD5 值（32位小写）。
        /// </summary>
        /// <param name="value">待处理的字符串。</param>
        public static string ToMd5(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            var hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// 判断字符串是否包含指定子串，忽略大小写。
        /// </summary>
        /// <param name="value">原字符串。</param>
        /// <param name="sub">要查找的子串。</param>
        public static bool ContainsIgnoreCase(this string value, string sub)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(sub)) return false;
            return value.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 将字符串按指定分隔符分割为字符串数组，自动去除空项。
        /// </summary>
        /// <param name="value">待分割的字符串。</param>
        /// <param name="separators">分隔符数组。</param>
        public static string[] SplitSafe(this string value, params char[] separators)
        {
            if (string.IsNullOrEmpty(value)) return Array.Empty<string>();
            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 中文转拼音
        /// </summary>
        /// <param name="value">中文字符串</param>
        /// <returns>string</returns>
        public static string ToPinyin(this string value)
        {
            var iA = new int[]
            {
                 -20319 ,-20317 ,-20304 ,-20295 ,-20292 ,-20283 ,-20265 ,-20257 ,-20242 ,-20230
                 ,-20051 ,-20036 ,-20032 ,-20026 ,-20002 ,-19990 ,-19986 ,-19982 ,-19976 ,-19805
                 ,-19784 ,-19775 ,-19774 ,-19763 ,-19756 ,-19751 ,-19746 ,-19741 ,-19739 ,-19728
                 ,-19725 ,-19715 ,-19540 ,-19531 ,-19525 ,-19515 ,-19500 ,-19484 ,-19479 ,-19467
                 ,-19289 ,-19288 ,-19281 ,-19275 ,-19270 ,-19263 ,-19261 ,-19249 ,-19243 ,-19242
                 ,-19238 ,-19235 ,-19227 ,-19224 ,-19218 ,-19212 ,-19038 ,-19023 ,-19018 ,-19006
                 ,-19003 ,-18996 ,-18977 ,-18961 ,-18952 ,-18783 ,-18774 ,-18773 ,-18763 ,-18756
                 ,-18741 ,-18735 ,-18731 ,-18722 ,-18710 ,-18697 ,-18696 ,-18526 ,-18518 ,-18501
                 ,-18490 ,-18478 ,-18463 ,-18448 ,-18447 ,-18446 ,-18239 ,-18237 ,-18231 ,-18220
                 ,-18211 ,-18201 ,-18184 ,-18183 ,-18181 ,-18012 ,-17997 ,-17988 ,-17970 ,-17964
                 ,-17961 ,-17950 ,-17947 ,-17931 ,-17928 ,-17922 ,-17759 ,-17752 ,-17733 ,-17730
                 ,-17721 ,-17703 ,-17701 ,-17697 ,-17692 ,-17683 ,-17676 ,-17496 ,-17487 ,-17482
                 ,-17468 ,-17454 ,-17433 ,-17427 ,-17417 ,-17202 ,-17185 ,-16983 ,-16970 ,-16942
                 ,-16915 ,-16733 ,-16708 ,-16706 ,-16689 ,-16664 ,-16657 ,-16647 ,-16474 ,-16470
                 ,-16465 ,-16459 ,-16452 ,-16448 ,-16433 ,-16429 ,-16427 ,-16423 ,-16419 ,-16412
                 ,-16407 ,-16403 ,-16401 ,-16393 ,-16220 ,-16216 ,-16212 ,-16205 ,-16202 ,-16187
                 ,-16180 ,-16171 ,-16169 ,-16158 ,-16155 ,-15959 ,-15958 ,-15944 ,-15933 ,-15920
                 ,-15915 ,-15903 ,-15889 ,-15878 ,-15707 ,-15701 ,-15681 ,-15667 ,-15661 ,-15659
                 ,-15652 ,-15640 ,-15631 ,-15625 ,-15454 ,-15448 ,-15436 ,-15435 ,-15419 ,-15416
                 ,-15408 ,-15394 ,-15385 ,-15377 ,-15375 ,-15369 ,-15363 ,-15362 ,-15183 ,-15180
                 ,-15165 ,-15158 ,-15153 ,-15150 ,-15149 ,-15144 ,-15143 ,-15141 ,-15140 ,-15139
                 ,-15128 ,-15121 ,-15119 ,-15117 ,-15110 ,-15109 ,-14941 ,-14937 ,-14933 ,-14930
                 ,-14929 ,-14928 ,-14926 ,-14922 ,-14921 ,-14914 ,-14908 ,-14902 ,-14894 ,-14889
                 ,-14882 ,-14873 ,-14871 ,-14857 ,-14678 ,-14674 ,-14670 ,-14668 ,-14663 ,-14654
                 ,-14645 ,-14630 ,-14594 ,-14429 ,-14407 ,-14399 ,-14384 ,-14379 ,-14368 ,-14355
                 ,-14353 ,-14345 ,-14170 ,-14159 ,-14151 ,-14149 ,-14145 ,-14140 ,-14137 ,-14135
                 ,-14125 ,-14123 ,-14122 ,-14112 ,-14109 ,-14099 ,-14097 ,-14094 ,-14092 ,-14090
                 ,-14087 ,-14083 ,-13917 ,-13914 ,-13910 ,-13907 ,-13906 ,-13905 ,-13896 ,-13894
                 ,-13878 ,-13870 ,-13859 ,-13847 ,-13831 ,-13658 ,-13611 ,-13601 ,-13406 ,-13404
                 ,-13400 ,-13398 ,-13395 ,-13391 ,-13387 ,-13383 ,-13367 ,-13359 ,-13356 ,-13343
                 ,-13340 ,-13329 ,-13326 ,-13318 ,-13147 ,-13138 ,-13120 ,-13107 ,-13096 ,-13095
                 ,-13091 ,-13076 ,-13068 ,-13063 ,-13060 ,-12888 ,-12875 ,-12871 ,-12860 ,-12858
                 ,-12852 ,-12849 ,-12838 ,-12831 ,-12829 ,-12812 ,-12802 ,-12607 ,-12597 ,-12594
                 ,-12585 ,-12556 ,-12359 ,-12346 ,-12320 ,-12300 ,-12120 ,-12099 ,-12089 ,-12074
                 ,-12067 ,-12058 ,-12039 ,-11867 ,-11861 ,-11847 ,-11831 ,-11798 ,-11781 ,-11604
                 ,-11589 ,-11536 ,-11358 ,-11340 ,-11339 ,-11324 ,-11303 ,-11097 ,-11077 ,-11067
                 ,-11055 ,-11052 ,-11045 ,-11041 ,-11038 ,-11024 ,-11020 ,-11019 ,-11018 ,-11014
                 ,-10838 ,-10832 ,-10815 ,-10800 ,-10790 ,-10780 ,-10764 ,-10587 ,-10544 ,-10533
                 ,-10519 ,-10331 ,-10329 ,-10328 ,-10322 ,-10315 ,-10309 ,-10307 ,-10296 ,-10281
                 ,-10274 ,-10270 ,-10262 ,-10260 ,-10256 ,-10254
             };
            var sA = new string[]
            {
                "a","ai","an","ang","ao"
                ,"ba","bai","ban","bang","bao","bei","ben","beng","bi","bian","biao","bie","bin"
                ,"bing","bo","bu"
                ,"ca","cai","can","cang","cao","ce","ceng","cha","chai","chan","chang","chao","che"
                ,"chen","cheng","chi","chong","chou","chu","chuai","chuan","chuang","chui","chun"
                ,"chuo","ci","cong","cou","cu","cuan","cui","cun","cuo"
                ,"da","dai","dan","dang","dao","de","deng","di","dian","diao","die","ding","diu"
                ,"dong","dou","du","duan","dui","dun","duo"
                ,"e","en","er"
                ,"fa","fan","fang","fei","fen","feng","fo","fou","fu"
                ,"ga","gai","gan","gang","gao","ge","gei","gen","geng","gong","gou","gu","gua","guai"
                ,"guan","guang","gui","gun","guo"
                ,"ha","hai","han","hang","hao","he","hei","hen","heng","hong","hou","hu","hua","huai"
                ,"huan","huang","hui","hun","huo"
                ,"ji","jia","jian","jiang","jiao","jie","jin","jing","jiong","jiu","ju","juan","jue"
                ,"jun"
                ,"ka","kai","kan","kang","kao","ke","ken","keng","kong","kou","ku","kua","kuai","kuan"
                ,"kuang","kui","kun","kuo"
                ,"la","lai","lan","lang","lao","le","lei","leng","li","lia","lian","liang","liao","lie"
                ,"lin","ling","liu","long","lou","lu","lv","luan","lue","lun","luo"
                ,"ma","mai","man","mang","mao","me","mei","men","meng","mi","mian","miao","mie","min"
                ,"ming","miu","mo","mou","mu"
                ,"na","nai","nan","nang","nao","ne","nei","nen","neng","ni","nian","niang","niao","nie"
                ,"nin","ning","niu","nong","nu","nv","nuan","nue","nuo"
                ,"o","ou"
                ,"pa","pai","pan","pang","pao","pei","pen","peng","pi","pian","piao","pie","pin","ping"
                ,"po","pu"
                ,"qi","qia","qian","qiang","qiao","qie","qin","qing","qiong","qiu","qu","quan","que"
                ,"qun"
                ,"ran","rang","rao","re","ren","reng","ri","rong","rou","ru","ruan","rui","run","ruo"
                ,"sa","sai","san","sang","sao","se","sen","seng","sha","shai","shan","shang","shao","she"
                ,"shen","sheng","shi","shou","shu","shua","shuai","shuan","shuang","shui","shun","shuo","si"
                ,"song","sou","su","suan","sui","sun","suo"
                ,"ta","tai","tan","tang","tao","te","teng","ti","tian","tiao","tie","ting","tong","tou","tu"
                ,"tuan","tui","tun","tuo"
                ,"wa","wai","wan","wang","wei","wen","weng","wo","wu"
                ,"xi","xia","xian","xiang","xiao","xie","xin","xing","xiong","xiu","xu","xuan","xue","xun"
                ,"ya","yan","yang","yao","ye","yi","yin","ying","yo","yong","you","yu","yuan","yue","yun"
                ,"za","zai","zan","zang","zao","ze","zei","zen","zeng","zha","zhai","zhan","zhang","zhao"
                ,"zhe","zhen","zheng","zhi","zhong","zhou","zhu","zhua","zhuai","zhuan","zhuang","zhui"
                ,"zhun","zhuo","zi","zong","zou","zu","zuan","zui","zun","zuo"
            };
            var B = new byte[2];
            var s = "";
            var c = value.ToCharArray();
            for (var j = 0; j < c.Length; j++)
            {
                B = Encoding.Default.GetBytes(c[j].ToString());
                if (B[0] <= 160 && B[0] >= 0)
                {
                    s += c[j];
                }
                else
                {
                    for (var i = (iA.Length - 1); i >= 0; i--)
                    {
                        if (iA[i] <= B[0] * 256 + B[1] - 65536)
                        {
                            s += sA[i];
                            break;
                        }
                    }
                }
            }
            return s;
        }
        #endregion
    }
}
