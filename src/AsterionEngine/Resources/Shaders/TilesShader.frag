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

// Values must match those in the TileVFX enumeration
#define VFX_NONE 0
#define VFX_GLOW_SLOW 1
#define VFX_GLOW_MEDIUM 2
#define VFX_GLOW_FAST 3
#define VFX_BLINK_SLOW 4
#define VFX_BLINK_MEDIUM 5
#define VFX_BLINK_FAST 6
#define VFX_NEGATIVE 7
#define VFX_NEGATIVE_BLINK_SLOW 8
#define VFX_NEGATIVE_BLINK_MEDIUM 9
#define VFX_NEGATIVE_BLINK_FAST 10
#define VFX_NEGATIVE_GLOW_SLOW 11
#define VFX_NEGATIVE_GLOW_MEDIUM 12
#define VFX_NEGATIVE_GLOW_FAST 13
#define VFX_SLANTED_RIGHT 14
#define VFX_SLANTED_LEFT 15
#define VFX_OSCILLATE_TOP_SLOW 16
#define VFX_OSCILLATE_TOP_MEDIUM 17
#define VFX_OSCILLATE_TOP_FAST 18
#define VFX_OSCILLATE_BOTTOM_SLOW 19
#define VFX_OSCILLATE_BOTTOM_MEDIUM 20
#define VFX_OSCILLATE_BOTTOM_FAST 21
#define WAVE_HORIZONTAL_SLOW 22
#define WAVE_HORIZONTAL_MEDIUM 23
#define WAVE_HORIZONTAL_FAST 24
#define WAVE_VERTICAL_SLOW 25
#define WAVE_VERTICAL_MEDIUM 26
#define WAVE_VERTICAL_FAST 27

#version 330 core

uniform int animationFrame;
uniform float time;

uniform vec2 tileUVSize = vec2(16.0 / 512.0, 16.0 / 64.0);

uniform sampler2D texture0;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;

in vec3 fragColor;
in vec2 fragUV;
in float fragTileMap;
in float fragVFX;

out vec4 color;

