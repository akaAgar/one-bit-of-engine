/*
==========================================================================
This file is part of One Bit of Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
One Bit of Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
One Bit of Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with One Bit of Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

#version 330 core

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

uniform vec2 tileUVSize;
uniform int animationFrame;
uniform float time;

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

vec2 getUVFromVFX(vec2 baseUV, int vfx)
{
  switch (vfx)
  {
    default:
      return baseUV;

    case VFX_SLANTED_RIGHT:
    case VFX_SLANTED_LEFT:
    case VFX_OSCILLATE_TOP_SLOW:
    case VFX_OSCILLATE_TOP_MEDIUM:
    case VFX_OSCILLATE_TOP_FAST:
    case VFX_OSCILLATE_BOTTOM_SLOW:
    case VFX_OSCILLATE_BOTTOM_MEDIUM:
    case VFX_OSCILLATE_BOTTOM_FAST:
    case WAVE_HORIZONTAL_SLOW:
    case WAVE_HORIZONTAL_MEDIUM:
    case WAVE_HORIZONTAL_FAST:
    case WAVE_VERTICAL_SLOW:
    case WAVE_VERTICAL_MEDIUM:
    case WAVE_VERTICAL_FAST:
      break;
  }

  vec2 tileUV = vec2(floor(baseUV.x / tileUVSize.x) * tileUVSize.x, floor(baseUV.y / tileUVSize.y) * tileUVSize.y);
  vec2 offsetUV = vec2((baseUV.x - floor(baseUV.x / tileUVSize.x) * tileUVSize.x) / tileUVSize.x, (baseUV.y - floor(baseUV.y / tileUVSize.y) * tileUVSize.y) / tileUVSize.y);

  float speed = 1;
  switch (vfx)
  {
    case VFX_OSCILLATE_TOP_MEDIUM:
    case VFX_OSCILLATE_BOTTOM_MEDIUM:
    case WAVE_HORIZONTAL_MEDIUM:
    case WAVE_VERTICAL_MEDIUM:
      speed = 2; break;

    case VFX_OSCILLATE_TOP_FAST:
    case VFX_OSCILLATE_BOTTOM_FAST:
    case WAVE_HORIZONTAL_FAST:
    case WAVE_VERTICAL_FAST:
      speed = 4; break;
  }

  switch (vfx)
  {
    case VFX_SLANTED_RIGHT:
      offsetUV.x = clamp(offsetUV.x + offsetUV.y * .2, 0, 1);
      return tileUV + offsetUV * tileUVSize;

    case VFX_SLANTED_LEFT:
      offsetUV.x = clamp(offsetUV.x + (1 - offsetUV.y) * .2, 0, 1);
      return tileUV + offsetUV * tileUVSize;

    case VFX_OSCILLATE_TOP_SLOW:
    case VFX_OSCILLATE_TOP_MEDIUM:
    case VFX_OSCILLATE_TOP_FAST:
    case VFX_OSCILLATE_BOTTOM_SLOW:
    case VFX_OSCILLATE_BOTTOM_MEDIUM:
    case VFX_OSCILLATE_BOTTOM_FAST:
      float oscillateOffsetY = (1 - offsetUV.y);
      if (vfx >= VFX_OSCILLATE_BOTTOM_SLOW) oscillateOffsetY = offsetUV.y;
      offsetUV.x = clamp(offsetUV.x + oscillateOffsetY * cos(time * speed) * .1, 0.01, 0.99);
      return tileUV + offsetUV * tileUVSize;

    case WAVE_HORIZONTAL_SLOW:
    case WAVE_HORIZONTAL_MEDIUM:
    case WAVE_HORIZONTAL_FAST:
      offsetUV.x = clamp(offsetUV.x + cos(time * speed) * sin(offsetUV.y * 16) * .15, 0.01, 0.99);
      return tileUV + offsetUV * tileUVSize;

    case WAVE_VERTICAL_SLOW:
    case WAVE_VERTICAL_MEDIUM:
    case WAVE_VERTICAL_FAST:
      offsetUV.y = clamp(offsetUV.y + cos(time * speed) * sin(offsetUV.x * 16) * .15, 0.01, 0.99);
      return tileUV + offsetUV * tileUVSize;
  }

  return baseUV;
}

vec4 getColorFromVFX(float brightness, int vfx)
{
  vec3 tileColor = fragColor;

  switch (vfx)
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
  }

  return vec4(brightness, brightness, brightness, 1) * vec4(tileColor, 1);
}


void main()
{
  // Get UV coordinates
  vec2 texUV = getUVFromVFX(fragUV, int(fragVFX));

  // Read pixel from the proper tilemap
  switch (int(fragTileMap))
  {
   default: color = texture(texture0, texUV); break;
   case 1: color = texture(texture1, texUV); break;
   case 2: color = texture(texture2, texUV); break;
   case 3: color = texture(texture3, texUV); break;
  }
 
  // Get brightness from the proper animation frame
  float brightness = 0;
  switch (animationFrame)
  {
    default: brightness = color.r; break;
    case 1: brightness = color.g; break;
    case 2: brightness = color.b; break;
  }

  // Get color
  color = getColorFromVFX(brightness, int(fragVFX));

  color.a = color.r;
}
