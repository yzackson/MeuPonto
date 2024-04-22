using SQLite;

namespace MeuPonto.Models
{
    public class Ponto
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public Tipo Tipo { get; set; }
        public string? Justificativa { get; set; }
    }

    public enum Tipo
    {
        ENTRADA,
        SAIDA,
        ALMOCO,
        RETORNO_ALMOCO,
        SAIDA_EXEPCIONAL,
        RETORNO_EXCEPCIONAL
    }
}
