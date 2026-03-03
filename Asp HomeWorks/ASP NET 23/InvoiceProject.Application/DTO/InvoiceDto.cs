using InvoiceProject.Models;

namespace InvoiceProject.DTO;

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
public class InvoiceRequestDto
{

    public int CustomerId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public string? Comment { get; set; }

    public List<InvoiceRowRequestDto> RowsCreate { get; set; } = [];
}
public class InvoiceResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<InvoiceRowResponseDto> RowsResponse { get; set; } = [];
    public decimal TotalSum { get; set; }
    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

}
public class InvoiceUpdateDto
{
    public int CustomerId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public string? Comment { get; set; }

}
public class InvoiceUpdateStatusDto
{
    public InvoiceStatus Status { get; set; }
}
