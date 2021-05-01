using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IJogoService: IDisposable
    {
        Task<List<JogosViewModel>> Obter(int pag, int quantidade);
        Task<JogosViewModel> Obter(Guid id);
        Task<JogosViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(JogoInputModel jogo, Guid id);
        Task Atualizar(Guid id,double preco);
        Task Remover(Guid id);
    }
}
