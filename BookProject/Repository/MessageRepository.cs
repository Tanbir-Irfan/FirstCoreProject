using System;
using BookProject.Models;
using Microsoft.Extensions.Options;

namespace BookProject.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _options;

        //private NewBookAlertConfig _options;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> options)
        {
            //second apporach
            this._options = options;

            //First approach
            //this._options = options.CurrentValue; 
            //options.OnChange(config =>
            //{
            //    _options = config;
            //});
        }

        public string GetName()
        {
            return this._options.CurrentValue.BookMsg;
        }

    }
}
