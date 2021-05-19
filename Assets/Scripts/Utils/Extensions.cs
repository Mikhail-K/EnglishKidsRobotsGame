using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
	public static class UIExtensions
	{
		public static Vector2 WorldToCanvas(this Canvas canvas,
											Vector3 world_position,
											Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
			if (camera == null)
				return Vector2.zero;

			var viewport_position = camera.WorldToViewportPoint(world_position);
			var canvas_rect = canvas.GetComponent<RectTransform>();
			return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) /*- (canvas_rect.sizeDelta.x * canvas_rect.pivot.x)*/,
							   (viewport_position.y * canvas_rect.sizeDelta.y) /*- (canvas_rect.sizeDelta.y * canvas_rect.pivot.y)*/);
		}

		public static Vector2 MouseToCanvas(this Canvas canvas,
											Vector2 mousePosition)
		{
			var pos = mousePosition / canvas.scaleFactor;
			return pos;
		}
	}
}