namespace InvoiceProject.DTO.Customer;

/// <summary>
/// Parameters for filtering, sorting, and paginating customer requests.
/// </summary>
public class CustomerQueryParams
{
    /// <summary>The page number to retrieve (defaults to 1).</summary>
    public int Page { get; set; } = 1;

    /// <summary>The number of records per page (1-100).</summary>
    public int PageSize { get; set; } = 10;

    /// <summary>The field name to sort by (e.g., "name", "email", "createdat").</summary>
    public string? Sort { get; set; }

    /// <summary>The direction of sorting: "asc" or "desc".</summary>
    public string? SortDirection { get; set; } = "asc";

    /// <summary>Search term to filter customers by Name, Email, or Phone.</summary>
    public string? Search { get; set; }

    /// <summary>
    /// Validates the query parameters and sets default values.
    /// </summary>
    public void Validate()
    {
        Page = Page < 1 ? 1 : Page;
        PageSize = PageSize < 1 ? 1 : (PageSize > 100 ? 100 : PageSize);
        SortDirection = SortDirection?.ToLower() == "desc" ? "desc" : "asc";
        if (!string.IsNullOrWhiteSpace(Sort)) Sort = Sort.ToLower();
    }
}
