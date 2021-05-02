﻿using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Obter notificações está sendo chamado de dentro de um método assíncrono,
            //mas como ele é síncrono, foi necessário o uso do Task.FromResult para possibilitar seu uso
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View();
        }

    }
}
