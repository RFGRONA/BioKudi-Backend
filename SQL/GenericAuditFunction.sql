-- Author: Gabriel Martinez
-- Date: 31/08/2024
-- Description: Script to create the generic function to audit changes in PostgreSQL

CREATE OR REPLACE FUNCTION audit_changes() RETURNS TRIGGER AS $$
DECLARE
    v_old_data JSON;
    v_new_data JSON;
BEGIN
    IF (TG_OP = 'UPDATE') THEN
        v_old_data = row_to_json(OLD);
        v_new_data = row_to_json(NEW);
        INSERT INTO AUDIT (view_action, action, date, old_value, post_value)
        VALUES (TG_TABLE_NAME::VARCHAR, TG_OP, CURRENT_TIMESTAMP, v_old_data::VARCHAR, v_new_data::VARCHAR);
    ELSIF (TG_OP = 'DELETE') THEN
        v_old_data = row_to_json(OLD);
        INSERT INTO AUDIT (view_action, action, date, old_value)
        VALUES (TG_TABLE_NAME::VARCHAR, TG_OP, CURRENT_TIMESTAMP, v_old_data::VARCHAR);
    ELSIF (TG_OP = 'INSERT') THEN
        v_new_data = row_to_json(NEW);
        INSERT INTO AUDIT (view_action, action, date, post_value)
        VALUES (TG_TABLE_NAME::VARCHAR, TG_OP, CURRENT_TIMESTAMP, v_new_data::VARCHAR);
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;