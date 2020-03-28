-- Add a postgis GEOMETRY column to the relation table for the purpose of indexing the location of the relation.
-- This will contain a bounding box surrounding the extremities of the relation.
SELECT AddGeometryColumn('relations', 'bbox', 4326, 'GEOMETRY', 2);

-- Add an index to the bbox column.
CREATE INDEX idx_relations_bbox ON relations USING gist (bbox);