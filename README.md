# FiapTechChallenge

# Para inicializar o projeto
- Executar o comando `docker-compose up -d` na raiz do projeto.

# Setup inicial
- O docker compose irá inicializar a aplicação e o banco de dados.
- Toda vez que o `docker-compose up -d` for executado o banco de dados será reinicializado.
- O Swagger da aplicação estará disponível em https://localhost:63841/swagger/index.html
- Existe um delay de 10s após o sqlserver começar a ser inicado e o script de criação da database e tabales ser executado, caso o sqlserver demore muito para iniciar pode impedir a criação da database.
Nesse caso pare o container e execute novamente.
