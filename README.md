# Fiap Tech Challenge - Fase 2

Projeto referente a entrega da fase 2 do Tech Challenge do curso Software Architecture Pós Tech - turma 5SOAT.

## Requisitos do negócio

O back-end deve permitir:

- Receber novos pedidos
- Consultar pedido realizado
- Consultat lista de pedidos ativos
- Atualizar status do pedido
- Adicionar diversos produtos a um pedido
- Opção de pagamento integrada para MVP
- Consultar status do pagamento
- Acompanhar o status de cada pedido
- Cadastrar produtos por categoria

## Requisitos de infraestrutura

![image](https://github.com/AlanFernandes86/FiapTechChallenge/assets/62666226/19a267b8-392b-4505-bcfd-f7bec4301ec0)

## Link do swagger

http://<ip-do-minekube>:31116/swagger/index.html

## Como executar o projeto

1 - Abrir o terminal e navegar até a pasta do projeto
2 - Executar os seguintes comando do kubectl na ordem
- kubectl apply -f sql-data.pv.yml
- kubectl apply -f sql-data.pvc.yml
- kubectl apply -f sql-server.statefulset.yml
- kubectl apply -f sql-server.service.yml
- kubectl apply -f techchallenge.api.deployment.yml
- kubectl apply -f techchallenge.api.service.yml
- kubectl apply -f sql-server.configmap.yml
- kubectl apply -f mssqltools.job.yml

## Link da collection incial no postman
https://github.com/AlanFernandes86/FiapTechChallenge/blob/feature/fase2/TechChallenge.postman_collection.json
