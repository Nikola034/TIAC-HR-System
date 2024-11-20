INSERT INTO accounts (id, email, password, refreshtoken, refreshtokenvalidto, passwordresettoken, passwordresettokenvalidto, isblocked)
VALUES
('387e3434-d303-4ebe-8109-f35b848b6f0f', 'john.doe@gmail.com', '$2a$11$kETI6AH7J5XMwtL05vOJGOos6kV9WxihWexCxRQP4QVUjx/JbYRdK', null, null, null, null, false),
('39491ef3-a1fa-4842-8fb1-8c8997622b52', 'jane.smith@gmail.com', '$2a$11$rtKfVHE7hxDFHZnZVFbUHO1EwMVdDvT/55fe8XfbmD0DDdlxeL5Sm', null, null, null, null, false),
('5a70e705-4c43-42fe-88f4-216e2598e97f', 'emily.doe@gmail.com', '$2a$11$19/dpXy/a8F9hrJPgwKTyOEshE9vtLFTh.RYUZ3tkUs.CsusaCah.', null, null, null, null, false),
('9eac01ad-37e5-4d49-82bf-ee445c691fad', 'stan.doe@gmail.com', '$2a$11$35Jj8APcNlp1Z8oXwzsT7uJyDw59i95y3C8HJr7YsqhwEr9aK/GNq', null, null, null, null, false),
('ae9071f9-1442-482d-8920-8e01b47d0c24', 'rachel.doe@gmail.com', '$2a$11$.yqa4Wfk768LqbAGIYmeROxmRaqh37OO78GykHWAjUwyX9jrNHE5G', null, null, null, null, false),
('b12f1dfd-9dbe-4f21-85b1-283c312301df', 'alan.doe@gmail.com', '$2a$11$DBgBsHkwhrXyj2d.Q6vgMuYsOcBW.u2.Ld8QvtBDMN7gjgY0kX5Bm', null, null, null, null, false),
('cb601d30-1616-4e18-aadc-8ee2babd20fb', 'owen.doe@gmail.com', '$2a$11$wgVvDB3k9Qk7hO4QvDLHCuXWckAwU7vW5rbTkvzBty9erLYIg1w.y', null, null, null, null, false),
('f10490d7-29e1-4b3a-bd39-1c516cd7bcd3', 'lisa.brown@gmail.com', '$2a$11$kETI6AH7J5XMwtL05vOJGOos6kV9WxihWexCxRQP4QVUjx/JbYRdK', null, null, null, null, false),
('ac59d6f0-7cde-4c57-82ad-799a5b9533e2', 'mike.green@gmail.com', '$2a$11$rtKfVHE7hxDFHZnZVFbUHO1EwMVdDvT/55fe8XfbmD0DDdlxeL5Sm', null, null, null, null, false),
('e543bd73-9296-4f0a-b5d4-7e30b90e63f6', 'susan.king@gmail.com', '$2a$11$19/dpXy/a8F9hrJPgwKTyOEshE9vtLFTh.RYUZ3tkUs.CsusaCah.', null, null, null, null, false),
('1e18b8f3-a478-4cf7-9d7c-12d34bcda1d8', 'robert.johnson@gmail.com', '$2a$11$35Jj8APcNlp1Z8oXwzsT7uJyDw59i95y3C8HJr7YsqhwEr9aK/GNq', null, null, null, null, false),
('beefab84-4e56-4eb9-89db-e614faeea70d', 'nancy.adams@gmail.com', '$2a$11$.yqa4Wfk768LqbAGIYmeROxmRaqh37OO78GykHWAjUwyX9jrNHE5G', null, null, null, null, false),
('34fba073-6bcf-4426-8be5-61fdc0e0c3ef', 'frank.moore@gmail.com', '$2a$11$DBgBsHkwhrXyj2d.Q6vgMuYsOcBW.u2.Ld8QvtBDMN7gjgY0kX5Bm', null, null, null, null, false),
('9c01495c-9d12-44bb-95c1-2f7a111ca0ff', 'emma.thompson@gmail.com', '$2a$11$wgVvDB3k9Qk7hO4QvDLHCuXWckAwU7vW5rbTkvzBty9erLYIg1w.y', null, null, null, null, false),
('d2b0b282-1b74-4f0b-870f-cb978ee17f14', 'noah.davis@gmail.com', '$2a$11$kETI6AH7J5XMwtL05vOJGOos6kV9WxihWexCxRQP4QVUjx/JbYRdK', null, null, null, null, false),
('89eb64d9-54e7-4374-8532-345c65b77f26', 'olivia.garcia@gmail.com', '$2a$11$rtKfVHE7hxDFHZnZVFbUHO1EwMVdDvT/55fe8XfbmD0DDdlxeL5Sm', null, null, null, null, false),
('f6074c6f-88d2-4514-8d1d-eebdc65314b1', 'jason.baker@gmail.com', '$2a$11$19/dpXy/a8F9hrJPgwKTyOEshE9vtLFTh.RYUZ3tkUs.CsusaCah.', null, null, null, null, false);

