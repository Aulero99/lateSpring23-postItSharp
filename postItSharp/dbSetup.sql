CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE albums(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  category VARCHAR(255) NOT NULL DEFAULT "misc",
  coverImg VARCHAR(500) NOT NULL,
  archived BOOLEAN NOT NULL DEFAULT false,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO albums
(title, category, coverImg, archived, creatorId)
VALUES
("Retro Pugs", "pugs", "https://images.unsplash.com/photo-1575425186775-b8de9a427e67?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=687&q=80", false, '645d75fdfdcb015333f9b0ba');
INSERT INTO albums
(title, category, coverImg, archived, creatorId)
VALUES
("Game Time", "games", "https://images.unsplash.com/photo-1575425186775-b8de9a427e67?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=687&q=80", false, '645d75fdfdcb015333f9b0ba');
INSERT INTO albums
(title, category, coverImg, archived, creatorId)
VALUES
("Music", "aesthetics", "https://images.unsplash.com/photo-1575425186775-b8de9a427e67?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=687&q=80", false, '645d75fdfdcb015333f9b0ba');
INSERT INTO albums
(title, category, coverImg, archived, creatorId)
VALUES
("Fast Cars", "games", "https://images.unsplash.com/photo-1575425186775-b8de9a427e67?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=687&q=80", false, '634844a08c9d1ba02348913d');

DELETE FROM albums where id = 1;

DELETE FROM accounts WHERE id = '645d75fdfdcb015333f9b0ba';



SELECT
*
FROM albums 
JOIN accounts ON albums.creatorId = accounts.id
WHERE accounts.id = '634844a08c9d1ba02348913d';


SELECT
alb.*,
creator.*
FROM albums alb
JOIN accounts creator ON alb.creatorId = creator.id
WHERE creator.id = '634844a08c9d1ba02348913d';
SELECT
alb.*,
creator.*
FROM albums alb
JOIN accounts creator ON alb.creatorId = creator.id
WHERE alb.id = 22;


SELECT
 title,
  name
FROM albums 
JOIN accounts ON albums.creatorId = accounts.id
WHERE accounts.id = '634844a08c9d1ba02348913d';