using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anotacoes.Entidades
{
    class Grupo
    {
        private Nullable<int> id;
        private string titulo;

        public Grupo(string titulo, Nullable<int> id = null)
        {
            this.id = id;
            this.titulo = titulo;
        }

        public int? Id { get => id;}
        public string Titulo { get => titulo;}
    }
}
