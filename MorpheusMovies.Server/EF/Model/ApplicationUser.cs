using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

[Table("ApplicationUser", Schema = "dbo")]
public class ApplicationUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Occupation Occupation { get; set; }
    public virtual IEnumerable<UserMoviePreference> MoviePreferences { get; set; } = new List<UserMoviePreference>();
}

public enum Occupation
{
    administrator,
    artist,
    doctor,
    educator,
    engineer,
    entertainment,
    executive,
    healthcare,
    homemaker,
    lawyer,
    librarian,
    marketing,
    none,
    other,
    programmer,
    retired,
    salesman,
    scientist,
    student,
    technician,
    writer
}

public enum Gender
{
    M,
    F
}
