using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int LibraryBranchId { get; set; }
}
