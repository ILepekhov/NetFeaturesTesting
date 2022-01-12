using KirillLinqQueryTesting;

var parameters = Enumerable.Range(1, 5)
    .Select(x => new ParameterModel(Enumerable.Repeat(x, 5)
        .Select(y => new DataModel(y))
        .ToList()))
    .ToList();

var rpmsByAggregate = parameters
    .Select(p => p.Data.Select(data => data.Rpm).ToList())
    .Aggregate((x, dataRmps) =>
    {
        x.AddRange(dataRmps);
        
        return x;
    })
    .ToList();

Console.WriteLine("By Aggregate: " + string.Join(", ", rpmsByAggregate));

var rpmsBySelectMany = parameters
    .SelectMany(p => p.Data.Select(d => d.Rpm))
    .ToList();

Console.WriteLine("By SelectMany: " + string.Join(", ", rpmsBySelectMany));
