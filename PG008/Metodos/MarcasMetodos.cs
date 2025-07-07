using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Metodos
{
    public class MarcasMetodos
    {
        private static MarcasMetodos _instance = null;

        public MarcasMetodos()
        {
        }

        public static MarcasMetodos Instancia
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarcasMetodos();
                }
                return _instance;
            }
        }
        
    }
}