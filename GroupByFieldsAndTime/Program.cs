// 将基础项一致，时间差小于1小时，则归为一组


var records = new List<Record>
{
    new Record { Id = 1, A = "1", B = "2", C = "3", D = new DateTime(2024, 1, 1, 10, 0, 0) },
    new Record { Id = 2, A = "1", B = "2", C = "3", D = new DateTime(2024, 1, 1, 10, 30, 0) },
    new Record { Id = 3, A = "1", B = "2", C = "3", D = new DateTime(2024, 1, 1, 12, 0, 0) }, // 不在这个组里
    new Record { Id = 4, A = "4", B = "5", C = "6", D = new DateTime(2024, 1, 1, 10, 0, 0) },
    new Record { Id = 5, A = "4", B = "5", C = "6", D = new DateTime(2024, 1, 1, 10, 45, 0) }
};


var groupedRecords = GroupRecordsByTimeSimilarity(records);

foreach (var group in groupedRecords)
{
    Console.WriteLine($"Group Key: {group.Key}");
    foreach (var record in group)
    {
        Console.WriteLine($"  Record ID: {record.Id}, Time: {record.D}");
    }
}

Console.ReadLine();

static IEnumerable<IGrouping<int, Record>> GroupRecordsByTimeSimilarity(IEnumerable<Record> records)
{
    var orderedRecords = records.OrderBy(r => r.D).ToList();
    var groups = new List<List<Record>>();

    foreach (var record in orderedRecords)
    {
        bool addedToExistingGroup = false;

        for (int i = 0; i < groups.Count; i++)
        {
            var lastRecordInGroup = groups[i].LastOrDefault();

            if (lastRecordInGroup != null && (record.D - lastRecordInGroup.D).TotalHours <= 1)
            {
                groups[i].Add(record);
                addedToExistingGroup = true;
                break;
            }
        }

        if (!addedToExistingGroup)
        {
            groups.Add(new List<Record> { record });
        }
    }

    return groups.Select((g, index) => g.AsEnumerable().GroupBy(x => index).First());
}


public class Record
{
    public int Id { get; set; }
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public DateTime D { get; set; }
}
