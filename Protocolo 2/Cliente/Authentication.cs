using System;

namespace Client
{
    public class Authentication
    {
        private readonly string _username;
        private readonly string _password;

        public Authentication(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public bool Authenticate()
        {
            // Implemente a lógica de autenticação aqui
        }
    }
}
