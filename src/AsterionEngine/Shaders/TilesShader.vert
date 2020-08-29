/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
Asterion Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
Asterion Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

#version 330 core

uniform mat4 projection;

layout(location = 0) in vec2 vertPosition;
layout(location = 1) in vec3 vertColor;
layout(location = 2) in vec2 vertUV;
layout(location = 3) in float vertTileMap;

out vec3 fragColor;
out vec2 fragUV;
out float fragTileMap;

void main()
{
  gl_Position = vec4(vertPosition, 0.0, 1.0) * projection;
  fragColor = vertColor;
  fragUV = vertUV;
  fragTileMap = vertTileMap;
}
