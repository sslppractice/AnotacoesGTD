using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Anotacoes;

namespace AnotacoesCons
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.evernote.com/shard/s449/notestore";
            string token, token_path = @"F:\Dropbox\Settings\evernote_token.txt";
            using (TextReader tr = new StreamReader(token_path))
            {
                token = tr.ReadToEnd();
            }
            AcessoEvernote acesso = new AcessoEvernote(token, url);

            //acesso.CarregarCadernos();
        }
    }
}
