using System;
using System.ComponentModel.DataAnnotations;
public class Author
{
    [Key]
    public int AuthorId { get; set; }
    public string Name { get; set; }
}
 