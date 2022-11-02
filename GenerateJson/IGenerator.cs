namespace GenerateJson;

public interface IGenerator
{
    Task Generate(Options options);
}