-- merge both way and node member bboxes together when both are not null
UPDATE 
    relations_geom
SET 
    bbox = ST_Envelope(ST_Union(relations_geom.way_member_bbox, relations_geom.node_member_bbox))
WHERE 
    relations_geom.way_member_bbox IS NOT NULL
AND 
    relations_geom.node_member_bbox IS NOT NULL;

-- only use the way member bbox when the node member bbox is null
UPDATE 
    relations_geom
SET 
    bbox = relations_geom.way_member_bbox
WHERE 
    relations_geom.node_member_bbox IS NULL;

-- only use the node member bbox when the way member bbox is null
UPDATE 
    relations_geom
SET 
    bbox = relations_geom.node_member_bbox
WHERE 
    relations_geom.way_member_bbox IS NULL;

-- update the relations geometry column
UPDATE 
    relations
SET 
    bbox = relations_geom.bbox
FROM
    relations_geom
WHERE
    relations.id = relations_geom.relation_id;

-- drop the relations_geom table
DROP TABLE public.relations_geom;

-- Cluster table by geographical location.
CLUSTER relations USING idx_relations_bbox;