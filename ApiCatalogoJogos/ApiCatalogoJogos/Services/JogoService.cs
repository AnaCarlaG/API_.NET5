using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository jogoRepository;
        public JogoService(IJogoRepository jogoRepository)
        {
            jogoRepository = jogoRepository;
        }
        public async Task Atualizar(JogoInputModel jogo, Guid id)
        {
            var entidadeJogo = await jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoCadastradoException();

            entidadeJogo.Preco = preco;

            await jogoRepository.Atualizar(entidadeJogo);
        }

        public void Dispose()
        {
            jogoRepository?.Dispose();
        }

        public async Task<JogosViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoCadastradoException();

            var jogoInsert = new Jogo{
                id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
            await jogoRepository.Inserir(jogoInsert);

            return new JogosViewModel
            {
                id = jogoInsert.id,
                Nome = jogoInsert.Nome,
                Preco = jogoInsert.Preco,
                Produtora = jogoInsert.Produtora
            };
        }

        public async Task<List<JogosViewModel>> Obter(int pag, int quantidade)
        {
            var jogos = await jogoRepository.Obter(pag, quantidade);

            return jogos.Select(jogo => new JogosViewModel
            {
                id= jogo.id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogosViewModel> Obter(Guid id)
        {
            var jogo = await jogoRepository.Obter(id);

            if(jogoRepository == null)
            {
                return null;
            }

            return new JogosViewModel
            {
                id = jogo.id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoCadastradoException();

            await jogoRepository.Remover(id);
        }
    }
}
