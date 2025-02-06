# Servidor de Leilão Seguro

## Descrição
Este projeto implementa um servidor de leilão seguro, utilizando criptografia para proteger as informações enviadas e recebidas pelos clientes. O servidor gerencia conexões TCP de múltiplos clientes simultaneamente e garante a segurança dos dados através de criptografia assimétrica e simétrica.

## Funcionalidades
- Aceita conexões de múltiplos clientes simultaneamente.
- Utiliza criptografia AES para proteger os dados transmitidos entre o cliente e o leilão e entre o servidor e o leilão.
- Garante a segurança das chaves sasimétricas usando RSA entre o servidor e o cliente.
- Processa solicitações e respostas de forma segura.
- Armazena informações criptografadas no servidor.

## Tecnologias Utilizadas
- C#
- .NET
- TCP/IP
- AES (Advanced Encryption Standard)
- RSA (Rivest-Shamir-Adleman)
- JSON para troca de mensagens

## Fluxo de Execução
1. O servidor inicia e fica aguardando conexões de clientes.
2. O cliente se conecta e envia uma solicitação.
3. O servidor verifica se o cliente está registrado e possui chave pública.
4. Os dados do leilão são criptografados e enviados ao cliente.
5. O cliente decripta os dados utilizando sua chave privada.
6. O servidor processa novas requisições e continua aceitando conexões.

## Como Executar
1. Compile e execute o servidor.
2. Execute os clientes para conectar ao servidor.
3. Envie mensagens JSON conforme o formato esperado.
4. O servidor processa as solicitações e retorna as respostas criptografadas.

## Problemas Conhecidos e Soluções
- **Erro de "The input data is not a complete block"**: Verifique se os dados criptografados foram enviados corretamente e se os IVs e chaves coincidem entre criptografia e decriptação.
- **Porta ocupada após fechamento do servidor**: Certifique-se de encerrar corretamente o servidor ao fechar o programa.

## Autor
Ruan Sanchez

