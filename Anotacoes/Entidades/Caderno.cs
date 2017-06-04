using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anotacoes.Entidades
{
    class Caderno
    {
        private int? id;
        private string titulo;
        private List<Nota> notas;

        public Caderno(string titulo, int? id = null)
        {
            this.id = id;
            this.titulo = titulo;
            this.notas = new List<Nota>();
        }

        public int? Id { get => id;}
        public string Titulo { get => titulo;}
        public List<Nota> Notas { get => notas; set => notas = value; }
    }
}
