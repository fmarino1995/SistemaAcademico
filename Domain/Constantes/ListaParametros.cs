namespace Domain.Constantes
{
    public class ListaParametros
    {
        public static List<KeyValuePair<string, string>> ListaTipoStatus = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(Parametros.StatusAtivo, DescricaoParametros.DescricaoStatusAtivo),
            new KeyValuePair<string, string>(Parametros.StatusInativo, DescricaoParametros.DescricaoStatusInativo)
        };

        public static List<KeyValuePair<string, string>> ListaTipoTurno = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(Parametros.TipoTurnoDiurno, DescricaoParametros.DescricaoTipoTurnoDiurno),
            new KeyValuePair<string, string>(Parametros.TipoTurnoNoturno, DescricaoParametros.DescricaoTipoTurnoNoturno)
        };

        public static List<KeyValuePair<int, string>> ListaPeriodos = new List<KeyValuePair<int, string>>
        {
            new KeyValuePair<int, string>(Parametros.PrimeiroPeriodo, DescricaoParametros.DescricaoPrimeiroPeriodo),
            new KeyValuePair<int, string>(Parametros.SegundoPeriodo, DescricaoParametros.DescricaoSegundoPeriodo),
            new KeyValuePair<int, string>(Parametros.TerceiroPeriodo, DescricaoParametros.DescricaoTerceiroPeriodo),
            new KeyValuePair<int, string>(Parametros.QuartoPeriodo, DescricaoParametros.DescricaoQuartoPeriodo),
            new KeyValuePair<int, string>(Parametros.QuintoPeriodo, DescricaoParametros.DescricaoQuintoPeriodo)
        };
    }
}
