using System;
using UnityEngine;

namespace Share
{
    public enum NumberType
    {
        Int,
        Float,
    }
    public static class NumberEx
    {
        public static float OverrideNewValue(this float baseValue, CalculationType calculationType, DataUnitType dataUnitType,
            float modifier)
        {
#if UNITY_EDITOR
            switch (calculationType)
            {
                case CalculationType.Equalize when dataUnitType is DataUnitType.Numeric:
                    Debug.Log($"baseValue => modifer");
                    break;
                case CalculationType.Equalize when dataUnitType is DataUnitType.Percentage:
                    Debug.Log($"baseValue => baseValue * modifier * 0.01f");
                    break;
                case CalculationType.Add when dataUnitType is DataUnitType.Numeric:
                    Debug.Log($"baseValue => baseValue + modifier");
                    break;
                case CalculationType.Add when dataUnitType is DataUnitType.Percentage:
                    Debug.Log($"baseValue => baseValue + baseValue * modifier * 0.01f");
                    break;
                case CalculationType.Multiply when dataUnitType is DataUnitType.Numeric:
                    Debug.Log($"baseValue => baseValue * modifier");
                    break;
                case CalculationType.Multiply when dataUnitType is DataUnitType.Percentage:
                    Debug.Log($"baseValue = baseValue * baseValue * modifier * 0.01f");
                    break;
                default:
                    Debug.Log($"None => return baseValue");
                    break;
            }
#endif
            return calculationType switch
            {
                CalculationType.Equalize when dataUnitType is DataUnitType.Numeric => modifier,
                CalculationType.Equalize when dataUnitType is DataUnitType.Percentage => (baseValue == 0 ? 1 : baseValue) * modifier * 0.01f,
                CalculationType.Add when dataUnitType is DataUnitType.Numeric => baseValue + modifier,
                CalculationType.Add when dataUnitType is DataUnitType.Percentage => baseValue + baseValue * modifier * 0.01f,
                CalculationType.Subtract when dataUnitType is DataUnitType.Numeric => baseValue - modifier,
                CalculationType.Subtract when dataUnitType is DataUnitType.Percentage => baseValue - baseValue * modifier * 0.01f,
                CalculationType.Multiply when dataUnitType is DataUnitType.Numeric => (baseValue == 0 ? 1 : baseValue) * modifier,
                CalculationType.Multiply when dataUnitType is DataUnitType.Percentage => (baseValue == 0 ? 1 : baseValue) * (baseValue == 0 ? 1 : baseValue) * modifier * 0.01f,
                CalculationType.None => baseValue,
                _ => baseValue
            };
        }
        /// <summary>
        /// 0 ~ 10_000 for converting to 0 ~ 1f 
        /// </summary>
        public static float RandomRangeBy(NumberType numberType)
        {
            return numberType switch
            {
                NumberType.Int => UnityEngine.Random.Range(0, 10_001),
                NumberType.Float => UnityEngine.Random.Range(0, 10_001).DotLeftShift(),
                _ => throw new ArgumentOutOfRangeException(nameof(numberType), numberType, null)
            };
        }

        public static bool RandomUnderFifty()
        {
            return UnityEngine.Random.value <= 0.5f;
        } 
        public static float DotLeftShift(this int value, int digit = 2)
        {
            digit = Math.Clamp(digit, 0, 4);

            return digit switch
            {
                0 => value,
                1 => value * 0.1f,
                2 => value * 0.01f,
                3 => value * 0.001f,
                4 => value * 0.0001f,
                _ => throw new ArgumentOutOfRangeException(nameof(digit), digit, null)
            };
        }
        public static int DotRightShift(this float value, int digit = 2)
        {
            return digit switch
            {
                0 => (int)(value),
                1 => (int)(value * 10),
                2 => (int)(value * 100),
                3 => (int)(value * 1000),
                4 => (int)(value * 10000),
                _ => throw new ArgumentOutOfRangeException(nameof(digit), digit, null)
            };
        }
        public static int DotRightShift(this int value, int digit = 2)
        {
            return digit switch
            {
                0 => value,
                1 => value * 10,
                2 => value * 100,
                3 => value * 1000,
                4 => value * 10000,
                _ => throw new ArgumentOutOfRangeException(nameof(digit), digit, null)
            };
        }
    
