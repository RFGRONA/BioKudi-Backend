-- Author: Gabriel Martinez
-- Date: 31/08/2024
-- Description: Script to preopulate the tables ACTIVITY, STATE, ROLE, DEPARTMENT, CITY, TAG and TYPE 

INSERT INTO ACTIVITY (name_activity) VALUES
('Mirador'),
('Caminata'),
('Senderismo'),
('Montañismo'),
('Ciclo paseos'),
('Camping'),
('Arborismo'),
('Tirolinea'),
('Cabalgata'),
('Alojamiento'),
('Fauna silvestre'),
('Avisturismo'),
('Escalada'),
('Rapel'),
('Gastronomía'),
('Puente colgante'),
('Canopy'),
('Cascadas'),
('Meditación'),
('Talleres'),
('Picnic'),
('Pesca deportiva'),
('Deportes náuticos'),
('Lagunas'),
('Piscinas'),
('Paseo en bote'),
('Deportes acuáticos'),
('Ciclo montañismo'),
('Aguas termales'),
('Spa'),
('Piscina natural'),
('Equitación'),
('Lodoterapia'),
('Museos'),
('Hidroterapia'),
('Recorridos de slackline'),
('Tirolesa'),
('Saltos de caida libre'),
('Bungee trampolín'),
('Parapente'),
('Baños turcos'),
('Jacuzzi'),
('Rafting'),
('Paintball'),
('Cuatrimoto'),
('Asados'),
('Glamping');

INSERT INTO STATE (name_state) VALUES
('Activo'),
('Inactivo'),
('Disponible'),
('Remodelación'),
('Cerrado'),
('Pendiente'),
('Revisado');

INSERT INTO ROLE (name_role) VALUES
('User'),
('Editor'),
('Admin');

INSERT INTO DEPARTMENT (name_department) VALUES
('Cundinamarca');

INSERT INTO CITY (name_city, department_id) VALUES
('Agua de Dios', 1),
('Albán', 1),
('Anapoima', 1),
('Anolaima', 1),
('Apulo', 1),
('Arbeláez', 1),
('Beltrán', 1),
('Bituima', 1),
('Bogotá', 1),
('Bojacá', 1),
('Cabrera', 1),
('Cachipay', 1),
('Cajicá', 1),
('Caparrapí', 1),
('Cáqueza', 1),
('Carmen de Carupa', 1),
('Chaguaní', 1),
('Chía', 1),
('Chipaque', 1),
('Choachí', 1),
('Chocontá', 1),
('Cogua', 1),
('Cota', 1),
('Cucunubá', 1),
('El Colegio', 1),
('El Peñón', 1),
('El Rosal', 1),
('Facatativá', 1),
('Fómeque', 1),
('Fosca', 1),
('Funza', 1),
('Fúquene', 1),
('Fusagasugá', 1),
('Gachalá', 1),
('Gachancipá', 1),
('Gachetá', 1),
('Gama', 1),
('Girardot', 1),
('Granada', 1),
('Guachetá', 1),
('Guaduas', 1),
('Guasca', 1),
('Guataquí', 1),
('Guatavita', 1),
('Guayabal de Síquima', 1),
('Guayabetal', 1),
('Gutiérrez', 1),
('Jerusalén', 1),
('Junín', 1),
('La Calera', 1),
('La Mesa', 1),
('La Palma', 1),
('La Peña', 1),
('La Vega', 1),
('Lenguazaque', 1),
('Machetá', 1),
('Madrid', 1),
('Manta', 1),
('Medina', 1),
('Mosquera', 1),
('Nariño', 1),
('Nemocón', 1),
('Nilo', 1),
('Nimaima', 1),
('Nocaima', 1),
('Pacho', 1),
('Paime', 1),
('Pandi', 1),
('Paratebueno', 1),
('Pasca', 1),
('Puerto Salgar', 1),
('Pulí', 1),
('Quebradanegra', 1),
('Quetame', 1),
('Quipile', 1),
('Ricaurte', 1),
('San Antonio del Tequendama', 1),
('San Bernardo', 1),
('San Cayetano', 1),
('San Francisco', 1),
('San Juan de Río Seco', 1),
('Sasaima', 1),
('Sesquilé', 1),
('Sibaté', 1),
('Silvania', 1),
('Simijaca', 1),
('Soacha', 1),
('Sopó', 1),
('Subachoque', 1),
('Suesca', 1),
('Supatá', 1),
('Susa', 1),
('Sutatausa', 1),
('Tabio', 1),
('Tausa', 1),
('Tena', 1),
('Tenjo', 1),
('Tibacuy', 1),
('Tibirita', 1),
('Tocaima', 1),
('Tocancipá', 1),
('Topaipí', 1),
('Ubalá', 1),
('Ubaque', 1),
('Ubaté', 1),
('Une', 1),
('Útica', 1),
('Venecia', 1),
('Vergara', 1),
('Vianí', 1),
('Villagómez', 1),
('Villapinzón', 1),
('Villeta', 1),
('Viotá', 1),
('Yacopí', 1),
('Zipacón', 1),
('Zipaquirá', 1);

INSERT INTO TYPE (name_type, table_relation) VALUES
('PQRS', 'TICKET'),
('Felicitación', 'TICKET'),
('Reclamo', 'TICKET'),
('Queja', 'TICKET'),
('Consulta', 'TICKET'),
('Sugerencia', 'TICKET'),
('Petición', 'TICKET'),
('Portada de lugar', 'PICTURE'),
('Foto de perfil', 'PICTURE');

INSERT INTO TAG (name_tag) VALUES
('Ecoturismo'),
('Aventura'),
('Naturaleza'),
('Sostenibilidad'),
('Cultura'),
('Conservación'),
('Fauna'),
('Flora'),
('Montaña'),
('Ríos'),
('Senderismo'),
('Paisajes'),
('Educación Ambiental'),
('Comunidades'),
('Eventos');