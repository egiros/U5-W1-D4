﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U5_W1_D4.Models
{
    public class Utenti
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public bool Admin { get; set; }

    }
}