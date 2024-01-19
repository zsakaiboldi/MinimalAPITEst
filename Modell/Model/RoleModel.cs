namespace Modell.Model;

public class RoleModel {
    public int Id { get; set; }
    public string Role { get; set; }
    public DateTime? Date { get; set; }
    public bool Available { get; set; }
    public int Modifier_Id { get; set; }
}
