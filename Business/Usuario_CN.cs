using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocio
{
    public class Usuario_CN
    {
        private UsuarioRepository _usuario_Repo;
        public Usuario_CN()
        {
            _usuario_Repo = new UsuarioRepository();
        }
        public List<Usuario_CE> Listar()
        {
            try
            {
                return _usuario_Repo.Listar();
            }
            catch (Exception)
            {

                throw;
            }          
        }
        public Usuario_CE Obtener(int id)
        {
            try
            {
                return _usuario_Repo.Obtener(id);
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public bool Crear(Usuario_CE oUsuario)
        {
            try
            {
                if(string.IsNullOrEmpty(oUsuario.Email))
                    throw new OperationCanceledException("El email no puede ser vacio");

                return _usuario_Repo.Crear(oUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Editar(Usuario_CE oUsuario)
        {
            try
            {
                var encontrado = _usuario_Repo.Obtener(oUsuario.Id);
                
                if (encontrado.Id == 0)
                    throw new OperationCanceledException("El usuario no existe");

                return _usuario_Repo.Editar(oUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Eliminar(int id)
        {
            try
            {
                var encontrado = _usuario_Repo.Obtener(id);

                if (encontrado.Id == 0)
                    throw new OperationCanceledException("El usuario no existe");

                return _usuario_Repo.Eliminar(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
