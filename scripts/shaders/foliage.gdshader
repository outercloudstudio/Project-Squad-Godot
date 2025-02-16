shader_type canvas_item;

uniform sampler3D noise: repeat_enable, filter_linear;
uniform float speed = 0.04;
uniform float noise_strength = 2.0;
instance uniform int id;

const float noise_scale = 0.5;

void fragment() {
	float random_offset = float(id) / 100.0;
	vec2 noise_pixel_location = floor(UV / TEXTURE_PIXEL_SIZE + random_offset * TEXTURE_PIXEL_SIZE * 64.0);
	vec2 noise_uv = mod(noise_pixel_location / 128.0 * noise_scale + random_offset , 1.0);
	float noise_offset_x = (texture(noise, vec3(noise_uv, mod(TIME * speed, 1))).r - 0.5) * noise_strength;
	float noise_offset_y = (texture(noise, vec3(mod(noise_uv + 0.73, 1.0), mod(TIME * speed, 1))).r - 0.5) * noise_strength / 2.0;
	vec2 noise_pixel_step = round(vec2(noise_offset_x, noise_offset_y));
	vec2 shifted_uv = UV + noise_pixel_step * TEXTURE_PIXEL_SIZE;

	COLOR = texture(TEXTURE, shifted_uv);
	
	//COLOR = vec4(noise_offset_x, noise_offset_y, 0.0, 1.0);

	//COLOR.rgb = vec3(noise_offset_x / noise_strength + 0.5, noise_offset_y / noise_strength + 0.5, 0.0);

	//COLOR.rgb = vec3(random_offset);
}