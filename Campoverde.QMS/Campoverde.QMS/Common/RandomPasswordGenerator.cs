﻿using System.Text;

namespace Campoverde.QMS.Common;

public class RandomPasswordGenerator
{
    // Instantiate random number generator.
    // It is better to keep a single Random instance
    // and keep using Next on the same instance.
    private readonly Random _random = new();

    // Generates a random number within a range.
    public int RandomNumber(int min, int max)
    {
        return _random.Next(min, max);
    }

    // Generates a random string with a given size.
    public string RandomString(int size, bool lowerCase = false)
    {
        var builder = new StringBuilder(size);

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.

        // char is a single Unicode character
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length = 26

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }

    // Generates a random password.
    // 4-LowerCase + 4-Digits + 2-UpperCase
    public string RandomPassword()
    {
        var passwordBuilder = new StringBuilder();

        // 4-Letters lower case
        passwordBuilder.Append(RandomString(4, true));

        // 2-Digits between 10 and 99
        passwordBuilder.Append(RandomNumber(10, 99));

        // 2-Letters upper case
        passwordBuilder.Append(RandomString(2));
        return passwordBuilder.ToString();
    }
}

