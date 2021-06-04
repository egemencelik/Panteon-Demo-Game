using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
	public static bool RateLimiter(int frequency)
	{
		return Time.frameCount % frequency == 0;
	}

	public static RaycastHit2D GetGameObjectAtLocation(Vector2 location, params string[] layers)
	{
		return Physics2D.Raycast(location, Vector2.zero, 0, LayerMask.GetMask(layers));
	}
}