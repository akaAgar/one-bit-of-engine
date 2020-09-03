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

uniform int animationFrame;
uniform float time;

uniform float tileUVWidth = 16.0 / 512.0;
uniform float tileUVHeight = 16.0 / 64.0;

uniform sampler2D texture0;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;

in vec3 fragColor;
in vec2 fragUV;
in float fragTileMap;
in float fragVFX;

out vec4 color;


// Returns the UV of the pixel within the tile and not within the entire tilemap (with UV 0,0 in the top-left corner of the tile and UV 1,1 in its bottom-right corner)
void InternalTileUV(in vec2 absoluteUV, out vec2 tileUV)
{
  tileUV = vec2(
    (absoluteUV.x - floor(absoluteUV.x / tileUVWidth) * tileUVWidth) / tileUVWidth,
    (absoluteUV.y - floor(absoluteUV.y / tileUVHeight) * tileUVHeight) / tileUVHeight
  );
}

void main()
{
  vec2 texUV = fragUV;

  // Values must match those in the TileVFX enumeration
  switch (int(fragVFX))
  {
  /*
    case 7:
	  InternalTileUV(fragUV, texUV);
	  break;
	  */
  }

  switch (int(fragTileMap))
  {
   default: color = texture(texture0, texUV); break;
   case 1: color = texture(texture1, texUV); break;
   case 2: color = texture(texture2, texUV); break;
   case 3: color = texture(texture3, texUV); break;
  }
 
  float brightness = 0;

  switch (animationFrame)
  {
    default: brightness = color.r; break;
    case 1: brightness = color.g; break;
    case 2: brightness = color.b; break;
  }
  
  // Values must match those in the TileVFX enumeration
  switch (int(fragVFX))
  {
    case 1: brightness *= abs(cos(time)); break; // GlowSlow
    case 2: brightness *= abs(cos(time * 2)); break; // GlowAverage
    case 3: brightness *= abs(cos(time * 4)); break; // GlowFast

	case 4: if (cos(time * 4) < 0) brightness = 0; break; // BlinkSlow
	case 5: if (cos(time * 8) < 0) brightness = 0; break; // BlinkAverage
	case 6: if (cos(time * 16) < 0) brightness = 0; break; // BlinkFast
	
	case 7: brightness = 1 - brightness; break; // Negative

	/*
	case 7:
	  // brightness *= abs(cos(time * (texUV.x / tileUVWidth)));
	  // brightness *= abs(cos(time * mod(texUV.x, tileUVWidth)));
	  
	  float xOffset = texUV.x - (tileUVWidth * floor(texUV.x / tileUVWidth));
	  xOffset *= tileUVWidth;
	  brightness *= abs(cos(xOffset));
	  break;
	  */
  }

  color = vec4(brightness, brightness, brightness, 1) * vec4(fragColor, 1);
  color.a = color.r;
}
