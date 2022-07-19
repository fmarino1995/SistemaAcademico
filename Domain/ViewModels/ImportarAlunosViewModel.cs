using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ViewModels
{
    public class ImportarAlunosViewModel
    {
        public LogImportacao LogImportacao { get; set; }
        public int QuantidadeImportados { get; set; }
        public int QuantidadeNaoImportados { get; set; }
        public string StringLog { get; set; }
    }
}
