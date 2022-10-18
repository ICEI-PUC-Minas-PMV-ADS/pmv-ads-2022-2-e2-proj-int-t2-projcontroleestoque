CREATE TABLE `movimentacaoProduto`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `registradPorId` INT NOT NULL,
    `solicitadoPorId` INT NOT NULL,
    `ProdutoId` INT NOT NULL,
    `motivo` VARCHAR(100) NOT NULL,
    `quantidade` INT NOT NULL,
    `dataMovimento` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `fornecedor`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `endereco` VARCHAR(100) NOT NULL,
    `telefone` VARCHAR(25) NOT NULL,
    `email` VARCHAR(50) NOT NULL,
    `criado` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
    `criadoPor` VARCHAR(50) NOT NULL,
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `produto`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL
    `nome` VARCHAR(100) NOT NULL,
    `descricao` VARCHAR(200) NOT NULL,
    `quantidade` INT NOT NULL,
    `fornecedorId` INT NOT NULL,
    `localizacao` VARCHAR(200) NOT NULL,
    `tags` VARCHAR(100) NOT NULL,
    `estrategiaConsumo` VARCHAR(200) NOT NULL,
    `validade` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
    `criado` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `inventario`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `realizadoPorId` INT NOT NULL,
    `solicitadoPorId` INT NOT NULL,
    `dataDeExecusao` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `dataDeCriacao` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `itemInventario`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `inventarioId` INT NOT NULL,
    `produtoId` INT NOT NULL,
    `observacao` VARCHAR(200) NOT NULL,
    `status` VARCHAR(40) NOT NULL,
    `quantidade` INT NOT NULL
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `itemSolicitacaoDeCompra`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `solicitacaoDeCompraId` INT NOT NULL,
    `fornecedorId` INT NOT NULL,
    `produtoId` INT NOT NULL,
    `valor` DOUBLE NOT NULL,
    `quantidade` INT NOT NULL
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE `solicitacaoDeCompra`(
	`id` INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    `usuarioId` INT NOT NULL,
    `valorTotal` INT NOT NULL,
    `dataCadastro` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `dataVencimento` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `responsavel` VARCHAR(100) NOT NULL,
    `autorizacao` BOOLEAN NOT NULL
) ENGINE=InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

CREATE TABLE admEstoque(
    usuarioId INT,
	admin BOOLEAN NOT NULL,
    autorizacao TEXT
    
);

CREATE TABLE notificacao(
	id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    receiverUsuarioId INT,
    titulo TEXT,
    corpo TEXT,
    criado DATETIME,
    criadoPor TEXT,
    destinatario TEXT
   
);

CREATE TABLE usuario(
	id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    nome TEXT,
    senha TEXT,
    funcao TEXT,
    dataCadastro DATETIME,
    email TEXT
);

CREATE TABLE ordemPagamento(
	id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	tipo INT,
    statusPagamento BOOL,
    numParcelas INT,
    dataPagamento DATE,
    statusParcela TEXT,
    valor FLOAT,
    solicitacaoDeCompraId INT
);

CREATE TABLE parcela(
	id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
    ordemDePagamentoId INT,
    numeroParcelas INT,
    dataPagamento DATE,
    statusParcela TEXT,
    valorParcela FLOAT
   
);

ALTER TABLE parcela ADD FOREIGN KEY (ordemDePagamentoId) REFERENCES ordemDePagamento(id);

ALTER TABLE ordemDePagamento ADD FOREIGN KEY (solicitacaoDeCompraId) REFERENCES solicitacaoDeCompra(id);

ALTER TABLE produto ADD FOREIGN KEY (fornecedorId) REFERENCES fornecedor(id);

ALTER TABLE movimentacaoProduto ADD FOREIGN KEY (registradoPorId) REFERENCES  usuario(id);
ALTER TABLE movimentacaoProduto ADD FOREIGN KEY (solicitadorPorId) REFERENCES usuario(id);
ALTER TABLE movimentacaoProduto ADD FOREIGN KEY (produtoId) REFERENCES produto(id);

ALTER TABLE itemInventario ADD FOREIGN KEY (inventarioId) REFERENCES inventario(id);
ALTER TABLE itemInventario ADD FOREIGN KEY (produtoId) REFERENCES produto(id);

ALTER TABLE inventario ADD FOREIGN KEY (solicitadorPorId) REFERENCES usuario(id);
ALTER TABLE inventario ADD FOREIGN KEY (realizadoPorId) REFERENCES usuario(id);

ALTER TABLE notificacao ADD FOREIGN KEY (receiverUsuarioId) REFERENCES usuario(id);

ALTER TABLE solicitacaoDeCompra ADD FOREIGN KEY (usuarioId) REFERENCES usuario(id);

ALTER TABLE admEstoque ADD FOREIGN KEY (usuarioId) REFERENCES usuario(id);

ALTER TABLE itemSolicitacaoDeCompra ADD FOREIGN KEY (fornecedorId) REFERENCES fornecedor(id);
ALTER TABLE itemSolicitacaoDeCompra ADD FOREIGN KEY (solicitacaoDeCompraId) REFERENCES solicitacaoDeCompra(id);
ALTER TABLE itemSolicitacaoDeCompra ADD FOREIGN KEY (produtoId) REFERENCES produto(id);