INSERT INTO employees (id, name, surname, daysoff, role, accountid)
VALUES
('0d2a2715-84f8-4b66-a710-6fcf2a62cbbb', 'John', 'Doe', 20, 1, '387e3434-d303-4ebe-8109-f35b848b6f0f'),
('5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'Jane', 'Smith', 10, 0, '39491ef3-a1fa-4842-8fb1-8c8997622b52'),
('dfbbd969-43f8-4963-a884-3bb4190ea888', 'Emily', 'Jones', 15, 0, '5a70e705-4c43-42fe-88f4-216e2598e97f'),
('40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'Stan', 'Smith', 20, 0, '9eac01ad-37e5-4d49-82bf-ee445c691fad'),
('3e41e111-f297-4b85-aad5-7e97b23d8a29', 'Rachel', 'Moss', 18, 0, 'ae9071f9-1442-482d-8920-8e01b47d0c24'),
('c7186c89-5592-4dc7-b161-48a67d853d39', 'Alan', 'Sadge', 20, 0, 'b12f1dfd-9dbe-4f21-85b1-283c312301df'),
('814aef91-0da7-401c-ab7e-9cba8d05c531', 'Owen', 'Gray', 16, 0, 'cb601d30-1616-4e18-aadc-8ee2babd20fb'),
('d1c1d1e9-22a5-4d99-933a-8a6589af291f', 'Lisa', 'Brown', 25, 1, 'f10490d7-29e1-4b3a-bd39-1c516cd7bcd3'),
('94db6c43-f738-43e8-a9d4-7c4c7d1b3ed1', 'Mike', 'Green', 18, 0, 'ac59d6f0-7cde-4c57-82ad-799a5b9533e2'),
('8a70b3f2-4e3a-4c7d-aefe-383a118b9413', 'Susan', 'King', 22, 0, 'e543bd73-9296-4f0a-b5d4-7e30b90e63f6'),
('27f1b0ec-b41a-471a-9b62-f01f18f7f68a', 'Robert', 'Johnson', 10, 1, '1e18b8f3-a478-4cf7-9d7c-12d34bcda1d8'),
('d4f1b8e3-ecb2-4142-a4d8-81739a3cd123', 'Nancy', 'Adams', 12, 0, 'beefab84-4e56-4eb9-89db-e614faeea70d'),
('b27cfde9-5d42-4b0f-9f7f-2e2951a3bb29', 'Frank', 'Moore', 20, 0, '34fba073-6bcf-4426-8be5-61fdc0e0c3ef'),
('a67d95c2-587b-45f9-96d8-d7a2f9ebec90', 'Emma', 'Thompson', 18, 1, '9c01495c-9d12-44bb-95c1-2f7a111ca0ff'),
('6a8f57c3-4a0d-4c37-a66d-3af27e5f83a1', 'Noah', 'Davis', 16, 0, 'd2b0b282-1b74-4f0b-870f-cb978ee17f14'),
('c8d5d36a-e1f1-4cd3-a60b-97e6c6b19f44', 'Olivia', 'Garcia', 20, 0, '89eb64d9-54e7-4374-8532-345c65b77f26'),
('f52a1b22-d711-4c56-b90f-1f57e678ba5b', 'Jason', 'Baker', 15, 1, 'f6074c6f-88d2-4514-8d1d-eebdc65314b1');

INSERT INTO clients (id, name, country)
VALUES
('b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1', 'Acme Corp', 'USA'),
('92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94', 'Global Ventures', 'Canada'),
('7e4e4728-89b4-4f0c-8fc1-462cb90e3265', 'Oceanic Ltd', 'Australia'),
('bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7', 'Tech Solutions', 'UK'),
('ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f', 'Innovate Inc', 'Germany'),
('f3f9f4f5-41d2-4c8d-bd1f-72f6d8a6b4f5', 'Dynamic Labs', 'USA'),
('cd1a2e3f-68d4-4f0d-a5b7-5e1d4a6b9f7f', 'Alpha Solutions', 'Canada'),
('8d5e4f1b-9c1d-4f8f-b2f3-6f2d3a7b8d5e', 'Prime Solutions', 'UK'),
('5b1f9e3d-82c9-4d3f-b7a1-4f6d3b8e9a1b', 'Fusion Corp', 'Germany'),
('d3f9a5e2-74c2-4f7d-a6d1-7f5e8b2d9f3b', 'Blue Sky Innovations', 'Australia'),
('e6f7a1d2-94c3-4b8a-b2d9-1c7d4a9f5f7e', 'DigitalX', 'France'),
('8f2d1e3f-3c1b-4d5a-a7d1-5e8b2f3c7d9f', 'Eco Planet', 'Brazil'),
('3e7f9a5b-6d8c-4f0a-a5e7-2d5b8a1d6f9e', 'InnovaTech', 'Spain'),
('d2b1e9f4-84a1-4d3f-b9c1-4a7f3d5e8f6d', 'Spark Solutions', 'South Africa'),
('7b4f3a5d-8c2f-4d8a-a5e1-1f9d3c7b2d5f', 'Red Oak Partners', 'Italy'),
('d5b8e1f2-4d7b-4d8a-a5f7-6e1d8b3a5c7d', 'GreenTech', 'Japan'),
('4e8d5f7a-2d6b-4d8f-a1f3-8d1c5b7a4f9e', 'CodeCrafters', 'New Zealand'),
('1f9a3e6c-5d8b-4f0a-b9f1-2c7b5e1a6f7d', 'Bluewave Solutions', 'Netherlands'),
('7d8a5f1b-6d4f-4b9a-a3f7-8c1d5b2a6f9e', 'Vivid Tech', 'India'),
('9e3f1b7c-6a2b-4f8d-a5f3-1c8a7d9f5b6e', 'NextGen Innovators', 'Sweden');

INSERT INTO projects (id, title, description, teamleadid, clientid)
VALUES
('e9b1d3c1-2f45-4e6d-9f32-5c6b8f0b45d2', 'Website Redesign', 'Complete overhaul of the client website', '5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1'),

('2d9d1c4e-6b3d-4f41-a1f5-d5297d92f5d9', 'Mobile App Development', 'Developing a cross-platform mobile app', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', '92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94'),

('3b8e5f9b-2ae3-4d9a-9b47-1a54c3d7a992', 'Data Migration', 'Migrating legacy data to a new system', NULL, '7e4e4728-89b4-4f0c-8fc1-462cb90e3265'),

('a2d1e5b6-f7b8-41ae-b21d-36e34cfe5c8b', 'SEO Optimization', 'Improving search engine rankings', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7'),

('b4d2c6e3-f1b2-4a4d-a8d6-4b9f73b65f1c', 'Cloud Migration', 'Transitioning on-prem systems to the cloud', NULL, 'ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f'),
('f7b8eaf3-c9b9-49b7-8773-ec3e14b91cba', 'Backend Revamp', 'Revamping backend architecture', 'd1c1d1e9-22a5-4d99-933a-8a6589af291f', 'b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1'),
('73d1eb18-3d73-4a77-ae7f-6b67b7f82de1', 'UI Update', 'Updating UI for better UX', '94db6c43-f738-43e8-a9d4-7c4c7d1b3ed1', '92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94'),
('c7d91e2a-67f6-421e-a163-3f719ae8f3ea', 'Server Optimization', 'Optimizing server performance', NULL, '7e4e4728-89b4-4f0c-8fc1-462cb90e3265'),
('f3a1e6e3-764b-4f8f-9234-f3a6f8d2e5ab', 'Data Analytics', 'Enhancing data insights', '27f1b0ec-b41a-471a-9b62-f01f18f7f68a', 'bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7'),
('bb1a5c9e-921c-41f2-8f8f-58b6bdef95a2', 'Inventory Management', 'Developing inventory system', NULL, 'ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f'),
('c5d1fba9-91e8-4567-9c3a-0fbb5cb7e3f3', 'E-commerce Platform', 'Building new e-commerce system', 'd4f1b8e3-ecb2-4142-a4d8-81739a3cd123', 'b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1'),
('d8e7ab29-8c9d-4f3a-b7e3-1e86f7a6f8b5', 'ERP System', 'Implementing ERP solution', 'b27cfde9-5d42-4b0f-9f7f-2e2951a3bb29', '92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94'),
('fd2e1bf5-c5b9-4894-a0a7-4f7e5f3e7d9e', 'Cloud Backup', 'Setting up cloud backup solutions', NULL, '7e4e4728-89b4-4f0c-8fc1-462cb90e3265'),
('a3d6e2b9-8f8d-4d7f-b2a7-3d7e2b5a3f4d', 'Digital Transformation', 'Leading digital transformation', '6a8f57c3-4a0d-4c37-a66d-3af27e5f83a1', 'bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7'),
('4f2a9e6d-931f-4f0b-8f8f-9b7f6b2e9d1a', 'Cybersecurity Enhancement', 'Enhancing cybersecurity measures', 'c8d5d36a-e1f1-4cd3-a60b-97e6c6b19f44', 'ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f');


INSERT INTO employeeprojects(id, employeeid, projectid)
VALUES
('4be06a69-ae90-4f0d-8607-d299d8d1ca96', '5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'e9b1d3c1-2f45-4e6d-9f32-5c6b8f0b45d2'),

('09fad54d-ca96-4ef8-a3d5-360764631d26', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', '2d9d1c4e-6b3d-4f41-a1f5-d5297d92f5d9'),

('f329d1b2-a9e7-4a1d-896a-7cf5eb8b334b', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'a2d1e5b6-f7b8-41ae-b21d-36e34cfe5c8b'),
('6f8b7d4a-a6c9-4d8a-a3f2-9d7e4b1f3a6c', 'd1c1d1e9-22a5-4d99-933a-8a6589af291f', 'f7b8eaf3-c9b9-49b7-8773-ec3e14b91cba'),
('7f3d9e6b-8a1d-4f8f-b9c1-1d2e8b3f5c7e', '94db6c43-f738-43e8-a9d4-7c4c7d1b3ed1', '73d1eb18-3d73-4a77-ae7f-6b67b7f82de1'),
('9a4f8d2b-3c6f-4d7a-a1f3-2e8b9c1d5f7e', '8a70b3f2-4e3a-4c7d-aefe-383a118b9413', 'c7d91e2a-67f6-421e-a163-3f719ae8f3ea'),
('2c7f8b9d-6e3a-4f0b-b7f2-1d9a3e5f7d4b', '27f1b0ec-b41a-471a-9b62-f01f18f7f68a', 'f3a1e6e3-764b-4f8f-9234-f3a6f8d2e5ab'),
('1d3e6b5a-8f2b-4f7a-b5d1-7f4a9e6c2d7f', 'd4f1b8e3-ecb2-4142-a4d8-81739a3cd123', 'bb1a5c9e-921c-41f2-8f8f-58b6bdef95a2'),
('6b9a3e4d-1c8f-4d7b-a5e3-2f8a7d1b6e5f', 'b27cfde9-5d42-4b0f-9f7f-2e2951a3bb29', 'c5d1fba9-91e8-4567-9c3a-0fbb5cb7e3f3'),
('3d8e1a5c-2b7f-4f8d-a5f1-6e7b9a2c5d3f', 'a67d95c2-587b-45f9-96d8-d7a2f9ebec90', 'd8e7ab29-8c9d-4f3a-b7e3-1e86f7a6f8b5'),
('7b3e9d2f-8f6a-4c8d-b7a1-1f9e3d6b5a7f', '6a8f57c3-4a0d-4c37-a66d-3af27e5f83a1', 'fd2e1bf5-c5b9-4894-a0a7-4f7e5f3e7d9e'),
('5f7a8d2b-9e3f-4b8d-b5c1-3d9a1e6f7a4b', 'c8d5d36a-e1f1-4cd3-a60b-97e6c6b19f44', 'a3d6e2b9-8f8d-4d7f-b2a7-3d7e2b5a3f4d'),
('1f3d6e9b-7b5a-4c8f-a1d2-9b6a8e5d7f4c', '6a8f57c3-4a0d-4c37-a66d-3af27e5f83a1', '4f2a9e6d-931f-4f0b-8f8f-9b7f6b2e9d1a');
