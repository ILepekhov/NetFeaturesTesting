Console.WriteLine($"{nameof(RecordBattery)}: example");

RecordBattery recordBattery = new("CR2032", 0.235, 100);
Console.WriteLine(recordBattery);

while (recordBattery.RemainingCapacityPercentage > 0)
{
    recordBattery.RemainingCapacityPercentage--;
}

Console.WriteLine(recordBattery);
Console.WriteLine();


Console.WriteLine($"{nameof(ReadonlyRecordBattery)}: example");

ReadonlyRecordBattery readonlyRecordBattery = new("CR2032", 0.235, 100);
Console.WriteLine(readonlyRecordBattery);

while (readonlyRecordBattery.RemainingCapacityPercentage > 0)
{
    ReadonlyRecordBattery updatedBattery = readonlyRecordBattery with { RemainingCapacityPercentage = readonlyRecordBattery.RemainingCapacityPercentage - 1 };    
    readonlyRecordBattery = updatedBattery;
}

Console.WriteLine(readonlyRecordBattery);
Console.WriteLine();


Console.WriteLine($"{nameof(PlainStructBattery)}: example");

PlainStructBattery plainStructBattery = new("CR2032", 0.235, 100);
Console.WriteLine(plainStructBattery);

while (plainStructBattery.RemainingCapacityPercentage > 0)
{
    plainStructBattery.RemainingCapacityPercentage--;
}

Console.WriteLine(plainStructBattery);
Console.WriteLine();


Console.WriteLine($"{nameof(ReadonlyPlainStructBattery)}: example");

ReadonlyPlainStructBattery readonlyPlainStructBattery = new("CR2032", 0.235, 100);
Console.WriteLine(readonlyPlainStructBattery);

while (readonlyPlainStructBattery.RemainingCapacityPercentage > 0)
{
    // we can't do it: ReadonlyPlainStructBattery updatedBattery = readonlyPlainStructBattery with { RemainingCapacityPercentage = readonlyPlainStructBattery.RemainingCapacityPercentage - 1 };
    readonlyPlainStructBattery = new(
        readonlyPlainStructBattery.Model,
        readonlyPlainStructBattery.TotalCapacityAmpHours,
        readonlyPlainStructBattery.RemainingCapacityPercentage - 1);
}

Console.WriteLine(readonlyPlainStructBattery);
Console.WriteLine();



public record struct RecordBattery(string Model, double TotalCapacityAmpHours, int RemainingCapacityPercentage);

public readonly record struct ReadonlyRecordBattery(string Model, double TotalCapacityAmpHours, int RemainingCapacityPercentage);

public struct PlainStructBattery
{
    public string Model;
    public double TotalCapacityAmpHours;
    public int RemainingCapacityPercentage;

    public PlainStructBattery(string model, double totalCapacityAmpHours, int RemainingCapacityPercentage)
    {
        Model = model;
        TotalCapacityAmpHours = totalCapacityAmpHours;
        this.RemainingCapacityPercentage = RemainingCapacityPercentage;
    }

    public override string ToString()
    {
        return $"{nameof(PlainStructBattery)} {{ {nameof(Model)} = {Model}, {nameof(TotalCapacityAmpHours)} = {TotalCapacityAmpHours}, {nameof(RemainingCapacityPercentage)} = {RemainingCapacityPercentage} }}";
    }
}

public readonly struct ReadonlyPlainStructBattery
{
    public readonly string Model;
    public readonly double TotalCapacityAmpHours;
    public readonly int RemainingCapacityPercentage;

    public ReadonlyPlainStructBattery(string model, double totalCapacityAmpHours, int RemainingCapacityPercentage)
    {
        Model = model;
        TotalCapacityAmpHours = totalCapacityAmpHours;
        this.RemainingCapacityPercentage = RemainingCapacityPercentage;
    }

    public override string ToString()
    {
        return $"{nameof(ReadonlyPlainStructBattery)} {{ {nameof(Model)} = {Model}, {nameof(TotalCapacityAmpHours)} = {TotalCapacityAmpHours}, {nameof(RemainingCapacityPercentage)} = {RemainingCapacityPercentage} }}";
    }
}