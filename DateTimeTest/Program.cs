/**
 * 时间日期测试
 */


// 计算相差时间
var now = DateTime.Now;
var yesterday = DateTime.Now.AddDays(-1);
var time = now.Subtract(yesterday).TotalMinutes / 60;
Console.WriteLine($"昨天是{yesterday.ToLongTimeString()}, 今天是{now.ToLongTimeString()}, 经过了{time.ToString("F2")}小时");
Console.ReadLine();