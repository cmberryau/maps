import psycopg2
import argparse
import getpass

# define arguments
parser = argparse.ArgumentParser(description='Creates bounding boxes for OSM relations')
parser.add_argument('hostname', help='PostgreSQL hostname')
parser.add_argument('db', help='PostgreSQL database name')
parser.add_argument('username', help='PostgreSQL username')

# parse arguments
args = parser.parse_args()

# connect to the db
try:
    conn = psycopg2.connect(dbname=args.db, user=args.username, host=args.hostname, password=getpass.getpass(prompt=args.username + '@' + args.hostname + ' password: '))
except:
    print("Could not connect to database")
    exit(0)

cur = conn.cursor()

try:
    cur.execute("SELECT relations.id FROM relations")
except:
    print("Unable to execute PostgreSQL command")

rows = cur.fetchall()

for row in rows:
    try:
        cur.execute("""INSERT INTO relations_geom (relation_id, node_member_bbox, way_member_bbox)
        VALUES ( %(id)s,
            (SELECT ST_SetSRID(ST_Extent(nodes.geom), 4326)::geometry AS node_member_bbox
             FROM nodes
             WHERE nodes.id IN
                     (SELECT member_id
                      FROM relation_members
                      WHERE relation_members.relation_id = %(id)s
                          AND (relation_members.member_type = 'N'))),
            (SELECT ST_SetSRID(ST_Extent(ways.bbox), 4326)::geometry AS way_member_bbox
             FROM ways
             WHERE ways.id IN
                     (SELECT member_id
                      FROM relation_members
                      WHERE relation_members.relation_id = %(id)s
                          AND (relation_members.member_type = 'W'))))""", {'id' : row[0],})
    except Exception as e:
        print(e)
        exit(0)

conn.commit()