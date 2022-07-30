﻿namespace Domain.Constantes
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
    }
}
