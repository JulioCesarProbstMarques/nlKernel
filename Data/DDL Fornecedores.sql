-- dbKernel.tb_for_fornecedores definição

CREATE TABLE `tb_for_fornecedores` (
  `FOR_ID` int(11) NOT NULL AUTO_INCREMENT,
  `FOR_NAM_FAN` varchar(200) NOT NULL,
  `FOR_CNPJ` varchar(20) NOT NULL,
  `FOR_END_COM` varchar(500) NOT NULL,
  `FOR_EML_CON` varchar(100) NOT NULL,
  `FOR_TEL_CON` varchar(20) NOT NULL,
  `FOR_STA_ACT` char(1) NOT NULL DEFAULT 'A',
  `FOR_DTA_INC` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  PRIMARY KEY (`FOR_ID`),
  UNIQUE KEY `UN_FOR_CNPJ` (`FOR_CNPJ`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


-- dbKernel.tb_end_fornecedores definição

CREATE TABLE `tb_end_fornecedores` (
  `END_ID` int(11) NOT NULL AUTO_INCREMENT,
  `FOR_ID` int(11) NOT NULL,
  `END_CEP` varchar(9) NOT NULL,
  `END_LOG` varchar(150) NOT NULL,
  `END_NUM` varchar(10) NOT NULL,
  `END_CPL` varchar(50) DEFAULT NULL,
  `END_BAI` varchar(100) NOT NULL,
  `END_CID` varchar(100) NOT NULL,
  `END_EST` char(2) NOT NULL,
  PRIMARY KEY (`END_ID`),
  KEY `fk_end_for` (`FOR_ID`),
  CONSTRAINT `fk_end_for` FOREIGN KEY (`FOR_ID`) REFERENCES `tb_for_fornecedores` (`FOR_ID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
