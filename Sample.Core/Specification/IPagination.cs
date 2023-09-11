namespace Sample.Core.Specification
{
    // <summary>
    /// pagination settings
    /// </summary>
    public interface IPagination
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}