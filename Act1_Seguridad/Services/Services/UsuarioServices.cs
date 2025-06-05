using Act1_Seguridad.Context;
using Act1_Seguridad.Services.IServices;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Act1_Seguridad.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        
        private readonly ApplicationDbContext _context; //inyeccion de dependencias del contexto de la base de datos
        public UsuarioServices(ApplicationDbContext context) { 
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Usuario>>> GetAll() //devueleve la lista de usuarios
        {
            try
            {

                List<Usuario> response = await _context.Usuarios.Include(x => x.Roles).ToListAsync();

                return new Response<List<Usuario>>(response);

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> GetById(int id)
        {
            try
            {
                //busca un usuario por su id
                Usuario usuario = await _context.Usuarios.Include(x => x.Roles).FirstOrDefaultAsync(x => x.PkUsuario == id);
                return new Response<Usuario>(usuario);

                //Usuario usuario = await _context.Usuarios.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error "+ex.Message);
            }
        }

        public async Task<Response<Usuario>>Create(UsuarioRequest request)
        {
            try
            {
                //crea un nuevo usuario con los datos del request
                Usuario usuario1 = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = request.FkRol,
                };
                _context.Usuarios.Add(usuario1);
                //_context.SaveChanges();
                // //guarda los cambios en la base de datos de forma asincrona
                await _context.SaveChangesAsync();
                return new Response<Usuario>(usuario1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        // Actualiza un usuario por su id
        public async Task<Response<Usuario>> Update(UsuarioRequest request, int id)
        {
            try
            {
                var response = _context.Usuarios.Find(id);
                response.Nombre = request.Nombre;
                response.UserName = request.UserName;
                response.Password = request.Password;
                response.FkRol = request.FkRol;
                //entity state modified para indicar que la entidad ha sido modificada
                _context.Entry(response).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new Response<Usuario>(response, "Se actualizo correctamente");

            }
            catch (Exception e)
            {

                throw new Exception("ocurrio un error: " + e.Message);
            }
        }

        public async Task<Response<Usuario>> Delete(int id)
        {
            try
            {
                Usuario response = _context.Usuarios.Find(id);
                _context.Usuarios.Remove(response);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(response, "Se elimino correctamente");
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrio un error " + e.Message);
            }
        }


    }
}
