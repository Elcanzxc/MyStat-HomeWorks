using InvoiceProject.Models;

namespace InvoiceProject.DTO.Invoice;

/// <summary>
/// Parameters for filtering, sorting, and paginating invoice requests.
/// </summary>
public class InvoiceQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Sort { get; set; }
    public string? SortDirection { get; set; } = "desc"; // Newer invoices first

    /// <summary>Search in Comments or Customer Name.</summary>
    public string? Search { get; set; }

    /// <summary>Filter by a specific Customer.</summary>
    public int? CustomerId { get; set; }

    /// <summary>Filter by Invoice Status.</summary>
    public InvoiceStatus? Status { get; set; }

    /// <summary>Filter by Minimum Total Sum.</summary>
    public decimal? MinTotal { get; set; }

    public void Validate()
    {
        Page = Page < 1 ? 1 : Page;
        PageSize = PageSize < 1 ? 1 : (PageSize > 100 ? 100 : PageSize);
        SortDirection = SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        if (!string.IsNullOrWhiteSpace(Sort)) Sort = Sort.ToLower();
    }
}
