```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3476)
13th Gen Intel Core i7-1355U, 1 CPU, 12 logical and 10 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method                  | Mean      | Error     | StdDev    |
|------------------------ |----------:|----------:|----------:|
| BlockingCollectionQueue | 14.930 ms | 0.2135 ms | 0.1783 ms |
| DataFlowQueue           | 12.435 ms | 0.2452 ms | 0.5278 ms |
| ActionBlockQueue        |  5.013 ms | 0.1058 ms | 0.3051 ms |
| ChannelQueue            |  5.254 ms | 0.1323 ms | 0.3795 ms |
