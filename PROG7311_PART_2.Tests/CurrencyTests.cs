using Xunit;
using PROG7311_PART_2.Service;

public class CurrencyTests
{
    [Fact]
    public void ConvertUsdToZar_ShouldReturnCorrectValue()
    {
        // Arrange
        decimal usd = 100;
        decimal rate = 18.5m;

        var service = new CurrencyServiceMock(rate);

        // Act
        var result = service.Convert(usd);

        // Assert
        Assert.Equal(1850, result);
    }
}

// Mock service
public class CurrencyServiceMock
{
    private readonly decimal _rate;

    public CurrencyServiceMock(decimal rate)
    {
        _rate = rate;
    }

    public decimal Convert(decimal usd)
    {
        return usd * _rate;
    }
}



