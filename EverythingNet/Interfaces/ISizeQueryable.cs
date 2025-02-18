using EverythingNet.Query;

namespace EverythingNet.Interfaces;

public interface ISizeQueryable : IQueryable
{
    ISizeQueryable Equal(int value);

    ISizeQueryable Equal(int value, SizeUnit u);

    ISizeQueryable Equal(Sizes size);

    ISizeQueryable GreaterThan(int value);

    ISizeQueryable GreaterThan(int value, SizeUnit u);

    ISizeQueryable GreaterOrEqualThan(int value);

    ISizeQueryable GreaterOrEqualThan(int value, SizeUnit u);

    ISizeQueryable LessThan(int value);

    ISizeQueryable LessThan(int value, SizeUnit u);

    ISizeQueryable LessOrEqualThan(int value);

    ISizeQueryable LessOrEqualThan(int value, SizeUnit u);

    ISizeQueryable Between(int min, int max);

    ISizeQueryable Between(int min, int max, SizeUnit u);

    ISizeQueryable Between(int min, SizeUnit umin, int max, SizeUnit umax);
}