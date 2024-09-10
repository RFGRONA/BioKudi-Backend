-- Author: Gabriel Martinez
-- Date: 03/09/2024
-- Description: Script to create the Stored Procedures 

CREATE OR REPLACE PROCEDURE get_places_by_department_city_proc(department_id INTEGER, city_id INTEGER, OUT result_set refcursor)
LANGUAGE plpgsql
AS $$
-- Author: Gabriel Martinez
-- Date: 01/09/2024
-- Description: Script to create the Stored Procedure get_places_by_department_city
BEGIN
    OPEN result_set FOR
    SELECT p.id_place, p.name_place, c.name_city
    FROM PLACE p
    JOIN CAT_CITY c ON p.city_id = c.id_city
    WHERE c.department_id = department_id AND c.id_city = city_id;
END;
$$;

CREATE OR REPLACE PROCEDURE get_average_rating_proc(p_place_id INTEGER, OUT avg_rating NUMERIC(2,1))
LANGUAGE plpgsql
AS $$
-- Author: Gabriel Martinez
-- Date: 03/09/2024
-- Description: Script to create the Stored Procedure get_average_rating
BEGIN
    SELECT AVG(rate) INTO avg_rating
    FROM REVIEW
    WHERE place_id = p_place_id;
END;
$$;

CREATE OR REPLACE PROCEDURE count_activities_for_place_proc(p_place_id INTEGER, OUT activity_count INTEGER)
LANGUAGE plpgsql
AS $$
-- Author: Gabriel Martinez
-- Date: 03/09/2024
-- Description: Script to create the Stored Procedure count_activities_for_place
BEGIN
    SELECT COUNT(*) INTO activity_count
    FROM ACTIVITY_PLACE
    WHERE place_id = p_place_id;
END;
$$;

CREATE OR REPLACE PROCEDURE get_average_ticket_response_time_proc(OUT avg_response_time INTERVAL)
LANGUAGE plpgsql
AS $$
-- Author: Gabriel Martinez
-- Date: 03/09/2024
-- Description: Script to create the Stored Procedure get_average_ticket_response_time
BEGIN
    SELECT AVG(date_answered - date_created) INTO avg_response_time
    FROM TICKET
    WHERE date_answered IS NOT NULL;
END;
$$;

CREATE OR REPLACE PROCEDURE update_ticket_state_proc(p_ticket_id INTEGER, p_new_state_id INTEGER)
LANGUAGE plpgsql
AS $$
-- Author: Gabriel Martinez
-- Date: 03/09/2024
-- Description: Script to create the Stored Procedure update_ticket_state
BEGIN
    UPDATE TICKET
    SET state_id = p_new_state_id, date_answered = CURRENT_TIMESTAMP
    WHERE id_ticket = p_ticket_id;

    RAISE NOTICE 'Ticket ID % updated to state ID %', p_ticket_id, p_new_state_id;
END;
$$;