# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="/1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

O problema apresentado e a proposta de resolução desse problema foram levantados e tratados pelos membros da equipe com o estudo e pesquisa sobre o modelo de negócio utilizado por grandes empresas para um controle de estoque eficiente que atenda às necessidades do mercado. 

## Personas

As personas foram levantadas para um melhor o processo de entendimento do problema e definição dos requisitos do sistema, foram baseadas em profissionais do setor de logística através de entrevista e questionário sobre suas necessidades. 

![Persona 1](/docs/img/persona1.png)
![Persona 2](/docs/img/persona2.png)
![Persona 3](/docs/img/persona3.png)
![Persona 4](/docs/img/persona4.png)

## Histórias de Usuários

A partir do levantamento do perfil e da necessidade de cada persona foram registradas as seguintes histórias de usuário

| EU COMO...  | QUERO| PARA ...                |
| -------------------- | ---------------------------------- | -------------------------------------- |
| João   | entrar no aplicativo com meu próprio perfil           | ter acesso a ferramentas que são importantes ao meu uso.                |
| João        | consultar se existe um determinado produto no estoque                 | prestar um atendimento rápido, sem perder tempo procurando algo que não está em estoque.  |
| João   |    informações atualizadas sobre a quantidade de determinado produto no estoque  | comunicar ao meu superior quando há falta de determinado produto. |
| João | buscar produtos similares | atender a determinadas demandas de produtos que possam ser substituídos. |
| João | ter acesso a informações sobre a localização de um produto  | prestar meu atendimento mais rápido, evitando buscas desnecessárias. |
| João |  ter acesso a informações sobre a estratégia de saída de determinado produto  | garantir que o produto a sair seja adequado a estratégia da empresa.  |
| João |  registrar as informações sobre a entrada de produtos ao estoque  | que o setor responsável possa ser capaz de aferir o processo de renovação de estoque.  |
| João |  ter acesso às informações sobre as possíveis entradas de produtos que podem ocorrer durante meu turno de trabalho  | estar me preparar adequadamente para a tarefa.  |
| João |  registrar a saída de produtos do estoque  | manter as informações sobre o estoque atualizadas.  |
| João |  registrar a ocorrência de produtos que não estiverem aptos ao uso  | que os responsáveis possam tomar decisões para garantir o funcionamento da empresa.  |
| João |  registrar relatórios sobre o inventario do estoque  | que os responsáveis possam ter controle sobre o funcionamento adequado do estoque.  |
| João |  ser notificado quando for necessário fazer o inventario fora do prazo rotineiro  | garantir que o processo seja realizado adequadamente.  |
| Maria |  registrar a solicitação de reposição de estoque  | garantir que todos os setores ligados a reposição estejam cientes e tenham informações corretas sobre o procedimento.  |
| Maria |  a possibilidade de editar uma solicitação de reposição de estoque quando necessário  | garantir que as reposições estejam sempre atualizadas em relação ao processo.  |
| Maria |  cadastrar, visualizar e alterar informações sobre o fornecedor  | mediar a solução de problemas que venham a ocorrer.  |
| Maria |  cadastrar, visualizar e alterar informações sobre um determinado produto  | garantir a integridade das informações.  |
| Maria |  emitir relatórios que possam ser filtrados sobre a reposição de estoque  | visualizar de forma mais ampla o procedimento como um todo.  |
| Maria |  ser notificada quando uma reposição de estoque ocorrer  | ter controle e auxiliar na tomada de decisões quando problemas ocorrerem.  |
| Maria |  emitir relatórios de perdas de produtos  | auxiliar no planejamento de reposições futuras.  |
| Maria |  agendar a execução periódica de inventario  | auxiliar no planejamento de reposições futuras e aferir o funcionamento do estoque da empresa.  |
| Maria |  agendar a execução extraordinária de inventario  | se adequar a situações extraordinárias no planejamento de reposições futuras e aferir o funcionamento do estoque da empresa.  |
| Mauro |  aprovar ou rejeitar a inclusão ou alteração das informações sobre produtos  | ter controle sobre os procedimentos e garantir a integridade deles.  |
| Mauro |  emitir relatórios sobre a saída de produtos passados do estoque  | considerar período em que determinado produto, irá precisar de uma quantidade maior a disposição para evitar ruptura no PDV.  |
| Priscila |  aprovar ou rejeitar uma solicitação para reposição de estoque  | garantir que o processo ocorra dentro das limitações estratégicas e financeiras da empresa.  |
| Priscila |  emitir relatórios sobre o balanço de solicitações de reposição de estoque em determinados períodos  | fiscalizar e garantir a lisura do processo de reposição de estoque.  |
| Priscila |  registrar o pagamento de solicitações de reposição de estoque  | agrupar as informações relacionadas a reposição de estoque em uma única plataforma.  |
| Priscila |  visualizar informações sobre a data de vencimento para pagamentos de solicitações de reposição  | garantir que os compromissos sejam honrados e os setores responsáveis possam argumentar com o fornecedor em caso de problemas.  |
| Priscila |  registar e controlar formas de pagamento para uma determinada solicitação de reposição  | ter controle sobre demandas que possam se prolongar.  |


