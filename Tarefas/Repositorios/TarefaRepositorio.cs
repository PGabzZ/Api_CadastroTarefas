using Microsoft.EntityFrameworkCore;
using Tarefas.Data;
using Tarefas.Models;
using Tarefas.Repositorios.Interfaces;

namespace Tarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext) {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);
            if (tarefaPorId == null)
            {
                throw new Exception($"A tarefa para o ID: {id} não foi enccontrado no banco de dados.");
            }
                _dbContext.Tarefas.Remove(tarefaPorId);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
            {
            TarefaModel tarefaPorId = await BuscarPorId(id);
            if (tarefaPorId == null)
            {
                throw new Exception($"A tarefa para o ID: {id} não foi enccontrado no banco de dados.");
            }
                tarefaPorId.Nome = tarefa.Nome;
                tarefaPorId.Descricao = tarefa.Descricao;
                tarefaPorId.Status = tarefa.Status;
                tarefaPorId.UsuarioId = tarefa.UsuarioId;
                tarefaPorId.PrazoTarefa = tarefa.PrazoTarefa;


                    _dbContext.Tarefas.Update(tarefaPorId);
                    await _dbContext.SaveChangesAsync();

                    return tarefaPorId;
                }
            }


        }
    
