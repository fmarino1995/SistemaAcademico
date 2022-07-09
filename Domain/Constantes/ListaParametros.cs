namespace Domain.Constantes
{
    public class ListaParametros
    {
        //public IDictionary<string, string> ListaTipoStatus = new Dictionary<string, string>()
        //{
        //    { Parametros.StatusAtivo, DescricaoParametros.DescriçãoStatusAtivo},
        //    { Parametros.StatusInativo, DescricaoParametros.DescriçãoStatusInativo}
        //};

        public static List<KeyValuePair<string, string>> ListaTipoStatus = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(Parametros.StatusAtivo, DescricaoParametros.DescriçãoStatusAtivo),
            new KeyValuePair<string, string>(Parametros.StatusInativo, DescricaoParametros.DescriçãoStatusInativo)
        };
        
        
    }
}
