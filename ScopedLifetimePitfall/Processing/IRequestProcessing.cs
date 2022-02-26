namespace ScopedLifetimePitfall.Processing;

public interface IRequestProcessing
{
    void Process(IEnumerable<MockRequest> requests);
}
