Conventions all abided by in small-straight section. Probably easiest to copy that and keep the following in mind.

# TODO
* Layer assignments
* Unity materials

# Structure
* Road must NOT be made of a plane. Ruins some of the raycasting in Unity.

# Dimensions (at ends)
* Road 2 meters wide at ends
* Flat grass half a meters wide on each side, -0.02 meters below ground at ends.
![](spec-images/flat-grass-spec.png)
* Slanted ground to be 30° from horizontal. Default 2 meters wide. (Position show is center)
![](spec-images/slanted-position.png)
* Wall to be vertical and 1m high (Position shown)
![](spec-images/wall-spec.png)

# Start and End Points
Start and end points of the model are declared in Unity using gameObjects. These hold the position and rotation at the ends.

Blender
* both ends must align to the grid. (Note: to make end of mesh with array collider to end exactly on end of curve, enable "Stretch" and "Bounds Clamp" on the curves "Data Object Properties")
![Model origin](spec-images/end-point-grid-alignment.png)
* both ends must have 5 degree slope downwards
![Endpoint slope](spec-images/5-degrees.png)
* both must have 0 tilt
![zero tilt at ends](spec-images/0tilt.png)


Unity
* start and end point must be align s.t. local +z axis points in forward direction
![](spec-images/forwardz.png)
* start must point down +z axis (Unity coordinates)
![Start must point down +z axis](spec-images/start-axis.png)

# Environment
TODO
NO TREES IN MODEL. TO BE ADDED IN UNITY

# UV Mapping
* Need to UV map the road s.t the texture is on the top, and the other faces are solid grey.
![](spec-images/uv.png)

# Colliders
TODO

# Seamless
* at the start of every track section, extend the curve out and downwards
![](spec-images/seamless.png)
* Use a lot of trees to hide imperfections. Trees may go off the start of the track