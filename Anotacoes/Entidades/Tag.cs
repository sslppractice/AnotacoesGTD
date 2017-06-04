using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anotacoes.Entidades
{
    class Tag
    {
        private Nullable<int> id;
        private string titulo;

        public Tag(string Titulo, Nullable<int> id = null)
        {
            this.id = id;
            this.titulo = Titulo;
        }

        public int? Id { get => id;}
        public string Titulo { get => titulo;}
    }
}
