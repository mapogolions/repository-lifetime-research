namespace SingletonLifetimePitfall.Processing;

public interface IRequestProcessing
{
    void Process(IEnumerable<MockRequest> requests);
}
