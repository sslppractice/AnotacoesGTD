using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anotacoes.Entidades;
using EvernoteSDK;

using EvernoteSDK.Advanced;
using Evernote.EDAM.Type;

namespace Anotacoes
{
    /// <summary>
    /// Pacote Nuget: evernote/evernote-cloud-sdk-windows
    /// https://github.com/evernote/evernote-cloud-sdk-windows/
    /// </summary>
    public class AcessoEvernote
    {
        private List<Caderno> cadernos;
        private List<Caderno> Cadernos { get => cadernos; }

        public List<string> TagsContexto()
        {
            List<string> lst = new List<string>();
            lst.AddRange(new string[] {"@at:home", "@at:work", "@at:anywhere" });
            return lst;
        }

        public AcessoEvernote(string SessionDevToken, string SessionNoteStoreUrl)
        {
            ENSession.SetSharedSessionDeveloperToken(SessionDevToken, SessionNoteStoreUrl);
            if (ENSession.SharedSession.IsAuthenticated == false)
            {
                ENSession.SharedSession.AuthenticateToEvernote();
            }

            this.cadernos = new List<Caderno>();
        }

        public void CarregarCadernos()
        {
            // Carrega os cadernos na lista
            foreach (ENNotebook ennobo in ENSession.SharedSession.ListNotebooks())
            {
                Caderno caderno = new Caderno(ennobo.Name, ennobo.GetHashCode());
                ENNoteSearch search = new ENNoteSearch(string.Empty);
                List<ENSessionFindNotesResult> NotesFounded = ENSession.SharedSession.FindNotes(search, 
                    ennobo, new ENSession.SearchScope(), new ENSession.SortOrder(), 9999);
                foreach (ENSessionFindNotesResult NoteResult in NotesFounded)
                {
                    Nota nota = new Nota(NoteResult.Title, string.Empty, null, NoteResult.GetHashCode());
                    caderno.Notas.Add(nota);
                }
                this.cadernos.Add(caderno);
            }
        }

        public void CriarNota(string Title, string Content, List<string> Tags)
        {
            // Create a new note (in the user's default notebook) with some plain text content.
            ENNote myPlainNote = new ENNote();
            myPlainNote.Title = Title;
            myPlainNote.Content = ENNoteContent.NoteContentWithString(Content);
            myPlainNote.TagNames = Tags; 
            ENNoteRef myPlainNoteRef = ENSession.SharedSession.UploadNote(myPlainNote, null);
        }
    }
}
