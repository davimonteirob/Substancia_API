public class Propriedade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<SubstanciaPropriedade> Substancias { get; set; }
}