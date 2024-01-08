# 主要区别：
- 时区信息的处理 
   DataTime：表示本地日期和时间，不包含时区信息
   DateTimeOffset：包含了时区信息，可以准确地表示一个日期和时间
- 可靠性和精确性
    DataTime：由于不包含时区信息，因此在处理跨时区的日期和时间上可能会导致可靠性和精确性的问题
    DateTimeOffset:由于包含时区信息，可以更可靠的处理跨时区的日期和时间操作
- 序列化和持久化：
     DataTime：在序列化和持久化需要额外的处理来记录和恢复时区信息
     DateTimeOffset：可以保留时区信息，因此能够在不同系统或跨时区的环境中正确地还原日期和时间
- 与各种协议和格式的兼容：
    某些协议和数据格式要求包括时区信息。使用 DateTimeOffset 类型可以直接满足这些需求，而 DateTime 类型则需要进行额外的转换和处理。

######   总的来说，DateTime 类型适用于本地化的日期和时间操作，而 DateTimeOffset 类型适用于跨时区的日期和时间操作。

# DateTime 与 DataTimeOffset之间的转换

将一个具有 UTC 时区的 DateTime 对象转换为相应的 DateTimeOffset 对象。

```
DateTime dateTime = new DateTime(2023, 1, 21, 7, 0, 0);
dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
DateTimeOffset Time2 = dateTime;
```
具有本地时区的 DateTime 对象转换为相应的 DateTimeOffset 对象。
```
 DateTime LocalTime1 = new DateTime(2023, 7, 20, 7, 0, 0);
 LocalTime1 = DateTime.SpecifyKind(LocalTime1,DateTimeKind.Local);
 DateTimeOffset Time2 = LocalTime1;
```
将一个具有特定日期和时间的 DateTime 对象转换为相应的 DateTimeOffset 对象，并设置时区偏移量为 "Central Standard Time" 所对应的偏移量。
```
  DateTime time1 = new DateTime(2021, 1, 20, 7, 0, 0);
      try
     {
         DateTimeOffset time2 = new DateTimeOffset(time1,
         TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time").GetUtcOffset(time1));
        }
        catch (Exception e)
     {
          Console.WriteLine(e);
          throw;
       }
    }
```

# 从DateTimeOffset 到DateTime 的转换
是将一个具有特定日期和时间的 DateTime 对象转换为相应的 DateTimeOffset 对象，
```
DateTime baseTime = new DateTime(2025, 6, 19, 7, 0, 0);
DateTimeOffset sourceTime = new DateTimeOffset(baseTime, TimeSpan.Zero);
DateTime targetTime = sourceTime.DateTime; 
```
一个具有特定日期和时间的 DateTime 对象转换为相应的带有本地时区偏移量的 DateTimeOffset 
```
DateTime baseTime = new DateTime(2008, 6, 19, 7, 0, 0);
DateTimeOffset sourceTime = new DateTimeOffset(baseTime, TimeZoneInfo.Local.GetUtcOffset(baseTime));
DateTime targetTime = sourceTime.DateTime;
```