## Requisitos do Projeto

Os requisitos do sistema são de extrema importância para um bom desenvolvimento do projeto e para atender de maneira satisfatória os usuários do sistema. Os requisitos foram levantados com embasamento nas histórias de usuários. 

### Requisitos Funcionais  

A tabela a seguir apresenta os requisitos do projeto, identificando a prioridade em que os mesmos devem ser entregues. 

| ID     | Descrição do Requisito                  | Prioridade |
| ------ | --------------------------------------- | ---------- |
| RF-001 | O sistema deve possuir sistema de Cadastro e de Login  | ALTA       |
| RF-002 | O sistema deve receber o Cadastro de novos produtos, que contenham informações como (localização do produto, categorias e quantidade por embalagem)    | ALTA      |
| RF-003 |  O sistema deve possuir um meio de consulta para encontrar os produtos por nome, categoria, código ou fornecedor    | ALTA      |
| RF-004 | O sistema deve permitir visualizar as informações dos produtos    | ALTA      |
| RF-005 | O sistema deve permitir alteração das informações do produto    | ALTA      |
| RF-006 |  O sistema deve permitir ao Superior aprovar ou rejeitar alterações nas informações dos produtos    | ALTA      |
| RF-007 | O sistema deve receber entrada e saída de produtos e modificar na quantidade deste    | ALTA      |
| RF-008 |  O sistema deve gerar relatórios após o usuário fazer Login com as reposições e saída de produtos    | ALTA      |
| RF-009 | O sistema deve gerar relatório sobre o inventário para o Superior     | ALTA      |
| RF-010 | O sistema deve registrar itens que possua má qualidade     | MÉDIA      |
| RF-011 | O sistema deve informar quando algum produto estiver próximo da validade    | ALTA      |
| RF-012 | O sistema deve informar quando um produto estiver com baixa quantidade    | ALTA      |
| RF-013 | O sistema deve permitir agendar execução periódica do inventário    | BAIXA      |
| RF-014 | O sistema deve informar a perda de produtos    | MÉDIA      |
| RF-015 | O sistema deve permitir cadastrar, visualizar e alterar informações sobre o fornecedor   | MÉDIA      |
| RF-016 | O sistema deve registrar a solicitação de reposição   | ALTA      |
| RF-017 | O sistema deve permitir a edição da reposição de estoque    | ALTA      |
| RF-018 | O sistema deve permitir ao Supervisor aprovar ou rejeitar a solicitação de reposição    | ALTA      |
| RF-019 | O sistema deve informar a situação do pagamento da reposição     | MÉDIA      |


### Requisitos não Funcionais

A tabela a seguir apresenta os requisitos não funcionais que o projeto deverá atender. 

| ID      | Descrição do Requisito                                            | Prioridade |
| ------- | ----------------------------------------------------------------- | ---------- |
| RNF-001 | O sistema deve funcionar nos principais navegadores do mercado (Chrome, Firefox, Edge, Brave...)  | ALTA      |
| RNF-002 | O sistema deve ser responsivo em celulares, tablets e desktop/notebook  | ALTA      |
| RNF-003 |  O sistema deve seguir as diretrizes de acessibilidade (WCAG)  | ALTA      |
| RNF-004 | O sistema deve encriptar a senha do usuário no banco de dados  | ALTA      |
| RNF-005 | O sistema deve gerenciar permissões de acordo com o cargo do usuário informado no momento do cadastro  | ALTA      |
| RNF-006 |  O cadastro de usuário deve seguir a Lei Geral de Proteção de Dados Pessoais (LGPD)  | ALTA      |
| RNF-007 | O tempo de carregamento da página deve ser menor que 5 segundos em 90% dos casos  | MÉDIA      |
| RNF-008 |  O tempo de carregamento de pesquisas e filtros do sistema deve ser menor que 5 segundos em 90% dos casos  | MÉDIA |
| RNF-009 | O tempo de geração de relatório deve ser menor que 1 minuto em 90% dos casos  | MÉDIA      |
| RNF-010 | O sistema deve permitir, simultaneamente, a quantidade de usuários estabelecido em contrato com o cliente  | ALTA      |
| RNF-011 | O sistema deve permitir exportar relatório  | MÉDIA      |

## Restrições

As questões que limitam a execução desse projeto e que se configuram como obrigações claras para o desenvolvimento do projeto em questão são apresentadas na tabela a seguir. 

| ID  | Restrição                                             |
| --- | ----------------------------------------------------- |
| RE-01   | A Etapa 1 do projeto deverá ser entregue até dia 11 de setembro de 2022  |
| RE-02   | A documentação do projeto deverá estar disponível no Github         |


## Diagrama de Casos de Uso

Segue na imagem abaixo o diagrama de caso de uso que retrata de forma abstrata as principais funções a serem desenvolvidas pelo sistema, demonstrando a interação entre os usuários (atores) e o sistema. 

![Diagrama de casos de uso](/docs/img/diagrama-casos-de-uso.png)<br>
*O diagrama foi criado utilizando a ferramenta de edição no site SmartDrawn.*
