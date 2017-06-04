using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anotacoes.Entidades
{
    class Nota
    {
        private Nullable<int> id;
        private string titulo;
        private string conteudo;
        private List<Tag> tags;

        public Nota(string titulo, string conteudo, List<Tag> tags, Nullable<int> id = null)
        {
            this.id = id;
            this.titulo = titulo;
            this.conteudo = conteudo;
            this.tags = tags;
        }

        public int? Id { get => id;  }
        public string Titulo { get => titulo;  }
        public string Conteudo { get => conteudo;  }
        public List<Tag> Tags { get => tags;  }
    }
}
