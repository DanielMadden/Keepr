USE boisecodeworks;

CREATE TABLE keeps
(
id INT AUTO_INCREMENT,
creatorId VARCHAR(255),
name VARCHAR(255),
description VARCHAR(255),
img VARCHAR(255),
views INT,
shares INT,

PRIMARY KEY (id),

FOREIGN KEY (creatorId)
  REFERENCES profiles (id)
  ON DELETE CASCADE
);

CREATE TABLE vaults
(
  id INT AUTO_INCREMENT,
  creatorId VARCHAR(255),
  name VARCHAR(255),
  description VARCHAR(255),
  isPrivate TINYINT,

  PRIMARY KEY (id),

  FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE
);

CREATE TABLE vaultKeeps
(
  id INT AUTO_INCREMENT,
  creatorId VARCHAR(255),
  vaultId INT,
  keepId INT,

  PRIMARY KEY (id),

  FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

  FOREIGN KEY (vaultId)
    REFERENCES vaults (id)
    ON DELETE CASCADE,

  FOREIGN KEY (keepId)
    REFERENCES keeps (id)
    ON DELETE CASCADE
)
