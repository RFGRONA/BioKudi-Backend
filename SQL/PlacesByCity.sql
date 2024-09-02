-- Author: Gabriel Martinez
-- Date: 01/09/2024
-- Description: Script to create the Stored Procedure get_places_by_department_city

CREATE OR REPLACE FUNCTION get_places_by_department_city(department_id INTEGER, city_id INTEGER)
RETURNS TABLE(id_place INTEGER, name_place VARCHAR, city_name VARCHAR) AS $$
BEGIN
    RETURN QUERY
    SELECT p.id_place, p.name_place, c.name_city
    FROM PLACE p
    JOIN CITY c ON p.city_id = c.id_city
    WHERE c.department_id = department_id AND c.id_city = city_id;
END;
$$ LANGUAGE plpgsql;
