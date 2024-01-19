namespace Modell.Model;

public class GroupModel 
{
    public int Id { get; set; }
    public string Group_Name { get; set; }
    public DateTime? Date { get; set; }
    public bool Available { get; set; }
    public int Modifier_Id { get; set; }
}
