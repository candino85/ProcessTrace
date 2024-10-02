using Servicios.Composite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Datos
{
    public class PermissionRepository
    {
        protected string accesso = ConfigurationManager.ConnectionStrings["ProcessTrace"].ConnectionString.ToString();

        public Array GetAllPermissions()
        {
            return Enum.GetValues(typeof(PermissionType));
        }

        public IList<Servicios.Composite.Component> GetAll(string role)
        {
            var where = "is null";

            if (!string.IsNullOrEmpty(role))
            {
                where = role;
            }

            var cnn = new SqlConnection(accesso);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var query = $@"with recursivo as (
                        select sp2.id_padre, sp2.id_hijo  from Permiso_Permiso SP2
                        where sp2.id_padre = {where} --acá se va variando la familia que busco
                        UNION ALL 
                        select sp.id_padre, sp.id_hijo from Permiso_Permiso sp 
                        inner join recursivo r on r.id_hijo= sp.id_padre
                        )
                        select r.id_padre,r.id_hijo,p.Id,p.Nombre, p.Permiso
                        from recursivo r 
                        inner join Permiso p on r.id_hijo = p.Id";

            cmd.CommandText = query;

            var reader = cmd.ExecuteReader();
            var list = new List<Servicios.Composite.Component>();

            while (reader.Read())
            {
                int id_padre = 0;
                if (reader["id_padre"] != DBNull.Value)
                {
                    id_padre = reader.GetInt32(reader.GetOrdinal("id_padre"));
                }

                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("nombre"));
                var permission = string.Empty;

                if (reader["permiso"] != DBNull.Value)
                    permission = reader.GetString(reader.GetOrdinal("permiso"));

                Servicios.Composite.Component component;

                if (string.IsNullOrEmpty(permission))// este campo identifica el tipo de permiso, solo las patentes van a tener un permiso del sistema relacionado
                {
                    component = new Role();
                }
                else
                {
                    component = new Permission();
                    component.Permission = permission;
                }
                component.Id = id;
                component.Name = name;

                var parent = GetComponent(id_padre, list);

                if (parent == null)
                    list.Add(component);
                else
                    parent.AddChild(component);
            }
            reader.Close();
            cnn.Close();
            return list;
        }

        public IList<Role> GetAllRolesFromDB()
        {
            var cnn = new SqlConnection(accesso);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var query = "select id, nombre, permiso from permiso where permiso is null";

            cmd.CommandText = query;
            var reader = cmd.ExecuteReader();

            Role role;
            List<Role> roleList = new List<Role>();


            while (reader.Read())
            {
                role = new Role();

                role.Id = reader.GetInt32(reader.GetOrdinal("id"));
                role.Name = reader.GetString(reader.GetOrdinal("nombre"));

                roleList.Add(role);
            }

            reader.Close();
            cnn.Close();

            return roleList;
        }

        public IList<Permission> GetAllPermissionsFromDB()
        {
            var cnn = new SqlConnection(accesso);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var query = $@"select id, nombre, permiso from permiso where permiso is not null";

            cmd.CommandText = query;

            var reader = cmd.ExecuteReader();

            List<Permission> permissionsList = new List<Permission>();


            while (reader.Read())
            {
                Permission permission = new Permission();

                permission.Id = reader.GetInt32(reader.GetOrdinal("id"));
                permission.Name = reader.GetString(reader.GetOrdinal("nombre"));
                permission.Permission = reader.GetString(reader.GetOrdinal("permiso"));

                permissionsList.Add(permission);
            }

            reader.Close();
            cnn.Close();

            return permissionsList;
        }

        public void SaveRole(Role role)
        {
            try
            {
                var cnn = new SqlConnection(accesso);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;
                var query = $@"delete from permiso_permiso  where id_padre = @id";

                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("id", role.Id));
                cmd.ExecuteNonQuery();

                foreach (var child in role.GetChild)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;

                    query = $"insert into permiso_permiso (id_padre, id_hijo) values (@id_padre, @id_hijo)";

                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("id_padre", role.Id));
                    cmd.Parameters.Add(new SqlParameter("id_hijo", child.Id));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Servicios.Composite.Component SaveComponent(Servicios.Composite.Component component)
        {
            try
            {
                var cnn = new SqlConnection(accesso);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var query = $@"insert into permiso (nombre, permiso) values (@nombre, @permiso); select id from permiso where id = @@identity;";

                cmd.Parameters.Add(new SqlParameter("nombre", component.Name));
                //cmd.Parameters.Add(new SqlParameter("permiso", component.Permission));
                cmd.CommandText = query;

                if (component is Role)
                    cmd.Parameters.Add(new SqlParameter("permiso", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("permiso", component.Permission.ToString()));


                component.Id = (int)cmd.ExecuteScalar();
                return component;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool DeletePermissionFromRole(Servicios.Composite.Component permission)
        {
            using (SqlConnection oConexion = new SqlConnection(accesso))
            {
                bool respuesta = false;

                var query = $"delete from permiso_permiso where id_padre = {permission.Parent.Id} and id_hijo = {permission.Id}";
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el permiso", ex);
                }
            }
        }

        public int DeleteComponent(Servicios.Composite.Component component)
        {
            var query = string.Empty;

            if (component is Role)
                query = "delete from permiso where id = @id ; delete from permiso_permiso where id_padre = @id";
            else
                query = "delete from permiso where id = @id ; delete from permiso_permiso where id_hijo = @id";
            var cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("id", component.Id));
            cmd.CommandText = query;

            //int result = accesso.DeleteCommand(cmd);
            //return result;
            return 1;
        }

        public Servicios.Composite.Component GetComponentById(Servicios.Composite.Component component, int id)
        {
            Servicios.Composite.Component componentToRemove = new Role();

            var query = $"select id_padre, ID_Hijo FROM permiso_permiso where Id_Hijo = {id} AND Id_Padre = {component.Id}";
            var cmd = new SqlCommand();
            cmd.CommandText = query;

            DataTable dt = new DataTable();
            //dt = accesso.Read(cmd);

            foreach (DataRow dr in dt.Rows)
            {
                Permission permission = new Permission
                {
                    Id = (int)dr["Id_Hijo"],
                    Parent = (Role)dr["Id_Padre"]
                };
                componentToRemove.AddChild(permission);
            }
            return componentToRemove;
        }
        public Servicios.Composite.Component GetComponent(Servicios.Composite.Component component)
        {
            var query = "select nombre, id, permiso from permiso where id = @id";
            var cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("id", component.Id));
            cmd.CommandText = query;


            if (component.Parent is null)
                cmd.Parameters.Add(new SqlParameter("permiso", DBNull.Value));
            else
                cmd.Parameters.Add(new SqlParameter("permiso", component.Permission.ToString()));

            //component.Id = accesso.WriteCommand(cmd);
            return component;
        }

        public Servicios.Composite.Component GetComponent(int id, IList<Servicios.Composite.Component> list)
        {
            Servicios.Composite.Component component = list != null ? list.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (component == null && list != null)
            {
                foreach (var c in list)
                {
                    var r = GetComponent(id, c.GetChild);
                    if (r != null && r.Id == id) return r;
                    else
                    if (r != null)
                        return GetComponent(id, r.GetChild);
                }
            }
            return component;
        }

        public bool FindUserPermissions(PermissionType permissionType, UserPermission userPermission)
        {
            var cnn = new SqlConnection(accesso);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"select p.Id, p.Nombre, p.Permiso from permiso p inner join usuario_permiso up on up.id_permiso = p.id where up.id_usuario = @id";
            cmd.Parameters.AddWithValue("@id", userPermission.Id);
            var reader = cmd.ExecuteReader();

            userPermission.Permissions.Clear();
            while (reader.Read())
            {
                var idPermiso = reader.GetInt32(reader.GetOrdinal("id"));
                var nombrePermiso = reader.GetString(reader.GetOrdinal("nombre"));
                var permisoPermiso = string.Empty;

                if (reader["permiso"] != DBNull.Value)
                    permisoPermiso = reader.GetString(reader.GetOrdinal("permiso"));

                Servicios.Composite.Component component;
                if (!string.IsNullOrEmpty(permisoPermiso)) // un solo permiso
                {
                    component = new Permission()
                    {
                        Id = idPermiso,
                        Name = nombrePermiso,
                        Permission = permisoPermiso
                    };
                    userPermission.Permissions.Add(component);
                }
                else // un conjunto de permisos
                {
                    component = new Role()
                    {
                        Id = idPermiso,
                        Name = nombrePermiso
                    };
                    var p = GetAll($"={component.Id}");

                    foreach (var permission in p)
                        component.AddChild(permission);

                    userPermission.Permissions.Add(component);
                }
            }

            bool exist = false;

            foreach (var p in userPermission.Permissions)
            {
                if (p.Permission.Equals(permissionType))
                    return true;
                else
                {
                    exist = IsInRole(p, permissionType, exist);
                    if (exist) return true;
                }
            }
            reader.Close();
            return exist;
        }

        bool IsInRole(Servicios.Composite.Component c, PermissionType pt, bool exist)
        {
            if (c.Permission.Equals(pt)) // un permiso
                exist = true;
            else
            {
                foreach (var p in c.GetChild)
                {
                    exist = IsInRole(p, pt, exist); // recursividad para recuperar todos los permisos
                    if (exist) return true;
                }
            }
            return exist;
        }

        public int GetUserRole(int idUser)
        {
            try
            {
                var cnn = new SqlConnection(accesso);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var query = $"select id_permiso from usuario_permiso where id_usuario = {idUser}";

                cmd.CommandText = query;

                var result = cmd.ExecuteScalar();
                if (result != null)
                    return (int)result;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int SaveUserPermission(int idUser, int idPermission)
        {
            try
            {
                var cnn = new SqlConnection(accesso);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var query = $@"if exists (select * from Usuario_Permiso where Id_Usuario = {idUser})
	                            update Usuario_Permiso set Id_Permiso = {idPermission} where Id_Usuario = {idUser}
                            else
	                            insert into usuario_permiso (id_usuario, id_permiso) values ({idUser},{idPermission})";

                cmd.CommandText = query;

                var result = cmd.ExecuteNonQuery();

                if (result != 0)
                    return result;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