// Returns some pseudo-noise from a set of UV coordinates. Code found on https://stackoverflow.com/questions/4200224/random-noise-functions-for-glsl
float noise(vec2 uv)
{
  return fract(sin(dot(uv.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

// Returns the UV of the pixel within the tile and not within the entire tilemap (with UV 0,0 in the top-left corner of the tile and UV 1,1 in its bottom-right corner)
/*
void InternalTileUV(in vec2 absoluteUV, out vec2 tileUV)
{
  tileUV = vec2(
    (absoluteUV.x - floor(absoluteUV.x / tileUVSize.x) * tileUVSize.x) / tileUVSize.x,
    (absoluteUV.y - floor(absoluteUV.y / tileUVSize.y) * tileUVSize.y) / tileUVSize.y
  );
}
*/

vec2 TileCornerUV(vec2 uv)
{
  return vec2(floor(uv.x / tileUVSize.x) * tileUVSize.x, floor(uv.y / tileUVSize.y) * tileUVSize.y);
}

vec2 InternalTileUVNormalized(vec2 absoluteUV)
{
  return vec2(
    (absoluteUV.x - floor(absoluteUV.x / tileUVSize.x) * tileUVSize.x) / tileUVSize.x,
    (absoluteUV.y - floor(absoluteUV.y / tileUVSize.y) * tileUVSize.y) / tileUVSize.y);
}

vec2 InternalTileUV(vec2 uv)
{
  return vec2(uv.x - floor(uv.x / tileUVSize.x) * tileUVSize.x, uv.y - floor(uv.y / tileUVSize.y) * tileUVSize.y);
}

void main()
{
  vec2 texUV = fragUV;
  vec2 tileUV, offsetUV;

  int fragVFXi = int(fragVFX);

  // This switch handles all VFX UV modifications
  switch (fragVFXi)
  {
    case VFX_SLANTED_RIGHT:
	  tileUV = TileCornerUV(texUV);
	  offsetUV = InternalTileUVNormalized(texUV);
	  offsetUV.x = clamp(offsetUV.x + offsetUV.y * .2, 0, 1);
	  texUV = tileUV + offsetUV * tileUVSize;
      break;

    case VFX_SLANTED_LEFT:
	  tileUV = TileCornerUV(texUV);
	  offsetUV = InternalTileUVNormalized(texUV);
	  offsetUV.x = clamp(offsetUV.x + (1 - offsetUV.y) * .2, 0, 1);
	  texUV = tileUV + offsetUV * tileUVSize;
      break;

    case VFX_OSCILLATE_TOP_SLOW:
    case VFX_OSCILLATE_TOP_MEDIUM:
    case VFX_OSCILLATE_TOP_FAST:
    case VFX_OSCILLATE_BOTTOM_SLOW:
    case VFX_OSCILLATE_BOTTOM_MEDIUM:
    case VFX_OSCILLATE_BOTTOM_FAST:
	  float oscillateSpeed = 1;
	  if ((fragVFXi == VFX_OSCILLATE_TOP_MEDIUM) || (fragVFXi == VFX_OSCILLATE_BOTTOM_MEDIUM)) oscillateSpeed = 2;
	  else if ((fragVFXi == VFX_OSCILLATE_TOP_FAST) || (fragVFXi == VFX_OSCILLATE_BOTTOM_FAST)) oscillateSpeed = 4;

      tileUV = TileCornerUV(texUV);
	  offsetUV = InternalTileUVNormalized(texUV);

	  float oscillateOffsetY = (1 - offsetUV.y);
	  if (fragVFXi >= VFX_OSCILLATE_BOTTOM_SLOW) oscillateOffsetY = offsetUV.y;

	  offsetUV.x = clamp(offsetUV.x + oscillateOffsetY * cos(time * oscillateSpeed) * .2, 0, 1);
	  texUV = tileUV + offsetUV * tileUVSize;
      break;

    case WAVE_HORIZONTAL_SLOW:
    case WAVE_HORIZONTAL_MEDIUM:
    case WAVE_HORIZONTAL_FAST:
	  float waveSpeed = 1;
	  if (fragVFXi == WAVE_HORIZONTAL_MEDIUM) waveSpeed = 2;
	  else if (fragVFXi == WAVE_HORIZONTAL_FAST) waveSpeed = 4;

	  tileUV = TileCornerUV(texUV);
	  offsetUV = InternalTileUVNormalized(texUV);
	  offsetUV.x = clamp(offsetUV.x + cos(time * waveSpeed) * sin(offsetUV.y * 16) * .15, 0, 1);
	  texUV = tileUV + offsetUV * tileUVSize;
      break;

    case WAVE_VERTICAL_SLOW:
    case WAVE_VERTICAL_MEDIUM:
    case WAVE_VERTICAL_FAST:
	  float wave2Speed = 1;
	  if (fragVFXi == WAVE_VERTICAL_MEDIUM) wave2Speed = 2;
	  else if (fragVFXi == WAVE_VERTICAL_FAST) wave2Speed = 4;

	  tileUV = TileCornerUV(texUV);
	  offsetUV = InternalTileUVNormalized(texUV);
	  offsetUV.y = clamp(offsetUV.y + cos(time * wave2Speed) * sin(offsetUV.x * 16) * .15, 0, 1);
	  texUV = tileUV + offsetUV * tileUVSize;
      break;
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
  
  switch (int(fragVFX))
  {
    case VFX_GLOW_SLOW: brightness *= abs(cos(time)); break;
    case VFX_GLOW_MEDIUM: brightness *= abs(cos(time * 2)); break;
    case VFX_GLOW_FAST: brightness *= abs(cos(time * 4)); break;

	case VFX_BLINK_SLOW: if (cos(time * 4) < 0) brightness = 0; break;
	case VFX_BLINK_MEDIUM: if (cos(time * 8) < 0) brightness = 0; break;
	case VFX_BLINK_FAST: if (cos(time * 16) < 0) brightness = 0; break;

	case VFX_NEGATIVE: brightness = 1 - brightness; break; // Negative

	case VFX_NEGATIVE_BLINK_SLOW: if (cos(time * 4) < 0) brightness = 1 - brightness; break;
	case VFX_NEGATIVE_BLINK_MEDIUM: if (cos(time * 8) < 0) brightness = 1 - brightness; break;
	case VFX_NEGATIVE_BLINK_FAST: if (cos(time * 16) < 0) brightness = 1 - brightness; break;

    case VFX_NEGATIVE_GLOW_SLOW: brightness = mix(brightness, 1 - brightness, abs(cos(time))); break;
    case VFX_NEGATIVE_GLOW_MEDIUM: brightness = mix(brightness, 1 - brightness, abs(cos(time * 2))); break;
    case VFX_NEGATIVE_GLOW_FAST: brightness = mix(brightness, 1 - brightness, abs(cos(time * 4))); break;

/*
	case 8:
	  float noise = noise(InternalTileUV(texUV));
	  brightness *= noise + abs(cos(time)) * (1 - noise);
	  break;
	  */

	/*
	case 7:
	  // brightness *= abs(cos(time * (texUV.x / tileUVSize.x)));
	  // brightness *= abs(cos(time * mod(texUV.x, tileUVSize.x)));
	  
	  float xOffset = texUV.x - (tileUVSize.x * floor(texUV.x / tileUVSize.x));
	  xOffset *= tileUVSize.x;
	  brightness *= abs(cos(xOffset));
	  break;
	  */
  }

  color = vec4(brightness, brightness, brightness, 1) * vec4(fragColor, 1);
  color.a = color.r;
}
