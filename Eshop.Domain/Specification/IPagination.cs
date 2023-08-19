namespace Eshop.Core.Specification
{
    public interface IPagination
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}