  public class SubstanciaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Notas { get; set; }
        public CategoriaReadDto Categoria { get; set; }
        public List<SubstanciaPropriedadeReadDto> Propriedades { get; set; }
    }

    public class CategoriaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

    }

    public class SubstanciaPropriedadeReadDto
    {
        public int PropriedadeId { get; set; }
        public string NomePropriedade { get; set; }
        public bool? ValorBool { get; set; }
        public decimal? ValorDecimal { get; set; }
    }