﻿namespace BitplatformWasmMode.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
public enum Gender
{
    Other,
    Male,
    Female,
}