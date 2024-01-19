namespace Modell.Model;

public class PasswordModel {
    public int Id { get; set; }
    public int User_Id { get; set; }
    public string Hashed_Password { get; set; }
    public string Salt { get; set; }
    public DateTime? Created { get; set; }
    public string Raw_Password { get; set; }
}
