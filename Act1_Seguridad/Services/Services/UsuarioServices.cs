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
        //el guion bajo represnta q esta protegido
        private readonly ApplicationDbContext _context;
        public UsuarioServices(ApplicationDbContext context) { 
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Usuario>>> GetAll()
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
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
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
                Usuario usuario1 = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = request.FkRol,
                };
                _context.Usuarios.Add(usuario1);
                //_context.SaveChanges();

                await _context.SaveChangesAsync();
                return new Response<Usuario>(usuario1);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }

        public async Task<Response<Usuario>> Update(UsuarioRequest request)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FindAsync(request.PkUsuario);
                Usuario response = new Usuario()
                {
                    Nombre = request.Nombre,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = request.FkRol,
                };
                _context.Usuarios.Update(response);
                await _context.SaveChangesAsync();
                return new Response<Usuario>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error " + ex.Message);
            }
        }


    }
}
