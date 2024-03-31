using System;

namespace GPT
{
    [Flags]
    public enum GPTSupportingFileIOExtensions
    {
        c, cpp, cs, css, go, html, java, js,
        json, md, php, py, rb, rs, ts, txt, xml,
        yaml, yml, h, hpp, hxx, cxx, cc, hh, m, mm,
        swift, sh, bash, ps1, bat, psd1, psm1, psm,
        uxml, uss, shader, cginc, glsl, hlsl, compute,
        pdf, doc, docx, xls, xlsx, ppt, pptx, rtf,
    }
    public enum Voice
    {
        alloy,
        Echo,
        fable,
        onyx,
        nova,
        shimmer
    }

    public enum AudioResponseFormat
    {
        mp3,
        opus,
        aac,
        flac,
        wav,
        pcm
    }

    public enum ScriptResponseFormat
    {
        json,
        text,
        srt,
        verbose_json,
        vtt
    }

    public enum EncodingFormat
    {
        Float,
        Base64
    }
    public enum Purpose
    {
        fine_tune,
        assistants
    }

    public enum ImageResponseFormat
    {
        Url,
        B64_json
    }

    public enum ImageStyle
    {
        vivid,
        natural,
    }

    public enum IsoLanguage
    {
        en,
        es,
        fr,
        de,
        it,
        ja,
        ko,
        pt,
        ru,
        zh
    }

    public enum GPTResponseMode
    {
        Streaming,
        Block
    }
}