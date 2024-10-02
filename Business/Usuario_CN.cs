using System;
using System.Collections.Generic;
using Entidades;
using Datos;
using Servicios;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuario_CN
    {
        private UsuarioRepository _usuario_Repo;
        private Bitacora_CN _bitacora;
        public Usuario_CN()
        {
            _usuario_Repo = new UsuarioRepository();
            _bitacora = Bitacora_CN.GetInstance;
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
        public async Task<bool> Crear(Usuario_CE oUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(oUsuario.Email))
                    throw new OperationCanceledException("El email no puede ser vacio");

                var existe = _usuario_Repo.Obtener(oUsuario.Email);
                if(existe.Id != 0)
                    throw new OperationCanceledException("El email ya se encuentra registrado");

                string randonPass = Encrypt.GetRandomPassword();
                oUsuario.Clave = Encrypt.GetSHA256(randonPass);

                bool resultado = _usuario_Repo.Crear(oUsuario);

                if (resultado)
                {
                    await EmailSender.NotificarContraseña(oUsuario.Email, randonPass, oUsuario.Email);
                    return resultado;
                }
                else
                {
                    return false;
                }
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
        public string LogIn(string email, string password)
        {
            var usuario = _usuario_Repo.Obtener(email);

            if (SessionManager.GetInstance.IsLogged)
            {
                _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 1, "Ya existe una sesión iniciada.");
                return "Ya existe una sesión iniciada.";
            }
            else if (usuario.Nombre == null)
            {
                _bitacora.LogEvent(0, "Auth", "Log In", 3, $"El usuario {email} no existe.");
                return $"El usuario '{email}' no existe.";
            }
            else if (!usuario.Activo)
            {
                _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 2, $"El usuario {email} se encuentra inactivo.");
                return $"El usuario {email} se encuentra inactivo, contacte a RRHH.";
            }
            else if (usuario.Bloqueado)
            {
                _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 2, $"El usuario {email} se encuentra bloqueado.");
                return $"El usuario {email} se encuentra bloqueado, contacte a un administrador para desbloquearlo.";
            }
            else if (Encrypt.GetSHA256(password).Equals(usuario.Clave))
            {
                SessionManager.GetInstance.Login(usuario);
                usuario.IntentosAcceso = 0;
                usuario.UltimoAcceso = DateTime.Now;
                _usuario_Repo.LoginExitoso(usuario);

                _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 1, $"El usuario {email} se logueo correctamente.");
                return $"Bienvenido {usuario.Nombre} {usuario.Apellido}";
            }
            else
            {
                usuario.IntentosAcceso++;
                var msg = "Contraseña incorrecta";
                if ((usuario.IntentosAcceso) < 3)                 //si la cantidad de intentos es menor a 3
                    msg += $", le quedan {3 - usuario.IntentosAcceso} intentos restantes antes de bloquear su usuario.";
                LoginFallido(usuario);
                if ((usuario.IntentosAcceso) > 2)                  //si la cantidad de intentos es mayor a 2
                {
                    usuario.Bloqueado = true;
                    _usuario_Repo.Editar(usuario);
                    _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 1, $"El usuario {email} ha sido bloqueado.");
                    msg += ", el usuario ha sido bloqueado.";
                    //_integrityRepo.SetDV("Usuarios");
                }
                _bitacora.LogEvent(usuario.Id, "Auth", "Log In", 3, $"El usuario {email} ingresó incorrectamente la contraseña.");
                return msg;
            }
        }
        public void LogOut()
        {
            if (!SessionManager.GetInstance.IsLogged)
                throw new Exception("No hay sesión iniciada");
            _bitacora.LogEvent(SessionManager.GetInstance.usuario.Id, "Auth", "Log Out", 1, $"El usuario {SessionManager.GetInstance.usuario.Email} ha finalizado su sesión correctamente.");
            SessionManager.GetInstance.Logout();
        }
        public void LoginFallido(Usuario_CE usuario)
        {
            _usuario_Repo.LoginFallido(usuario);
            //_integrityRepo.SetDV("Usuarios");
            _bitacora.LogEvent(usuario.Id, "Auth", "Fallo de Log In", 3, $"El usuario {usuario.Email} ha ingresado mal la clave {usuario.IntentosAcceso} vez/veces.");
        }
        public void ActualizarClave(Usuario_CE usuario)
        {
            _usuario_Repo.CambiarClave(usuario);
            _bitacora.LogEvent(usuario.Id, "Auth", "Cambio de clave", 1, $"El usuario {usuario.Email} ha cambiado su clave correctamente.");
        }
        public async Task<bool> ResetClave(Usuario_CE usuario)
        {
            var nuevaClave = Encrypt.GetRandomPassword();
            usuario.Clave = Encrypt.GetSHA256(nuevaClave);
            var resultado = _usuario_Repo.CambiarClave(usuario);            
            _bitacora.LogEvent(usuario.Id, "Auth", "Reseteo de clave", 1, $"Se ha reseteado la clave del usuario {usuario.Email} correctamente.");
            
            await EmailSender.NotificarContraseña(usuario.Email, nuevaClave, usuario.Email);

            return resultado;
        }
    }
}
