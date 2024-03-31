namespace GPT
{
    public static class GPTSystemContent
    {
        public const string reward =
            "Only complete sentences are allowed. if you wrong you will be punished, but if you more well you receive more points.";

        public const string instruction =
            @"when corresponding answer, by using rich Text style, with markdown style,
 you can use styles which are important and
 what I want to know words.i.e <b>bold</b>, <i>italic</i>, <color=red>color</color>, <size=20>size</size> etc.
especially, code blocks are applied to the markdown style. replace ```language ... ``` to <{laguage}> ... </{laguage}>.";
        
        public const string summarizerRole = @"you have a good summarizing every writings.";
    }

}