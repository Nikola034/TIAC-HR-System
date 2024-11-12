INSERT INTO employees (id, name, surname, daysoff, role)
VALUES
('0d2a2715-84f8-4b66-a710-6fcf2a62cbbb', 'John', 'Deer', 20, 1),
('5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'Jane', 'Smith', 10, 0),
('dfbbd969-43f8-4963-a884-3bb4190ea888', 'Emily', 'Jones', 15, 0),
('40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'Stan', 'Smith', 20, 0),
('3e41e111-f297-4b85-aad5-7e97b23d8a29', 'Rachel', 'Moss', 18, 0),
('c7186c89-5592-4dc7-b161-48a67d853d39', 'Alan', 'Sadge', 20, 0),
('814aef91-0da7-401c-ab7e-9cba8d05c531', 'Oewn', 'Gray', 16, 0);


INSERT INTO clients (id, name, country)
VALUES
('b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1', 'Acme Corp', 'USA'),
('92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94', 'Global Ventures', 'Canada'),
('7e4e4728-89b4-4f0c-8fc1-462cb90e3265', 'Oceanic Ltd', 'Australia'),
('bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7', 'Tech Solutions', 'UK'),
('ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f', 'Innovate Inc', 'Germany');


INSERT INTO projects (id, title, description, teamleadid, clientid)
VALUES
('e9b1d3c1-2f45-4e6d-9f32-5c6b8f0b45d2', 'Website Redesign', 'Complete overhaul of the client website', '5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'b3fcb9d0-9adf-4eeb-81b2-8396af31f4d1'),

('2d9d1c4e-6b3d-4f41-a1f5-d5297d92f5d9', 'Mobile App Development', 'Developing a cross-platform mobile app', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', '92a1b2f3-6f3e-4a10-b8f5-0a71d4e3cb94'),

('3b8e5f9b-2ae3-4d9a-9b47-1a54c3d7a992', 'Data Migration', 'Migrating legacy data to a new system', NULL, '7e4e4728-89b4-4f0c-8fc1-462cb90e3265'),

('a2d1e5b6-f7b8-41ae-b21d-36e34cfe5c8b', 'SEO Optimization', 'Improving search engine rankings', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'bcd5d4e5-3b49-4d5e-8286-4bbf53ec67d7'),

('b4d2c6e3-f1b2-4a4d-a8d6-4b9f73b65f1c', 'Cloud Migration', 'Transitioning on-prem systems to the cloud', NULL, 'ae32f195-7c1d-4a67-8f3b-bd5e2f264f9f');


INSERT INTO employeeprojects(id, employeeid, projectid)
VALUES
('4be06a69-ae90-4f0d-8607-d299d8d1ca96', '5fb4e623-5ba2-4cd6-9232-00d56b3cd354', 'e9b1d3c1-2f45-4e6d-9f32-5c6b8f0b45d2'),

('09fad54d-ca96-4ef8-a3d5-360764631d26', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', '2d9d1c4e-6b3d-4f41-a1f5-d5297d92f5d9'),

('f329d1b2-a9e7-4a1d-896a-7cf5eb8b334b', '40cd5083-14c1-4aa9-a0d9-7bf96eb64df6', 'a2d1e5b6-f7b8-41ae-b21d-36e34cfe5c8b');
