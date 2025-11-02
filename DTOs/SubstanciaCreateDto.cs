namespace SubstanciasAPI.DTOs;
public class SubstanciaCreateDto
{
    public string Nome { get; set; }
    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public string Notas { get; set; }
    public int CategoriaId { get; set; }
    public List<SubstanciaPropriedadeDto> Propriedades { get; set; }
}

public class SubstanciaPropriedadeDto
{
    public int PropriedadeId { get; set; }
    public bool? ValorBool { get; set; }
    public decimal? ValorDecimal { get; set; }
}
