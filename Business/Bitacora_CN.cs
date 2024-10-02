using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Bitacora_CN
    {
        private static Bitacora_CN _instance;
        private BitacoraRepository _repository;

        public Bitacora_CN()
        {
            _repository = new BitacoraRepository();
        }
        public static Bitacora_CN GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new Bitacora_CN();
                return _instance;
            }
        }
        public List<Evento_CE> Listar()
        {
            try
            {
                return _repository.Listar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Evento_CE> Listar(Filtro filtro)
        {
            _repository = new BitacoraRepository();
            return _repository.GetEvents(filtro);
        }
        public void LogEvent(int usuario, string modulo, string operacion, int criticidad, string msj)
        {
            _repository.LogEvent(usuario, modulo, operacion, criticidad, msj);
        }
    }
}
