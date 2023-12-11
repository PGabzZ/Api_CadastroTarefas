﻿using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;
using Tarefas.Repositorios.Interfaces;

namespace Tarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext) {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi enccontrado no banco de dados.");
            }
                _dbContext.Usuarios.Remove(usuarioPorId);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
            {
                UsuarioModel usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi enccontrado no banco de dados.");
            }
                    usuarioPorId.Nome = usuario.Nome;
                    usuarioPorId.Email = usuario.Email;

                    _dbContext.Usuarios.Update(usuarioPorId);
                    await _dbContext.SaveChangesAsync();

                    return usuarioPorId;
                }
            }


        }
    
