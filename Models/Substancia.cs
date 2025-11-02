public class Substancia
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public string Notas { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public ICollection<SubstanciaPropriedade> Propriedades { get; set; }
}