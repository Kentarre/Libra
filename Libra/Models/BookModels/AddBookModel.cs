namespace Libra.Models.BookModels;

public class AddBookModel
{
    public string OwnerId { get; set; }
    public string BookName { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int PublishingDate { get; set; }
    public List<IFormFile> Pictures { get; set; }
    
    public AddBookModel(IFormCollection form)
    {
        OwnerId = form["OwnerId"].ToString();
        BookName = form["BookName"].ToString();
        Description = form["Description"].ToString();
        Author = form["Author"].ToString();
        PublishingDate = Convert.ToInt32(form["PublishingDate"]);
        Pictures = form.Files.ToList();
    }
}