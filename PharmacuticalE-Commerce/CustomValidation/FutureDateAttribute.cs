﻿using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
	public override bool IsValid(object value)
	{
		if (value is DateTime date)
		{
			return date >= DateTime.Today;
		}
		return false;
	}
}
