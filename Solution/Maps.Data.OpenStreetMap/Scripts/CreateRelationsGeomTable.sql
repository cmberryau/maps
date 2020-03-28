-- create the relations_geom table
CREATE TABLE public.relations_geom
(
    relation_id bigint NOT NULL,
    PRIMARY KEY (relation_id)
)
WITH (
    OIDS = FALSE
);

ALTER TABLE public.relations_geom
    OWNER to postgres;

-- add the geometry columns
SELECT AddGeometryColumn('relations_geom', 'node_member_bbox', 4326, 'GEOMETRY', 2);
SELECT AddGeometryColumn('relations_geom', 'way_member_bbox', 4326, 'GEOMETRY', 2);
SELECT AddGeometryColumn('relations_geom', 'bbox', 4326, 'GEOMETRY', 2);