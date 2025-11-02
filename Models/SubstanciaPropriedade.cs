public class SubstanciaPropriedade
{
    public int SubstanciaId { get; set; }
    public Substancia Substancia { get; set; }
    public int PropriedadeId { get; set; }
    public Propriedade Propriedade { get; set; }
    public bool? ValorBool { get; set; }
    public decimal? ValorDecimal { get; set; }

}