        public static int Pow(this int value, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= value;
            }
            return result;
        }
        public static float Pow(this float value, int exponent)
        {
            float result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= value;
            }
            return result;
        }
        public static int FloorToInt(this float value)
        {
            return Mathf.FloorToInt(value);
        }
        public static int RoundToInt(this float value)
        {
            return Mathf.RoundToInt(value);
        }
        public static int DivideBy2(this int value)
        {
            return value >> 1;
        }
        public static int MultiplyBy2(this int value)
        {
            return value << 1;
        }
        public static bool IsEven(this int value)
        {
            return (value & 1) == 0;
        }
        public static bool IsOdd(this int value)
        {
            return (value & 1) == 1;
        }
        public static bool IsEven(this float value)
        {
            return ((int)value & 1) == 0;
        }
        public static bool IsOdd(this float value)
        {
            return ((int)value & 1) == 1;
        }
        public static int And(this int value, int mask)
        {
            return value & mask;
        }
        public static int Or(this int value, int mask)
        {
            return value | mask;
        }
        public static int Xor(this int value, int mask)
        {
            return value ^ mask;
        }
        public static int Not(this int value)
        {
            return ~value;
        }
        public static bool Contains(this int value, params int[] masks)
        {
            if(masks.Length == 0) throw new ArgumentNullException(nameof(masks));
            foreach (int mask in masks)
            {
                if ((value & mask) != mask) continue;
                return true;
            }
            return false;
        }
        public static int BoolToInt(this bool value)
        {
            return value ? 1 : 0;
        }
        public static bool IntToBool(this int value)
        {
            return value != 0;
        }
    
        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }
        public static int Clamp(this int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }
        public static float Clamp01(this float value)
        {
            return Mathf.Clamp01(value);
        }
        public static float ClampDigit(this float value, int digit)
        {
            if(digit < 0) throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            if(digit is 0) return Mathf.FloorToInt(value);
            return Mathf.FloorToInt(value * 1.DotRightShift(digit)) * 1.DotLeftShift(digit);
        }
        public static float Add(this float @base, float value, DataUnitType dataUnitType = DataUnitType.Numeric)
        {
            if(dataUnitType is DataUnitType.None) throw new ArgumentNullException(nameof(dataUnitType));
            return dataUnitType is DataUnitType.Numeric ? @base + value : @base + @base * value * 0.01f;
        }
        public static float Sub(this float @base, float value, DataUnitType dataUnitType = DataUnitType.Numeric)
        {
            if(dataUnitType is DataUnitType.None) throw new ArgumentNullException(nameof(dataUnitType));
            return dataUnitType is DataUnitType.Numeric ? @base - value : @base - @base * value * 0.01f;
        }
        public static float Mul(this float @base, float value, DataUnitType dataUnitType = DataUnitType.Numeric)
        {
            if(dataUnitType is DataUnitType.None) throw new ArgumentNullException(nameof(dataUnitType));
            return dataUnitType is DataUnitType.Numeric ? @base * value : @base * @base * value * 0.01f;
        }
        public static float Div(this float @base, float value, DataUnitType dataUnitType = DataUnitType.Numeric)
        {
            if(dataUnitType is DataUnitType.None) throw new ArgumentNullException(nameof(dataUnitType));
            if(value == 0) return 0;
        
            return dataUnitType is DataUnitType.Numeric ? @base / value : @base / @base * value * 0.01f;
        }
        public static int Add(this int @base, int value)
        {
            return @base + value;
        }
        public static int Sub(this int @base, int value)
        {
            return @base - value;
        }
        public static int Mul(this int @base, int value)
        {
            return @base * value;
        }
        public static int Div(this int @base, int value)
        {
            if(value == 0) return 0;
            return @base / value;
        }
        public static float Mod(this float value, float mod)
        {
            if(mod == 0) return 0;
            float result = value % mod;
            return result < 0 ? result + mod : result;
        }
        public static int Mod(this int value, int mod)
        {
            if(mod == 0) return 0;
            int result = value % mod;
            return result < 0 ? result + mod : result;
        }
        public static float Neg(this float value)
        {
            return -value;
        }
        public static int Neg(this int value)
        {
            return -value;
        }
        public static T ToEnum<T>(this int value) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static float SetHighAccuracyTimer(float delta, float maxTime)
        {
            if(maxTime is 0) return 0;
            if (delta > maxTime) delta = maxTime;
        
            return delta / maxTime;
        }
        public static bool TryCheckTimeOver(float delta, float maxTime, out float progress)
        {
            progress = SetHighAccuracyTimer(delta, maxTime);
            return progress is 1;
        }
    }
}