# Maps
Maps is designed to be a middleware between geospatial data (map features, routing etc) providers and Unity3D with the objective to provide a simple (not quite there yet) way to use map data within games and other applications made with Unity3D. The integration into Unity3D is actually quite light, so it could be potentially integrated with other front-ends.

# What can it do?
You can render 3D and 2D maps within Unity3D from compiled map data offline (no internet connection required).

# How can I check it out?
Pull down the repo and open the MapsExample Unity3D project. Make sure that works on your end - if not let me know!

# How can I use this in my own project?
Hmm, good question! It is not very new user friendly right yet - that's my next priority.

Right now, you would need to figure out how to compile your own map db. To do that, you need to first have the OpenStreetMap data on a local psql db. Then you should look at Maps.Data.Compiler.CLI to compile your db.

Once you've got it compiled, you'd use your db instead of the example db and set your start latlon pair to an point within your desired area.

# Where does the map data come from?
Curently the map data is from OpenStreetMap, which is then filtered and compiled into a SQLite database.

# Why did I make this and not use something else?
It was a really fun challenge from many angles and there was a client of mine who needed it at the time.

# What are the list of bigger features?
Generally:
- 3D and 2D map rendering
- Tiled binary map DB compilation
- Segment and area simplfication
- Segment and area tesselation
- Routing via OsmSharp

In the context of Unity3D:
- Tested on iOS, Android and PC
- Use any material/shader you want for objects
- Uses default Unity UI

# Known major issues?
Not very new user friendly
When traversing zoom levels, it freezes a little
Map rotation can sometimes get in a weird state

# Example images:
![Example image 0](https://raw.githubusercontent.com/cmberryau/maps/master/Images/example0.png)
![Example image 1](https://raw.githubusercontent.com/cmberryau/maps/master/Images/example1.png)
![Example image 2](https://raw.githubusercontent.com/cmberryau/maps/master/Images/example2.png)
![Example image 3](https://raw.githubusercontent.com/cmberryau/maps/master/Images/example3.png)
