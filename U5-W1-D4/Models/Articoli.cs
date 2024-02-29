using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U5_W1_D4.Models
{
    public class Articoli
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public string Descrizione { get; set; }
        public string ImmagineCopertina { get; set; }
        public string ImmagineAggiuntiva1 { get; set; }
        public string ImmagineAggiuntiva2 { get; set; }
        public bool Visibile { get; set; }
    }
}