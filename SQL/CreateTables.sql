-- Author: Gabriel Martinez
-- Date: 31/08/2024
-- Description: Script to create the database schema in PostgreSQL

CREATE TABLE IF NOT EXISTS AUDIT
(
    id_audit SERIAL PRIMARY KEY,
    view_action VARCHAR(50),
    action VARCHAR(50),
    date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    modified_by VARCHAR(124),
    old_value TEXT,
    post_value TEXT
);

CREATE TABLE IF NOT EXISTS DEPARTMENT
(
    id_department SERIAL PRIMARY KEY,
    name_department VARCHAR(40)
);

CREATE TABLE IF NOT EXISTS CITY
(
    id_city SERIAL PRIMARY KEY,
    name_city VARCHAR(60),
    department_id INTEGER REFERENCES DEPARTMENT(id_department)
);

CREATE TABLE IF NOT EXISTS ACTIVITY
(
    id_activity SERIAL PRIMARY KEY,
    name_activity VARCHAR(128) NOT NULL,
    url_icon VARCHAR(15)
);

CREATE TABLE IF NOT EXISTS TAG
(
    id_tag SERIAL PRIMARY KEY,
    name_tag VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS STATE
(
    id_state SERIAL PRIMARY KEY,
    name_state VARCHAR(30) NOT NULL,
    table_relation VARCHAR(30)
);

CREATE TABLE IF NOT EXISTS TYPE
(
    id_type SERIAL PRIMARY KEY,
    name_type VARCHAR(30),
    table_relation VARCHAR(30)
);

CREATE TABLE IF NOT EXISTS ROLE
(
    id_role SERIAL PRIMARY KEY,
    name_role VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS PLACE
(
    id_place SERIAL PRIMARY KEY,
    name_place VARCHAR(80) NOT NULL,
    latitude NUMERIC(9, 6) NOT NULL,  
    longitude NUMERIC(9, 6) NOT NULL, 
    address VARCHAR(128) NOT NULL,
    description VARCHAR(560) NOT NULL,
    link VARCHAR(255) NOT NULL,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    city_id INTEGER REFERENCES CITY(id_city),  
    state_id INTEGER REFERENCES STATE(id_state),  
    CONSTRAINT chk_latitude CHECK (latitude >= -90 AND latitude <= 90),
    CONSTRAINT chk_longitude CHECK (longitude >= -180 AND longitude <= 180)
);

CREATE TABLE IF NOT EXISTS PERSON
(
    id_user SERIAL PRIMARY KEY,
    name_user VARCHAR(65) NOT NULL,
    telephone VARCHAR(15),
    email VARCHAR(75) NOT NULL UNIQUE,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    password VARCHAR(128) NOT NULL,
    hash VARCHAR(255) NOT NULL,
    state_id INTEGER REFERENCES STATE(id_state),
    role_id INTEGER NOT NULL REFERENCES ROLE(id_role),
    account_deleted BOOLEAN DEFAULT FALSE,
    email_notification BOOLEAN DEFAULT TRUE,
    email_post BOOLEAN DEFAULT TRUE,
    email_list BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS REVIEW
(
    id_review SERIAL PRIMARY KEY,
    rate NUMERIC(2, 1) NOT NULL CHECK (rate >= 1 AND rate <= 5),
    comment VARCHAR(255),
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    person_id INTEGER NOT NULL REFERENCES PERSON(id_user),
    place_id INTEGER NOT NULL REFERENCES PLACE(id_place)
);

CREATE TABLE IF NOT EXISTS TICKET
(
    id_ticket SERIAL PRIMARY KEY,
    affair TEXT NOT NULL, 
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_answered TIMESTAMP,
    answered_by VARCHAR(60),
    scalp_admin BOOLEAN DEFAULT FALSE,
    person_id INTEGER NOT NULL REFERENCES PERSON(id_user),
    state_id INTEGER REFERENCES STATE(id_state),
    type_id INTEGER REFERENCES TYPE(id_type)
);

CREATE TABLE IF NOT EXISTS POST
(
    id_post SERIAL PRIMARY KEY,
    title VARCHAR(60) NOT NULL,
    created_by VARCHAR(60) NOT NULL,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    text TEXT NOT NULL, 
    place_id INTEGER REFERENCES PLACE(id_place)
);

CREATE TABLE IF NOT EXISTS LIST
(
    id_list SERIAL PRIMARY KEY,
    person_id INTEGER REFERENCES PERSON(id_user),
    name_list VARCHAR(30)
);

CREATE TABLE IF NOT EXISTS PICTURE
(
    id_picture SERIAL PRIMARY KEY,
    name VARCHAR(128) NOT NULL,
    link VARCHAR(255) NOT NULL,
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    type_id INTEGER REFERENCES TYPE(id_type),
    place_id INTEGER REFERENCES PLACE(id_place),
    person_id INTEGER REFERENCES PERSON(id_user),
    ticket_id INTEGER REFERENCES TICKET(id_ticket),
    review_id INTEGER REFERENCES REVIEW(id_review)
);

CREATE TABLE IF NOT EXISTS ACTIVITY_PLACE
(
    place_id INTEGER NOT NULL REFERENCES PLACE(id_place),
    activity_id INTEGER NOT NULL REFERENCES ACTIVITY(id_activity),
    PRIMARY KEY(place_id, activity_id)
);

CREATE TABLE IF NOT EXISTS POST_TAG
(
    post_id INTEGER NOT NULL REFERENCES post(id_post),
    tag_id INTEGER NOT NULL REFERENCES TAG(id_tag),
    PRIMARY KEY(post_id, tag_id)
);

CREATE TABLE IF NOT EXISTS PLACE_LIST
(
    place_id INTEGER NOT NULL REFERENCES PLACE(id_place),
    list_id INTEGER NOT NULL REFERENCES LIST(id_list),
    PRIMARY KEY(place_id, list_id)
);